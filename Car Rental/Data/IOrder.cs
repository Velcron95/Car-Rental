// IOrder interface
using Car_Rental.Models;

public interface IOrder
{
    IEnumerable<Order> GetAll();
    Order GetById(int id);
    void Add(Order order);
    void Update(Order order);
    void Delete(Order order);
    IEnumerable<OrderViewModel> DisplayOrders();
}
