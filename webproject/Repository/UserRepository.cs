using webproject.Data;
using webproject.Interfaces;
using webproject.Models;

namespace webproject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(o => o.Id == id).FirstOrDefault();
        }

        public User GetUser(string name)
        {
            return _context.Users.Where(o => o.Name == name).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UpdateUser(User user)
        {
            var saved = _context.Users.Update(user);
            return Save();
        }
        public bool UserExists(int userId)
        {
            return _context.Users.Any(p => p.Id == userId);
        }
    }
}
