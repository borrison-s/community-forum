using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ForumApp.DataAccess.Implementations;
using ForumApp.DataAccess.Interfaces;
using ForumApp.Domain.Models;
using ForumApp.Dtos.Users;
using ForumApp.Mappers.Users;
using ForumApp.Services.Interfaces;
using ForumApp.Shared;
using ForumApp.Shared.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ForumApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //we retrieve the AppSettings section from appSettings.json file
        private readonly IOptions<AppSettings> _options;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {

            // Validate inputs
            ValidationHelper.ValidateNotNullOrEmpty(registerUserDto.UserName, nameof(registerUserDto.UserName));
            ValidationHelper.ValidateNotNullOrEmpty(registerUserDto.Password, nameof(registerUserDto.Password));
            ValidationHelper.ValidatePasswordsMatch(registerUserDto.Password, registerUserDto.ConfirmedPassword);
            

            //Check for duplicate username

            if (_userRepository.GetUserByUsername(registerUserDto.UserName) != null)
            {
                throw new DuplicateEntityException($"Username {registerUserDto.UserName} is already in use.");
            }

            string hash = GenerateHash(registerUserDto.Password);

  
            // Use mapper to convert DTO to User
            User newUser = registerUserDto.ToUser(hash);
            _userRepository.Add(newUser);

        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.UserName) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new DataException("Username and password are required fields.");
            }

            string hash = GenerateHash(loginUserDto.Password);

            User userDb = _userRepository.GetUserByUsernameAndPassword(loginUserDto.UserName, hash);
            if (userDb == null)
            {
                throw new DataException($"Invalid login for username: {loginUserDto.UserName}");
            }

            //generate JWT token that will be returned to the client

            string userRole = string.IsNullOrEmpty(userDb.Role.ToString()) ? "noRole" : userDb.Role.ToString();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.OurSecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(5), // the token will be valid for one hour
                //signature configuration, signing algorithm that will be used to generate hash (third part of token)
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secretKeyBytes),
                        SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("userFullName", userDb.Name),
                        new Claim(ClaimTypes.NameIdentifier, userDb.UserName),
                        new Claim("userRole", userRole)
                    })
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert to string
            string resultToken = jwtSecurityTokenHandler.WriteToken(token);
            return resultToken;
        }

        private static string GenerateHash(string password)
        {
            //MD5 has algorithm
            MD5 mD5CryptoServiceProvider = MD5.Create();

            //Test123 - get the bytes => 5678432
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            //hash the bytes => 5678432 -> 6493873
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get a string from the hashed bytes, 6493873 => qsd546f
            return Encoding.ASCII.GetString(hashedBytes);
        }





    }
}
