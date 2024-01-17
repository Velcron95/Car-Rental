using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Customer> Customer { get; set; }
        
        public DbSet<Order> Order { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
            
        }

    }
}
