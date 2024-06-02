using FitHub.Models.Dtos;
using FitHub.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool isNationalIdExist = _userService.UserExists(userRegisterDto.NationalId);
            if (isNationalIdExist)
            {
                return BadRequest("National Id already exists");
            }

            if (userRegisterDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(userRegisterDto);

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

            var user = _userService.GetUser(nationalId);
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

            var user = _userService.GetUser(nationalId);
            if (user == null)
            {
                return NotFound();
            }

            var result = _userService.UpdateMonths(nationalId, months);
           
            return Ok(result);
        }
    }

}
