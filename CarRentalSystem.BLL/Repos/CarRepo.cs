using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.DAL.Data.Contexts;
using CarRentalSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.BLL.Repos
{
    public class CarRepo : ICarRepo
    {
        private readonly CarDbContexts _context;
        public CarRepo(CarDbContexts context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            return car;
        }

    
        public async Task AddCarAsync(Car model)
        {
            await _context.AddAsync(model);
            
        }
        public void RemoveCar(Car model)
        {
            _context.Remove(model);
            
        }

        public void UpdateCar(Car model)
        {
            _context.Update(model);
            
        }

        public async Task<List<Car>> GetCarByModelAsync(string name)
        {
            return await _context.Cars.Where(c=>c.Model.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
