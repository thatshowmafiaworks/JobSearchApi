using System.ComponentModel.DataAnnotations;

namespace JobSearchApi.Models.DTO
{
    public class CompanyModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Website { get; set; }
    }
}
