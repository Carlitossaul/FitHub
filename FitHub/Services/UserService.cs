using FitHub.Models;
using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using FitHub.Services.IServices;

namespace FitHub.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser(int nationalId)
        {
            return _userRepository.GetUser(nationalId);
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            return await _userRepository.Register(userRegisterDto);
        }

        public ResponseAPI UpdateMonths(int nationalId, int days)
        {
            return _userRepository.UpdateMonths(nationalId, days);
        }

        public bool UserExists(int nationalId)
        {
            return _userRepository.UserExists(nationalId);
        }
    }
}
