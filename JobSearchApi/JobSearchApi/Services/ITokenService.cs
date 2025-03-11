using JobSearchApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobSearchApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
