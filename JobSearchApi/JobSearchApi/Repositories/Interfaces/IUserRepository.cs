using JobSearchApi.Models;
using JobSearchApi.Models.DTO;

namespace JobSearchApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Register(RegisterModel model);
        Task Update(ApplicationUser user);
        Task Delete(ApplicationUser user);
        Task FindById(string id);
        Task FindByEmail(string email);
    }
}