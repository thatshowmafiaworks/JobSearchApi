using JobSearchApi.Models;
using JobSearchApi.Models.DTO;

namespace JobSearchApi.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> FindById(string id);
        Task<string> Create(CompanyModel model);
        Task<string> Update(CompanyModel model);
        Task<string> Delete(string id);
        Task<List<CompanyViewModel>> GetAll();
    }
}