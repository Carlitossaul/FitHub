using AutoMapper;
using FitHub.Models;
using FitHub.Models.Dtos;

namespace FitHub.FithubMappers
{
    public class FithubMapper : Profile
    {
        public FithubMapper()
        {
            CreateMap<Admin, AdminDataDto>().ReverseMap();
            CreateMap<Admin, AdminLoginResponseDto>().ReverseMap();
            
        }
    }
}
