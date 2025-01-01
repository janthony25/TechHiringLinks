using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TechHiringLinks.Data;
using TechHiringLinks.Models.Dto;
using TechHiringLinks.Repository.IRepository;

namespace TechHiringLinks.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ApplicationRepository> _logger;    

        public ApplicationRepository(DataContext dataContext, ILogger<ApplicationRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public async Task<List<GetApplicationsDto>> GetApplicationListAsync()
        {
            try
            {
                var applicationList = await _dataContext.Links
                                .Select(app => new GetApplicationsDto
                                {
                                    LinkId = app.LinkId,
                                    WebsiteLink = app.WebsiteLink,
                                    CompanyName = app.CompanyName,
                                    Position = app.Position,
                                    Location = app.Location,
                                    Status = app.Status,
                                    DateSubmitted = app.DateSubmitted
                                }).ToListAsync();

                _logger.LogInformation("Successfully retrieved application list.");
                return applicationList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving application list.");
                throw;
            }
        }
    }
}
