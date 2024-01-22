using Car_Rental.Models;
using System.Collections.Generic;

public interface IUser
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
    
}
