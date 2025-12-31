using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NkhanhAPI.Models
{
    public class Walk
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double LengthInKm { get; set; }

        // 🔗 FK Difficulty
        public int DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; } = null!;

        // 🔗 FK Region
        public int RegionId { get; set; }
        public Region Region { get; set; } = null!;
    }
}
