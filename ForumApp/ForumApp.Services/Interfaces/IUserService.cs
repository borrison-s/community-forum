using ForumApp.Dtos.Users;

namespace ForumApp.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto dto);
        string LoginUser(LoginUserDto loginUserDto);
        LoginResponseDto Login(LoginUserDto loginUserDto);
    }
}
