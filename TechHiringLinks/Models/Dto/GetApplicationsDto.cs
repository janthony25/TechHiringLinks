﻿namespace TechHiringLinks.Models.Dto
{
    public class GetApplicationsDto
    {
        public int LinkId { get; set; }
        public required string WebsiteLink { get; set; }
        public required string CompanyName { get; set; }
        public required string Position { get; set; }
        public required string Location { get; set; }
        public string? Status { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public required string ApplicationStatusName { get; set; }
    }
}
