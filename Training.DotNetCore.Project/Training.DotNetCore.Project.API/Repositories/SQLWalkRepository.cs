using Microsoft.EntityFrameworkCore;
using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true)
        {
            //var walks = await dbContext.Walks.ToListAsync();

            var walks = dbContext.Walks
                            .Include("Difficulty")//Get along with Difficulty Entity
                            .Include("Region")//Get along with Difficulty Entity
                            .AsQueryable();

            //Second Commented
            //var walks = await dbContext.Walks
            //                .Include("Difficulty")//Get along with Difficulty Entity
            //                .Include("Region")//Get along with Difficulty Entity
            //                .ToListAsync();

            //First Commented
            //var walks = await dbContext.Walks
            //    .Include(x => x.Difficulty)//Get along with Difficulty Entity
            //    .Include(x => x.Region)//Get along with Difficulty Entity
            //    .ToListAsync();

            //Apply Filter

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false && isAscending != null)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ((bool)isAscending) ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ((bool)isAscending) ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid Id)
        {
            var walk = await dbContext.Walks
                .Include("Difficulty")//Get along with Difficulty Entity
                .Include("Region")//Get along with Difficulty Entity
                .FirstOrDefaultAsync(x => x.Id == Id);
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            //Get Data from Database using Id
            var walkDomainModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            //return null if no record exists
            if (walkDomainModel == null)
            {
                return null;
            }
            //Update Domain Region Model Values Input

            walkDomainModel.Name = walk.Name;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.LengthInKM = walk.LengthInKM;
            walkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            walkDomainModel.DifficultyId = walk.DifficultyId;
            walkDomainModel.RegionId = walk.RegionId;

            //SaveChangesAsync and return updated Model to client
            await dbContext.SaveChangesAsync();

            //Return Data to client
            return walkDomainModel;
        }
        public async Task<Walk?> DeleteAsync(Guid Id)
        {
            //Get Data from Database using Id
            var walkDomainModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

            //return null if no record exists
            if (walkDomainModel == null)
            {
                return null;
            }
            //Remove entity and save changes
            dbContext.Walks.Remove(walkDomainModel);
            await dbContext.SaveChangesAsync();
            return walkDomainModel;

        }
    }
}
