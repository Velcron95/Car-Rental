using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomer
{
    private readonly ApplicationDbContext applicationDbContext;

    public CustomerRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public IEnumerable<Customer> GetAll()
    {
        return applicationDbContext.Customer.OrderBy(x => x.Email);
    }

    public Customer GetById(int id)
    {
        return applicationDbContext.Customer.FirstOrDefault(c => c.CustomerId == id);
    }

    public void Add(Customer customer)
    {
        applicationDbContext.Customer.Add(customer);
        applicationDbContext.SaveChanges();
    }

    public void Update(Customer customer)
    {
        applicationDbContext.Update(customer);
        applicationDbContext.SaveChanges();
    }

    public void Delete(Customer customer)
    {
        applicationDbContext.Remove(customer);
        applicationDbContext.SaveChanges();
    }

    
}

