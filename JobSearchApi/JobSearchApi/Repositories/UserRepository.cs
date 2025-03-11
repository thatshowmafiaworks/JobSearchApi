using JobSearchApi.Models;
using JobSearchApi.Models.DTO;
using JobSearchApi.Repositories.Interfaces;

namespace JobSearchApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task Delete(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
