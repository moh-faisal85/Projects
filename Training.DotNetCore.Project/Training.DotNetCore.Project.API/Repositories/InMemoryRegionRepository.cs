using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var regions = new List<Region>
             {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Static Auckland Region",
                    code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/18884939/pexels-photo-18884939.jpeg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Static Wellington Region",
                    code = "WLG",
                    RegionImageUrl = "https://images.pexels.com/photos/2777898/pexels-photo-2777898.jpeg"
                }
            };
            return  regions;
        }

        public Task<Region> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> Update(Guid Id, Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
