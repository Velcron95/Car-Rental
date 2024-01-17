using Car_Rental.Models;
using System.Collections.Generic;

public interface ICustomer
{
    IEnumerable<Customer> GetAll();
    Customer GetById(int id);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
    
}
