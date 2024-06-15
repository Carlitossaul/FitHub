using FitHub.Models;
using FitHub.Models.Dtos;

namespace FitHub.Repositories.IRepositories
{
    public interface IAdminRepository
    {
        public Task<AdminDataDto> Register(AdminRegisterDto adminRegisterDto);  
        public Task<AdminLoginResponseDto> Login(AdminLoginDto adminLoginDto);
        public bool AdminExists(string username);
        public Admin GetAdmin(string adminId);
        public ICollection<AdminDataDto> GetAdmins(); 
        public bool DeleteAdmin(string adminId);
    }
}
