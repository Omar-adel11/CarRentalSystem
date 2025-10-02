using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.BLL.Repos;
using CarRentalSystem.DAL.Models;
//using CarRentalSystem.PL.CarDTO;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.PL.DTO;
namespace CarRentalSystem.PL.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CarController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Car> cars;
            if(SearchInput is null)
            {
                 cars = _unitOfWork.carRepo.GetAllCars();

            }
            else
            {
                 cars = _unitOfWork.carRepo.GetCarByModel(SearchInput);
            }
                return View(cars);
        }

        [HttpPost]
        public IActionResult Add()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Add(CarDTO _carDTO)
        {
            if (ModelState.IsValid) //server-side validation
            {
                var car = new Car()
                {
                    Make = _carDTO.Make,
                    Model = _carDTO.Model,
                    Year = _carDTO.Year,
                    Color = _carDTO.Color,
                    RentPricePerDay = _carDTO.RentPricePerDay,
                    IsAvailable = _carDTO.IsAvailable
                };

                var count = _unitOfWork.carRepo.AddCar(car);

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(_carDTO);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) 
                return BadRequest("Invalid Id");
            var car = _unitOfWork.carRepo.GetCarById(id.Value);
            var count = _unitOfWork.carRepo.RemoveCar(car);
            if(count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Can't Delete Car");

        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Invalid Id");
            var car = _unitOfWork.carRepo.GetCarById(id.Value);
            if (car == null)
                return NotFound();
            return View(car);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest("Invalid Id");
            var car = _unitOfWork.carRepo.GetCarById(id.Value);
            if (car == null)
                return NotFound();
            var cardto = new CarDTO()
            {
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                RentPricePerDay = car.RentPricePerDay,
                IsAvailable = car.IsAvailable
            };
            return View(cardto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, CarDTO _cardto)
        {
            if (ModelState.IsValid) //server-side validation
            {
                var car = new Car()
                {
                    Id = id.Value,
                    Make = _cardto.Make,
                    Model = _cardto.Model,
                    Year = _cardto.Year,
                    Color = _cardto.Color,
                    RentPricePerDay = _cardto.RentPricePerDay,
                    IsAvailable = _cardto.IsAvailable
                };

                var count = _unitOfWork.carRepo.UpdateCar(car);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_cardto);
        }

        

    }
}
