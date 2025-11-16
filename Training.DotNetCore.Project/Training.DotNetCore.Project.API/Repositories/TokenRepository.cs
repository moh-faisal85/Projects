using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Training.DotNetCore.Project.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser identityUser, List<string> roles)
        {
            //Create Claims

            var _claims = new List<Claim>();
            //
            _claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            //
            foreach (var role in roles)
            {
                _claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            //
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: _claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            //
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token: token);
            return tokenResult.ToString();
        }
    }
}
