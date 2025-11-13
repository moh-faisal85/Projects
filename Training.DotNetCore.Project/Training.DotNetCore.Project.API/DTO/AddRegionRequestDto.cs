using System.ComponentModel.DataAnnotations;

namespace Training.DotNetCore.Project.API.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(length: 3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(length: 3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(length: 100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
        
        public string? RegionImageUrl { get; set; }
    }
}
