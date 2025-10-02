using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.DAL.Models;

namespace CarRentalSystem.BLL.Interfaces
{
    public interface ICarRepo
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car model);
        void RemoveCar(Car model);
        void UpdateCar(Car model);

        Task<List<Car>> GetCarByModelAsync(string name);
        
    }
}
