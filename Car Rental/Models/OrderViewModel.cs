namespace Car_Rental.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
    }
}
