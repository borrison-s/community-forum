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

namespace ForumApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
