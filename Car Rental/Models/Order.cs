using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  Car Car { get; set; }
        public  Customer Customer { get; set; }

        public Order()
        {
            
        }
    }
}
