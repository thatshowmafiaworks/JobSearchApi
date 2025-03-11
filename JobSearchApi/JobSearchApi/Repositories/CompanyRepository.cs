using JobSearchApi.Data;
using JobSearchApi.Models;
using JobSearchApi.Models.DTO;
using JobSearchApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobSearchApi.Repositories
{
    public class CompanyRepository(AppDbContext dbContext, ILogger<CompanyRepository> logger) : ICompanyRepository
    {
        public async Task<string> Create(CompanyModel model)
        {
            var company = new Company
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Website = model.Website
            };
            try
            {
                await dbContext.Companies.AddAsync(company);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Created new company with Id:{company.Id}\tName:{company.Name}");
                return company.Id;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception in CompanyRepository: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var company = await FindById(id);
            if (company is null)
            {
                logger.LogError($"Not found company with id:{id}");
                return false;
            }

            dbContext.Companies.Remove(company);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Deleted company with id:{id}\t name:{company.Name}");
            return true;
        }

        public async Task<Company> FindById(string id)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company is null)
            {
                logger.LogError($"Not found company with id:{id}");
                return null;
            }
            return company;
        }

        public async Task<List<CompanyViewModel>> GetAll()
        {
            var companies = await dbContext.Companies.ToListAsync();
            if (companies != null && companies.Count > 0)
            {
                var result = companies.Select(x => new CompanyViewModel { Id = x.Id, Name = x.Name, Website = x.Website }).ToList();
                return result;
            }
            return null;
        }

        public async Task<bool> Update(string id, CompanyModel companyModel)
        {
            var company = await FindById(id);

            if (company is null)
            {
                return false;
            }

            company.Name = companyModel.Name;
            company.Website = companyModel.Website;
            dbContext.Companies.Update(company);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
