using System.ComponentModel.DataAnnotations;

namespace NkhanhAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
