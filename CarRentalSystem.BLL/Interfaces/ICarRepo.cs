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
        List<Car> GetAllCars();
        Car GetCarById(int id);
        int AddCar(Car model);
        int RemoveCar(Car model);
        int UpdateCar(Car model);

        List<Car> GetCarByModel(string name);
        
    }
}
