using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.PL.DTO
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        public string SMSOrEmail { get; set; }
        
    }
}
