using AutoMapper;
using CarRentalSystem.DAL.Models;

namespace CarRentalSystem.PL.DTO.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpDTO, AppUser>().ReverseMap();
        }
    }
}
