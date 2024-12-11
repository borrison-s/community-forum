using ForumApp.DataAccess.Implementations;
using ForumApp.DataAccess.Interfaces;
using ForumApp.Dtos.Users;
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
                throw new DuplicateEntityException("Username already exist");
            }

            //Create and save the user

        }
    }
}
