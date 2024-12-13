using ForumApp.Domain.Models;

namespace ForumApp.Shared.TokenHelpers.Interfaces
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}
