using System.ComponentModel.DataAnnotations;
using ForumApp.Domain.Enums;

namespace ForumApp.Dtos.Users
{
    public class RegisterUserDto
    {
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }


        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmedPassword { get; set; }
        public Role Role { get; set; } = Role.RegularUser; // Default role

    }
}
