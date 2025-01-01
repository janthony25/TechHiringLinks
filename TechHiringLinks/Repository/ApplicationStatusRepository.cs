using Microsoft.EntityFrameworkCore;
using TechHiringLinks.Data;
using TechHiringLinks.Models;
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

        public async Task AddStatusAsync(AddApplicationStatusDto dto)
        {
            try
            {

                if (string.IsNullOrEmpty(dto.ApplicationStatusName))
                {
                    _logger.LogError("Invalid input: ApplicationStatusName is null or empty.");
                    throw new ArgumentNullException(nameof(dto.ApplicationStatusName), "ApplicationStatusName cannot be null or empty");
                }

                var application = new ApplicationStatus
                {
                    ApplicationStatusName = dto.ApplicationStatusName
                };


                _dataContext.ApplicationStatus.Add(application);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding new status.");
                throw;
            }
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
