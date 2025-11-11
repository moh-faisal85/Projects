using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateAsync(Region region);
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid Id);
        Task<Region?> UpdateAsync(Guid Id, Region region);
        Task<Region?> DeleteAsync(Guid Id);
    }
}
