using ForumApp.DataAccess.Interfaces;
using ForumApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumAppDbContext _context;

        public UserRepository(ForumAppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users
                .Include(x => x.Posts)
                .Include(x => x.Threads)
                .ToList();
        }

        public User GetById(int id)
        {
            return _context.Users
                .Include(x => x.Posts)
                .Include(x => x.Threads)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }

        public User GetUserByUsernameAndPassword(string username)
        {
            throw new NotImplementedException();
        }



    }
}
