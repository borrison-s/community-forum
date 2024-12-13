using ForumApp.Domain.Models;

namespace ForumApp.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //fetches a user by their username (e.g., for checking if a username already exists during registration).
        User GetUserByUsername(string username);
        //fetches a user for login authentication
        User GetUserByUsernameAndPassword(string username, string password);
    }
}
