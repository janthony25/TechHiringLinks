using TechHiringLinks.Models.Dto;

namespace TechHiringLinks.Repository.IRepository
{
    public interface IApplicationStatusRepository
    {
        Task<List<ApplicationStatusListDto>> GetApplicationStatusListAsync();
        Task AddStatusAsync(AddApplicationStatusDto dto);
    }
}
