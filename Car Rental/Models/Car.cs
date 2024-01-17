using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Car_Rental.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
       
        public Car()
        {

        }
    }
}
