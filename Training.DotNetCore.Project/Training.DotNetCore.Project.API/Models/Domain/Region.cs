namespace Training.DotNetCore.Project.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        //Nullable (optional) field
        public string? RegionImageUrl { get; set; }
    }
}
