using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental.Data
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return applicationDbContext.Order.ToList();
        }

        public Order GetById(int id)
        {
            return applicationDbContext.Order.FirstOrDefault(o => o.OrderId == id);
        }

        public void Add(Order order)
        {
            applicationDbContext.Order.Add(order);
            applicationDbContext.SaveChanges();
        }

        public void Update(Order order)
        {
            applicationDbContext.Update(order);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Order order)
        {
            applicationDbContext.Remove(order);
            applicationDbContext.SaveChanges();
        }
        public IEnumerable<OrderViewModel> DisplayOrders()
        {
            var orders = applicationDbContext.Order.Include(o => o.Car).ToList();

            // Map Order entities to OrderViewModel
            var orderViewModels = orders.Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                CarBrand = o.Car.Brand,
                CarModel = o.Car.Model
            });

            return orderViewModels;
        }

       
    }
}
