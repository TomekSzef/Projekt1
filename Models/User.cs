using System.ComponentModel.DataAnnotations;

namespace AutoWebApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        public string LastName { get; set; } = String.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string? NIP { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public bool IsAdmin { get; set; } = false;
        public List<Order> Orders { get; set; } = new();
        public User()
        {

        }
    }
}
