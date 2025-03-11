using System.ComponentModel.DataAnnotations;

namespace JobSearchApi.Models.DTO
{
    public class CompanyViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
    }
}
