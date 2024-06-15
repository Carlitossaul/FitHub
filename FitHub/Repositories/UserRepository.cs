using FitHub.Data;
using FitHub.Models;
using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using System.Net;

namespace FitHub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        protected ResponseAPI _response;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
            _response = new ResponseAPI();
        }
        public User GetUser(int nationalId)
        {
            return _db.User.FirstOrDefault(u => u.NationalId == nationalId);
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            
            User user = new User()
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Phone = userRegisterDto.Phone,
                NationalId = userRegisterDto.NationalId,
                address = userRegisterDto.address,
                Photo = userRegisterDto.Photo,
            };  

            user.RegistrationDate = DateTime.Now;
            user.ExpirationDate = user.RegistrationDate.AddMonths(1);

            _db.User.Add(user);
            await _db.SaveChangesAsync();

            return user;

        }

        public ResponseAPI UpdateMonths(int nationalId, int months)
        {
            try
            {

            var user = _db.User.FirstOrDefault(u => u.NationalId == nationalId);
           
            user.ExpirationDate = user.ExpirationDate > DateTime.Now ? user.ExpirationDate.AddMonths(months) : DateTime.Now.AddMonths(months);
            _db.Update(user);

            _db.SaveChanges();

                _response.Result = user;
                _response.Message = "User updated successfully";
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;

                return _response;
            }

        }

        public bool UserExists(int nationalId)
        {
            return _db.User.Any(u => u.NationalId == nationalId);
        }
    }
}
