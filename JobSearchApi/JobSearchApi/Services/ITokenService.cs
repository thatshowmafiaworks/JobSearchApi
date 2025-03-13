using JobSearchApi.Models;

namespace JobSearchApi.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
