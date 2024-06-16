using AutoMapper;
using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitHub.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        protected ResponseAPI _response;
        private readonly IMapper _mapper;
        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _response = new ResponseAPI();
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdminRegisterDto adminRegisterDto)
        {
            if (adminRegisterDto == null)
            {
                return BadRequest();
            }
            bool admin = _adminRepository.AdminExists(adminRegisterDto.UserName);
            if (admin)
            {
                _response.Message = "Username already exists!";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var result = await _adminRepository.Register(adminRegisterDto);
            if (result == null)
            {
                _response.Message = "Registration failed!";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            _response.Message = "Registration successful!";
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = result;
            Console.WriteLine(result);
            return Ok(_response);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var admins = _adminRepository.GetAdmins();

            if (!admins.Any())
            {
                return NotFound(new {message = "Not admins already"});
            }

            return Ok(admins);
        }

        [HttpDelete("{adminId}")]
        public IActionResult DeleteAdmin(string adminId)
        {
            if (!_adminRepository.DeleteAdmin(adminId))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{adminId}")]
        public IActionResult GetAdmin(string adminId)
        {
            var admin = _adminRepository.GetAdmin(adminId);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDto adminLoginDto)
        {
            if (adminLoginDto == null)
            {
                return BadRequest();
            }
            var responseLogin = await _adminRepository.Login(adminLoginDto);
            if (responseLogin.Admin == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _response.Message = "Login failed!";
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            _response.Message = "Login successful!";
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = responseLogin;
            return Ok(_response);
        }
    
    }
}
