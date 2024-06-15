using FitHub.Models;
using FitHub.Models.Dtos;

namespace FitHub.Services.IServices
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userRegisterDto);
        User GetUser(int nationalId);
        ResponseAPI UpdateMonths(int nationalId, int days);
        bool UserExists(int nationalId);
    }
}
