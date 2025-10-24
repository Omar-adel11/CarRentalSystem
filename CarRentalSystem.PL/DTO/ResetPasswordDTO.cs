using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.PL.DTO
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public string? Email { get; set; }
    }
}
