using FitHub.Models;
using FitHub.Models.Dtos;

namespace FitHub.Services.IServices
{
    public interface IAdminService
    {
        bool AdminExists(string username);
        ICollection<AdminDataDto> GetAdmins();
        Admin GetAdmin(string adminId);
        Task<AdminLoginResponseDto> Login(AdminLoginDto adminLoginDto);
        Task<AdminDataDto> Register(AdminRegisterDto adminRegisterDto);
        bool DeleteAdmin(string adminId);
    }
}
