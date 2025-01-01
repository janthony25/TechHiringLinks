using TechHiringLinks.Models.Dto;

namespace TechHiringLinks.Repository.IRepository
{
    public interface IApplicationRepository
    {
        Task<List<GetApplicationsDto>> GetApplicationListAsync();
    }
}
