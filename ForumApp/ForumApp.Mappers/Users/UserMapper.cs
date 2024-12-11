using ForumApp.Domain.Enums;
using ForumApp.Domain.Models;
using ForumApp.Dtos.Users;

namespace ForumApp.Mappers.Users
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUserDto, string hash)
        {
            return new User
            {
                UserName = registerUserDto.UserName,
                //Password = registerUserDto.Password //password must not be kept as plain text in db!!!!!
                Password = hash,
                Email = registerUserDto.Email,
                Name = registerUserDto.Name,
                Role = Role.RegularUser,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.Now
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                UserName = user.UserName,
                Name = user.Name,
                Role = user.Role
            };
        }


    }
}
