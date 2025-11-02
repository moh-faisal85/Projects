namespace Training.DotNetCore.Project.API.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        //Nullable (optional) field
        public string? RegionImageUrl { get; set; }
    }
}
