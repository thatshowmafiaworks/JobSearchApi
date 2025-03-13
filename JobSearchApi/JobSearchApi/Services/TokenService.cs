using JobSearchApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobSearchApi.Services
{
    public class TokenService(
        IConfiguration config,
        ILogger<TokenService> logger,
        UserManager<ApplicationUser> userManager) : ITokenService
    {
        public async Task<string> GenerateToken(ApplicationUser user, IList<string> roles)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(config["SigninKey"]);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role.ToString(), role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = config["Issuer"],
                Audience = config["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var key = Encoding.UTF8.GetBytes(config["SigninKey"]);
            //logger.LogCritical(key.ToString());
            //var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            //var token = new JwtSecurityToken(
            //    issuer: config["Issuer"],
            //    audience: config["Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(8),
            //    signingCredentials: creds
            //);



            //var Token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
