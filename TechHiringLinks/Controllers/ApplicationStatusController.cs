﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechHiringLinks.Models.Dto;
using TechHiringLinks.Repository.IRepository;

namespace TechHiringLinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IApplicationStatusRepository _statusRepository;

        public ApplicationStatusController(ILogger<ApplicationController> logger,
                                     IApplicationStatusRepository statusRepository)
        {
            _logger = logger;
            _statusRepository = statusRepository;
        }

        [HttpGet("StatusList")]
        public async Task<IActionResult> GetStatusList()
        {
            try
            {
                _logger.LogInformation("Request to retrieve status list.");

                var statusList = await _statusRepository.GetApplicationStatusListAsync();
                return Ok(statusList);

            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStatus(AddApplicationStatusDto dto)
        {
            try
            {
                await _statusRepository.AddStatusAsync(dto);
                return Ok("Status Added successfully.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "invalid input.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding new status.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateApplicationStatusDto dto)
        {
            try
            {
                await _statusRepository.UpdateStatusAsync(id, dto);
                return Ok("StatusName successfully updated.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"StatusName with id {id} not found.");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating status with id {id}");
                return StatusCode(500, "An error occurred while updating status.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            try
            {
                _logger.LogInformation($"Request to delete status with id {id}");

                await _statusRepository.DeleteStatusAsync(id);
                return Ok($"Status with id {id} successfully deleted.");

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError($"Status with id {id} not found.");
                return NotFound(new {message = ex.Message});    
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting status with id {id}.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
