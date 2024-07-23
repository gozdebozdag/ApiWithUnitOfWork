using ApiWithUnitOfWork.DataAccess.Context;
using ApiWithUnitOfWork.Domain.Entities;
using ApiWithUnitOfWork.Domain.Repository;
using ApiWithUnitOfWork.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace ApiWithUnitOfWork.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            private readonly ApiDbContext _context;
            private readonly IConfiguration _configuration;
            private readonly IUnitOfWork _unitOfWork;
            public AuthController(ApiDbContext context, IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _context = context;
                _configuration = configuration;
            }
            [HttpPost]
            public async Task<IActionResult> Login(string Email, string Sifre)
            {
                Expression<Func<User, bool>> predicate = x => x.Email == Email && x.PasswordHash == Sifre;

                var kullanici = _unitOfWork.User.Authenticate(predicate);
                if (kullanici == null)
                {
                    return Ok(new { status = 0, message = "Eposta veya şifre hatalı." });
                }
           

                var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signingCredentials = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    //claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: signingCredentials
                    );
                var veriler = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { status = 1, message = veriler });
            }
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] UserForRegisterDto kullaniciForRegisterDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = _unitOfWork.User.GetByEmail(kullaniciForRegisterDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email already exists");
                }

                //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(kullaniciForRegisterDto.Sifre);

                var newUser = new User
                {
                    Email = kullaniciForRegisterDto.Email,
                    PasswordHash = kullaniciForRegisterDto.Password,
                    UserName = kullaniciForRegisterDto.Username
                };

                _unitOfWork.User.Add(newUser); // Kullanıcıyı ekleyin
                await _unitOfWork.CompleteAsync(); // Değişiklikleri kaydedin

                return StatusCode(201, "User registered successfully");
            }
        }
    
}
