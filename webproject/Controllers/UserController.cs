using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webproject.Data;
using webproject.Interfaces;
using webproject.Models;
using webproject.Repository;
using webproject.Service;

namespace webproject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notifRepository;
        private readonly UserService _userService;
        public UserController(IUserRepository userRepository, DataContext context, UserService userService)
        {
            _userRepository = userRepository;
            _userService= userService;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users= _userRepository.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        //[HttpGet("{userId}")]
        //[ProducesResponseType(200, Type = typeof(User))]
        //[ProducesResponseType(400)]
        //public IActionResult GetUser(int userId)
        //{
        //    if (_userRepository.GetUser(userId) == null)
        //        return NotFound();
        //    var user = _userRepository.GetUser(userId);
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    return Ok(user);
        //}

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var user_v = _userRepository.GetUser(user.Id);

            if (user_v != null)
            {
                ModelState.AddModelError("", "user already exists");
                return StatusCode(422, ModelState);
            }
            _userRepository.CreateUser(user);
            return Ok("Successfully created");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updatedUser)
        {
            if (userId != updatedUser.Id)
            {
                return BadRequest("Mismatch between URL id and user id in the body.");
            }

            var existingUser =  _userRepository.GetUser(userId);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;

            _userRepository.UpdateUser(existingUser);

            return Ok("Updated");
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            _userRepository.DeleteUser(_userRepository.GetUser(userId));
            return Ok("User Deleted");
        }
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Notification>))]
        public IActionResult GetNotifications(int userId)
        {
            var notifs = _userService.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notifs);
        }


    }
}
