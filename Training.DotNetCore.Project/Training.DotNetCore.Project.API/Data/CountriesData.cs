using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Data
{
    public static class CountriesData
    {
        public static List<Country> Get()
        {
            var countries = new[]
            {
                new {Id=1, Name="United States"},
                new {Id=2, Name="Germany"},
                new {Id=3, Name="Brazil"},
                new {Id=4, Name="Chaina"},
                new {Id=5, Name="Italy"},
                new {Id=6, Name="Japan"},
            };
            return countries.Select(c => new Country { Id = c.Id, Name = c.Name }).ToList();
        }
    }
}
