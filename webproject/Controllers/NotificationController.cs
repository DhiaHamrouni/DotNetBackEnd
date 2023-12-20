using Microsoft.AspNetCore.Mvc;
using webproject.Interfaces;
using webproject.Models;
using webproject.Repository;

namespace webproject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        public NotificationController(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Notification>))]
        public IActionResult GetNotifications()
        {
            var notifs = _notificationRepository.GetNotifications();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notifs);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddNotification([FromBody] Notification notification_v)
        {
            if (notification_v == null)
            {
                return BadRequest("Invalid notification data.");
            }
            var user = _userRepository.GetUser(notification_v.Id_User);
            Console.WriteLine(user.Name);
            user.notifications.Add(notification_v);
            var notification = new Notification
            {
                Title = notification_v.Title,
                Description = notification_v.Description,
                date= notification_v.date,
                type= notification_v.type,
                Label = notification_v.Label,
                user= user
            };
            _userRepository.UpdateUser(user);
            _notificationRepository.CreateNotification(notification);

            return Ok("Notification added successfully.");
        }
        [HttpGet("{userid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Notification>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotifFromUser(int userid)
        {
            List<Notification> notifications = new List<Notification>();
            var notifs = _notificationRepository.GetNotifications();
            foreach (var notif in notifs)
            {
                if (notif.Id_User == userid)
                {
                    notifications.Add(notif);
                }
            }
            if (notifications.Count > 0 )
            {
                return Ok(notifications);
            }
            return BadRequest("that user does not exist");
        }
        [HttpDelete("{notifId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNotification(int notifId) {
            if (!_notificationRepository.NotificationExist(notifId))
            {
                return NotFound();
            }
            _notificationRepository.DeleteNotification(_notificationRepository.GetNotification(notifId));
            return Ok("Notification Deleted");
        }


    }
}
