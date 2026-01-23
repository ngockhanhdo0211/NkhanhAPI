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
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(
                identityUser, request.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }

            if (request.Roles != null && request.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(
                    identityUser, request.Roles);

                if (!identityResult.Succeeded)
                {
                    return BadRequest(identityResult.Errors);
                }
            }

            return Ok("User registered successfully");
        }

        // =========================
        // LOGIN
        // POST: /api/auth/login
        // =========================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Username or password incorrect");
            }

            var checkPassword = await userManager.CheckPasswordAsync(
                user, request.Password);

            if (!checkPassword)
            {
                return BadRequest("Username or password incorrect");
            }

            // 🔐 Generate JWT
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new LoginResponseDto
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
