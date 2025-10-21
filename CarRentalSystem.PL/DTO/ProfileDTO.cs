using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.PL.DTO
{
    public class ProfileDTO
    {
        public string Username { get; set; }
       

        // For the form
        [Required]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile? Image { get; set; }

        public string? PictureURL { get; set; }
        public string? Address { get; set; }



        [Required(ErrorMessage = "Phone number is required.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [CreditCard]
        public string CreditCardNumber { get; set; }
    }
}
