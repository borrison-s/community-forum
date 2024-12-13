using ForumApp.Domain.Enums;

namespace ForumApp.Dtos.Users
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
    }
}
