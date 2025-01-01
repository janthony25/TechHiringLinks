namespace TechHiringLinks.Models
{
    public class LinkApplicationStatus
    {
        public int LinkId { get; set; }
        public Link Link { get; set; }
        public int ApplicationStatusId { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
