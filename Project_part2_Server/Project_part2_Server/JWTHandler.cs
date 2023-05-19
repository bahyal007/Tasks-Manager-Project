using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskModel;

namespace Project_part2_Server
{
    public class JWTHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<TaskUser> _userManager;
        public JWTHandler(IConfiguration configuration, UserManager<TaskUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> GetTokenAsync(TaskUser user) =>
            new(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: await GetClaimsAsync(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationTimeInMinutes"])),
                signingCredentials: GetSigningCredentials());

        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]!);
            SymmetricSecurityKey secret = new(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(TaskUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.UserName!)
            };
            claims.AddRange(from role in await _userManager.GetRolesAsync(user) select new Claim(ClaimTypes.Role, role));
            return claims;
        }
    }
}
