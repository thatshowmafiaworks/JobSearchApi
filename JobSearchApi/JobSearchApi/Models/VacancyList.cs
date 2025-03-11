namespace JobSearchApi.Models
{
    public class VacancyList
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<VacancyDetails> Vacancies { get; set; }
    }
}
