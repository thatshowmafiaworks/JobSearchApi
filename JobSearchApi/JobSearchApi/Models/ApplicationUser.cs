using Microsoft.AspNetCore.Identity;

namespace JobSearchApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public VacancyList VacancyList { get; set; }
    }
}
