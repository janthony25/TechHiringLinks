using System.ComponentModel.DataAnnotations;

namespace TechHiringLinks.Models
{
    public class ApplicationStatus
    {
        [Key]
        public int ApplicationStatusId { get; set; }
        public required string ApplicationStatusName { get; set; }   

    }
}
