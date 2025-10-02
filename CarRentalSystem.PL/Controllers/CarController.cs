using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.BLL.Repos;
using CarRentalSystem.DAL.Models;
//using CarRentalSystem.PL.CarDTO;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.PL.DTO;
using CarRentalSystem.PL.Services;
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

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CarDTO _carDTO)
        {
            if (ModelState.IsValid) //server-side validation
            {
                if (_carDTO.ImageFile is not null)
                {
                    _carDTO.ImageName = DocumentSettings.UploadFile(_carDTO.ImageFile, "images");
                }


                var car = new Car()
                {
                    Make = _carDTO.Make,
                    Model = _carDTO.Model,
                    Year = _carDTO.Year,
                    Color = _carDTO.Color,
                    RentPricePerDay = _carDTO.RentPricePerDay,
                    IsAvailable = _carDTO.IsAvailable,
                    ImageName = _carDTO.ImageName

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

            if(car.ImageName is not null)
            {
                DocumentSettings.DeleteFile(car.ImageName, "images");
            }

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
                IsAvailable = car.IsAvailable,
                ImageName = car.ImageName

            };
            return View(cardto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, CarDTO _cardto)
        {
            if (ModelState.IsValid) //server-side validation
            {
                if(_cardto.ImageName is not null && _cardto.ImageFile is not null)
                {
                    DocumentSettings.DeleteFile(_cardto.ImageName, "images");
                }

                if(_cardto.ImageFile is not null)
                {
                    _cardto.ImageName = DocumentSettings.UploadFile(_cardto.ImageFile, "images");
                }

                var car = new Car()
                {
                    Id = id.Value,
                    Make = _cardto.Make,
                    Model = _cardto.Model,
                    Year = _cardto.Year,
                    Color = _cardto.Color,
                    RentPricePerDay = _cardto.RentPricePerDay,
                    IsAvailable = _cardto.IsAvailable,
                    ImageName = _cardto.ImageName
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
