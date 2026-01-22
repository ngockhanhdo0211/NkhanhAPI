using System.ComponentModel.DataAnnotations;
using NkhanhAPI.Validations;

namespace NkhanhAPI.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.1, 1000)]
        public double LengthInKm { get; set; }

        [Required]
        [NotEmptyGuid]
        public Guid RegionId { get; set; }

        [Required]
        [NotEmptyGuid]
        public Guid DifficultyId { get; set; }
    }
}
