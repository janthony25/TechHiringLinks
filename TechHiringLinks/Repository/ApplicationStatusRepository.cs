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

        public async Task DeleteStatusAsync(int id)
        {
            try
            {
                var status = await _dataContext.ApplicationStatus.FindAsync(id);

                if (status == null || status.ApplicationStatusId == 0)
                {
                    _logger.LogError("Status not found.");
                    throw new KeyNotFoundException($"Status with id {id} not found.");
                }

                _dataContext.ApplicationStatus.Remove(status);
                _logger.LogInformation($"Status with id {id} was deleted successfully.");
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting ApplicationStatus.");
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

        public async Task UpdateStatusAsync(int id, UpdateApplicationStatusDto dto)
        {
            try
            {
                var status = await _dataContext.ApplicationStatus.FindAsync(id);

                if (status == null || status.ApplicationStatusId == 0)
                {
                    _logger.LogError("Status not found.");
                    throw new KeyNotFoundException("Status not found.");
                }

                status.ApplicationStatusName = dto.ApplicationStatusName;

                _logger.LogInformation("StatusName successfully updated.");
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating status.");
                throw;
            }
        }
    }
}
