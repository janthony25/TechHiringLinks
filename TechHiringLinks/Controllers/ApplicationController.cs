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

        public ApplicationController(IApplicationRepository applicationRepository, ILogger<ApplicationController> logger)
        {
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        [HttpGet]
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
