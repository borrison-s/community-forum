using ForumApp.Domain.Enums;

namespace ForumApp.Dtos.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }
    }
}
