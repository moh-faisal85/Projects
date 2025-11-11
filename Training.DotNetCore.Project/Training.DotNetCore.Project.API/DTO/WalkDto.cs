using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }

        //public Guid DifficultyId { get; set; }

        //public Guid RegionId { get; set; }
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
