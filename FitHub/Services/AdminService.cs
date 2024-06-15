using FitHub.Models;
using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using FitHub.Services.IServices;

namespace FitHub.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public bool AdminExists(string username)
        {
            return _adminRepository.AdminExists(username);
        }

        public Admin GetAdmin(string adminId)
        {
            return _adminRepository.GetAdmin(adminId);
        }

        public ICollection<AdminDataDto> GetAdmins()
        {
            return _adminRepository.GetAdmins();
        }

        public Task<AdminLoginResponseDto> Login(AdminLoginDto adminLoginDto)
        {
            return _adminRepository.Login(adminLoginDto);
        }

        public Task<AdminDataDto> Register(AdminRegisterDto adminRegisterDto)
        {
           return _adminRepository.Register(adminRegisterDto);
        }

        public bool DeleteAdmin(string adminId)
        {
            return _adminRepository.DeleteAdmin(adminId);
        }
    }
}
