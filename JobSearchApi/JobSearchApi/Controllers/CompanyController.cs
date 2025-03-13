using JobSearchApi.Data;
using JobSearchApi.Models.DTO;
using JobSearchApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "wrong request" });
            }
            var result = await companyRepository.Create(model);
            if (string.IsNullOrWhiteSpace(result))
            {
                return StatusCode(500, new { error = "something went wrong on server side" });
            }
            return Ok(new { result = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
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

        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] CompanyModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
            {
                logger.LogWarning($"User tried to update company with null id");
                return BadRequest(new { error = "Id is empty" });
            }
            var company = await companyRepository.FindById(model.Id);
            if (company is null)
            {
                logger.LogInformation($"Not found company with id: {model.Id}");
                return BadRequest(new { error = $"Not found company with id: {model.Id}" });
            }
            var result = await companyRepository.Update(model);
            if (string.IsNullOrWhiteSpace(result))
            {
                logger.LogError($"Update company with id:{model.Id} went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Something went wrong, please try later" });

            }
            logger.LogInformation($"Updated company with id: {model.Id}");
            return Ok(new { result = $"Updated company with Id: {result}" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                logger.LogError($"Provided empty id for delete");
                return BadRequest(new { error = "Id is empty" });
            }
            var result = await companyRepository.Delete(id);
            if (string.IsNullOrWhiteSpace(result))
            {
                logger.LogError($"Provided empty id for delete");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"Something went wrong, please try later" });
            }
            logger.LogInformation($"Deleted company with id: {id}");
            return Ok(new { result = result });
        }
    }
}
