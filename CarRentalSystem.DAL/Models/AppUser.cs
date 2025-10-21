using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace CarRentalSystem.DAL.Models
{
    public class AppUser :IdentityUser
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsAgree { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? Address { get; set; }
        public string? PictureURL { get; set; }
    }
}
