using System.ComponentModel.DataAnnotations;

namespace Training.DotNetCore.Project.API.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 50)]
        public double LengthInKM { get; set; }//Nullable

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
