using ForumApp.DataAccess.Implementations;
using ForumApp.Dtos.Users;
using ForumApp.Services.Interfaces;

namespace ForumApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public void RegisterUser(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
