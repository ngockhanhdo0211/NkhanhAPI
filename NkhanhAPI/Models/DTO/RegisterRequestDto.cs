using System.ComponentModel.DataAnnotations;
namespace NkhanhAPI.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
