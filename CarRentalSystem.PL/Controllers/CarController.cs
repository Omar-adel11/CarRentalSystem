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
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Car> cars;
            if(SearchInput is null)
            {
                 cars = await _unitOfWork.carRepo.GetAllCarsAsync();

            }
            else
            {
                 cars = await _unitOfWork.carRepo.GetCarByModelAsync(SearchInput);
            }
                return View(cars);
        }

        [HttpPost]
        public IActionResult Add()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(CarDTO _carDTO)
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

                await _unitOfWork.carRepo.AddCarAsync(car);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(_carDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
                return BadRequest("Invalid Id");
            var car = await _unitOfWork.carRepo.GetCarByIdAsync(id.Value);
            _unitOfWork.carRepo.RemoveCar(car);
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Can't Delete Car");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Invalid Id");
            var car = await _unitOfWork.carRepo.GetCarByIdAsync(id.Value);
            if (car == null)
                return NotFound();
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Invalid Id");
            var car = await _unitOfWork.carRepo.GetCarByIdAsync(id.Value);
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
        public async Task<IActionResult> Edit([FromRoute] int? id, CarDTO _cardto)
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

                 _unitOfWork.carRepo.UpdateCar(car);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_cardto);
        }

        

    }
}
