using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.DAL.Data.Contexts;
using CarRentalSystem.DAL.Models;

namespace CarRentalSystem.BLL.Repos
{
    public class CarRepo : ICarRepo
    {
        private readonly CarDbContexts _context;
        public CarRepo(CarDbContexts context)
        {
            _context = context;
        }

        public List<Car> GetAllCars()
        {
            var cars = _context.Cars.ToList();
            return cars;
        }

        public Car GetCarById(int id)
        {
            var car = _context.Cars.Find(id);
            return car;
        }

    
        public int AddCar(Car model)
        {
            _context.Add(model);
            return _context.SaveChanges();
        }
        public int RemoveCar(Car model)
        {
            _context.Remove(model);
            return _context.SaveChanges();
        }

        public int UpdateCar(Car model)
        {
            _context.Update(model);
            return _context.SaveChanges();
        }

        public List<Car> GetCarByModel(string name)
        {
            return _context.Cars.Where(c=>c.Model.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
