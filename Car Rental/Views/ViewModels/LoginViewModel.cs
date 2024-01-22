using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class LoginViewModel
    {
        public int UserId { get; set; } 
        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
