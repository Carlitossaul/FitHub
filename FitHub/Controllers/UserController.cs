using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool isNationalIdExist = _userRepository.UserExists(userRegisterDto.NationalId);
            if (isNationalIdExist)
            {
                return BadRequest("National Id already exists");
            }

            if (userRegisterDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userRepository.Register(userRegisterDto);

            if (result == null)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }

            return Ok(result);
        }



        [HttpGet("getuser/{nationalId}")]
        public IActionResult GetUser(int nationalId)
        {

            if (string.IsNullOrEmpty(nationalId.ToString()))
            {
                return BadRequest();
            }

            var user = _userRepository.GetUser(nationalId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPatch("updatemonths/{nationalId}")]
        public IActionResult UpdateMonths(int nationalId, [FromBody] int months)
        {
            if (string.IsNullOrEmpty(nationalId.ToString()) || string.IsNullOrEmpty(months.ToString()))
            {
                return BadRequest();
            }

            var user = _userRepository.GetUser(nationalId);
            if (user == null)
            {
                return NotFound();
            }

            var result = _userRepository.UpdateMonths(nationalId, months);
           
            return Ok(result);
        }
    }

}
