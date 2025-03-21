﻿using Microsoft.AspNetCore.Identity;

namespace JobSearchApi.Models
{
    public class VacancyDetails
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string SubmissionType { get; set; }
        public DateTime ResponseDate { get; set; }
        public string CompanyResponse { get; set; }
        public string Notes { get; set; }


        public string VacancyListId { get; set; }
        public VacancyList VacancyList { get; set; }
    }
}
