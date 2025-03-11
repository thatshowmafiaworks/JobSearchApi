using JobSearchApi.Data;
using JobSearchApi.Models.DTO;
using JobSearchApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchApi.Controllers
{
    [Route("company")]
    public class CompanyController(
        ICompanyRepository companyRepository,
        AppDbContext dbContext,
        ILogger<CompanyController> logger
        ) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Create([FromBody] CompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "wrong request" });
            }
            var result = await companyRepository.Create(model);
            if (string.IsNullOrEmpty(result))
            {
                return StatusCode(500, new { error = "something went wrong on server side" });
            }
            return Ok(new { result = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { error = "id is empty" });
            }
            var company = await companyRepository.FindById(id);
            if (company is null)
            {
                return BadRequest(new { error = $"not found company with id: {id}" });
            }
            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await companyRepository.GetAll();
            if (companies is null)
            {
                return Ok(new { message = "There is no companies yet" });
            }
            return Ok(new { companies = companies });
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] string id, [FromBody] CompanyModel model)
        {
            throw new NotImplementedException("Update not implemented");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            throw new NotImplementedException("Delete not implemented");

        }
    }
}
