using webproject.Models;

namespace webproject.Interfaces
{
    public interface INotificationRepository
    {
        ICollection<Notification> GetNotifications();
        Notification GetNotification(int id);
        bool CreateNotification(Notification notif);
        bool UpdateNotification(Notification notif);
        bool DeleteNotification(Notification notif);
        bool Save();
        bool NotificationExist(int id);

    }
}
