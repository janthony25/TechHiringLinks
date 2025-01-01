using Microsoft.EntityFrameworkCore;
using TechHiringLinks.Data;
using TechHiringLinks.Models.Dto;
using TechHiringLinks.Repository.IRepository;

namespace TechHiringLinks.Repository
{
    public class ApplicationStatusRepository : IApplicationStatusRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ApplicationStatusRepository> _logger;

        public ApplicationStatusRepository(DataContext dataContext, ILogger<ApplicationStatusRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public async Task<List<ApplicationStatusListDto>> GetApplicationStatusListAsync()
        {
            try
            {
                var applicationStatusList = await _dataContext.ApplicationStatus
                                    .Select(appStatus => new ApplicationStatusListDto
                                    {
                                        ApplicationStatusId = appStatus.ApplicationStatusId,
                                        ApplicationStatusName = appStatus.ApplicationStatusName
                                    }).ToListAsync();

                return applicationStatusList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving application status link.");
                throw;
            }
        }
    }
}
