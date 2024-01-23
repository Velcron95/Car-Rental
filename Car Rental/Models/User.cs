using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;

        public virtual List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
