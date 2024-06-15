using AutoMapper;
using FitHub.Data;
using FitHub.Models;
using FitHub.Models.Dtos;
using FitHub.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitHub.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Admin> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private string _keySecret;
        public AdminRepository(ApplicationDbContext db,UserManager<Admin> userManager,RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _keySecret = config.GetSection("AppSettings:Secret").Value;
        }
        public bool AdminExists(string username)
        {
            var admin = _db.Admin.FirstOrDefault(u => u.UserName == username);
            if(admin == null)
            {
                return false;
            }
            return true;
        }

        public ICollection<AdminDataDto> GetAdmins()
        {
            return _mapper.Map<ICollection<AdminDataDto>>(_db.Admin.ToList());
        }

        public Admin GetAdmin(string adminId)
        {
            return _db.Admin.FirstOrDefault(u => u.Id == adminId);
        }

        public async Task<AdminLoginResponseDto> Login(AdminLoginDto adminLoginDto)
        {
            var admin = _db.Admin.FirstOrDefault(u => u.UserName.ToLower() == adminLoginDto.UserName.ToLower());

            bool isValidPassword = await _userManager.CheckPasswordAsync(admin, adminLoginDto.Password);

            if(admin == null || !isValidPassword)
            {
                return new AdminLoginResponseDto()
                {
                    Token = "",
                    Admin = null,
                };
            }

            var roles = await _userManager.GetRolesAsync(admin);

            var manageToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_keySecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id),
                    new Claim(ClaimTypes.Name, admin.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manageToken.CreateToken(tokenDescriptor);

            return new AdminLoginResponseDto()
            {
                Token = manageToken.WriteToken(token),
                Admin = _mapper.Map<AdminDataDto>(admin),
            };
        }

        public async Task<AdminDataDto> Register(AdminRegisterDto adminRegisterDto)
        {
            Admin admin = new Admin()
            {
                UserName = adminRegisterDto.UserName,
                Email = adminRegisterDto.Email,
            };
            var result = await _userManager.CreateAsync(admin, adminRegisterDto.Password);

            if(result.Succeeded)
            {
                if(!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }
                await _userManager.AddToRoleAsync(admin, "admin");
                var adminReturn = _db.Admin.FirstOrDefault(admin => admin.UserName == adminRegisterDto.UserName);
                
                return _mapper.Map<AdminDataDto>(adminReturn);
            }

            return null;
        }


        public bool DeleteAdmin(string adminId)
        {
            var result = _db.Admin.Remove(_db.Admin.FirstOrDefault(u => u.Id == adminId));
            if(result != null)
            {
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
