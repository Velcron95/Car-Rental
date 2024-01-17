using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Car_Rental.Data
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CarRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Car> GetAll()
        {
            return applicationDbContext.Car.ToList();
        }

        public Car GetById(int id)
        {
            return applicationDbContext.Car.FirstOrDefault(c => c.CarId == id);
        }

        public void Add(Car car)
        {
            applicationDbContext.Car.Add(car);
            applicationDbContext.SaveChanges();
        }

        public void Update(Car car)
        {
            applicationDbContext.Update(car);
            applicationDbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            applicationDbContext.Remove(car);
            applicationDbContext.SaveChanges();
        }
    }

}
