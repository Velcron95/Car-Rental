using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }

        public virtual List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
