using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NkhanhAPI.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        // =========================
        // REGISTER
        // POST: /api/auth/register
        // =========================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            // 1️⃣ Tạo user từ DTO
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            // 2️⃣ Tạo user với password
            var identityResult = await userManager.CreateAsync(
                identityUser, request.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }

            // 3️⃣ GÁN ROLE MẶC ĐỊNH (QUAN TRỌNG)
            // ❌ KHÔNG cho client tự truyền role
            await userManager.AddToRoleAsync(identityUser, "Reader");

            return Ok(new
            {
                Message = "User registered successfully",
                Email = identityUser.Email,
                Role = "Reader"
            });
        }

        // =========================
        // LOGIN
        // POST: /api/auth/login
        // =========================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            // 1️⃣ Tìm user theo email
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Username or password incorrect");
            }

            // 2️⃣ Kiểm tra password
            var checkPassword = await userManager.CheckPasswordAsync(
                user, request.Password);

            if (!checkPassword)
            {
                return BadRequest("Username or password incorrect");
            }

            // 3️⃣ Lấy role của user
            var roles = await userManager.GetRolesAsync(user);

            // 4️⃣ Tạo claims cho JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 5️⃣ Tạo key ký JWT
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            // 6️⃣ Tạo token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            // 7️⃣ Trả token cho client
            return Ok(new LoginResponseDto
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
