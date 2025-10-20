using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.PL.DTO
{
    public class SignUpDTO
    {
        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Confirm Email is required")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Emails do not match")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsSeller { get; set; }

        public bool IsAgree { get; set; }

    }
}
