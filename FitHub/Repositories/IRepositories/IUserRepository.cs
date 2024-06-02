using FitHub.Models;
using FitHub.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Register(UserRegisterDto userRegisterDto);
        User GetUser(int nationalId);
        bool UserExists(int nationalId);
        ResponseAPI UpdateMonths(int nationalId, int days);
    }
}
