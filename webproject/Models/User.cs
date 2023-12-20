using System.ComponentModel.DataAnnotations.Schema;

namespace webproject.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Notification> notifications { get; set; } = new List<Notification>();
    }
}
