using System.ComponentModel.DataAnnotations;

namespace NkhanhAPI.Models.Domain
{
    public class Walk
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double LengthInKm { get; set; }

        // 🔗 FK Difficulty
        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; } = null!;

        // 🔗 FK Region
        public Guid RegionId { get; set; }
        public Region Region { get; set; } = null!;
    }
}
