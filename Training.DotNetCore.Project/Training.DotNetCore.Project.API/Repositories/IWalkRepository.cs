using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
