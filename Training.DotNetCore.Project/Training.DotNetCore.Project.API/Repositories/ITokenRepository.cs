using Microsoft.AspNetCore.Identity;

namespace Training.DotNetCore.Project.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
