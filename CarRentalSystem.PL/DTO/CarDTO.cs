using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.PL.DTO
{
    public class CarDTO
    {
        [Required(ErrorMessage = "This is Required!")]
        public string Make { get; set; }
        [Required(ErrorMessage = "This is Required!")]
        public string Model { get; set; }
        [Required(ErrorMessage = "This is Required!")]
        public string Color { get; set; }
        [Required(ErrorMessage = "This is Required!")]
        public int Year { get; set; }
        [Required(ErrorMessage = "This is Required!")]
        public decimal RentPricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
