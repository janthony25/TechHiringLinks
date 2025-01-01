using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechHiringLinks.Repository.IRepository;

namespace TechHiringLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<ApplicationController> _logger;    
        private readonly IApplicationStatusRepository _statusRepository;

        public ApplicationController(IApplicationRepository applicationRepository, ILogger<ApplicationController> logger, 
                                     IApplicationStatusRepository statusRepository)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
            _statusRepository = statusRepository;
        }

        [HttpGet("ApplicationList")]
        public async Task<IActionResult> GetApplicationList()
        {
            try
            {
                _logger.LogInformation("Request to retrieved application list.");

                var applicationList = await _applicationRepository.GetApplicationListAsync();
                return Ok(applicationList);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occured." });
            }
        }

      
    }
}
