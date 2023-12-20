using Microsoft.EntityFrameworkCore;
using webproject.Data;
using webproject.Interfaces;

namespace webproject.Service
{
    public class UserService
    {
        private readonly DataContext _dbContext;
        public UserService(DataContext dbContext) {
            _dbContext = dbContext;
        }
        public List<object> GetUsers()
        {
            var users = _dbContext.Users.Select(user => new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                notifications = user.notifications.Select(notification => notification.Id_notif).ToList()
            }).ToList<object>();

            return users;
        }
    }
}
