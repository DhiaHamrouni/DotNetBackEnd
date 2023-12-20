using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webproject.Models
{

        public enum Label
        {
            Frais,
            Abscence,
            Entretien
        }
        public enum Type
        {
            Urgent,
            Attente
        }
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_notif { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public Type type { get; set; }
        public Label Label { get; set; }
        [Required]
        public int Id_User { get; set; } 
        public User user { get; set; }
        }
}
