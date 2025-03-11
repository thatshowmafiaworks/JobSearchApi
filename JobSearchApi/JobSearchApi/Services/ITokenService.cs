using Microsoft.AspNetCore.Identity;

namespace JobSearchApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser user, IList<string> roles);
    }
}
