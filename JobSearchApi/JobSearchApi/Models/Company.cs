namespace JobSearchApi.Models
{
    public class Company
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Website { get; set; }
        public ICollection<VacancyDetails> Vacancies { get;set; }
    }
}
