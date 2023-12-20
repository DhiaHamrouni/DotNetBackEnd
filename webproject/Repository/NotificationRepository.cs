using webproject.Data;
using webproject.Interfaces;
using webproject.Models;

namespace webproject.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;
        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateNotification(Notification notif)
        {
            _context.Notifications.Add(notif);
            return Save();
        }


        public bool DeleteNotification(Notification notif)
        {
            _context.Notifications.Remove(notif);
            return Save();
        }

        public Notification GetNotification(int id)
        {
            return _context.Notifications.Where(o => o.Id_notif == id).FirstOrDefault();
        }

        public ICollection<Notification> GetNotifications()
        {
            return _context.Notifications.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public bool UpdateNotification(Notification notif)
        {
            throw new NotImplementedException();
        }

        public bool NotificationExist(int id)
        {
            return _context.Notifications.Any(n =>  n.Id_notif == id);
        }

    }
}
