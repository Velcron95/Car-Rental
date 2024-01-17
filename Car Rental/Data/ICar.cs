using Car_Rental.Models;
using System.Collections.Generic;

namespace Car_Rental.Data
{
    public interface ICar
    {
        IEnumerable<Car> GetAll();
        Car GetById(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }

}
