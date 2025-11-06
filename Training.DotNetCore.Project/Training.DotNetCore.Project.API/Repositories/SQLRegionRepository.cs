using Microsoft.EntityFrameworkCore;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
         var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == Id);
            if (existingRegion == null)
            {
                return null;
            }
            //Update Domain Region Model Values Input
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            //SaveChangesAsync and return updated Model to client
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
