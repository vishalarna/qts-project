using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Instructor;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.Instance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceInstructorController : ControllerBase
    {
        private readonly IInstanceFetcher _instanceFetcher;
        private readonly IStringLocalizer<InstanceClientUserController> _localizer;
        private readonly IInstructorService _instructorService;
        private readonly IDatabaseResolver _databaseResolver;

        public InstanceInstructorController(IInstanceFetcher instanceFetcher,
           IStringLocalizer<InstanceClientUserController> localizer,
           IInstructorService instructorService,
           IDatabaseResolver databaseResolver)
        {
            _instanceFetcher = instanceFetcher;
            _localizer = localizer;
            _instructorService = instructorService;
            _databaseResolver = databaseResolver;
        }

        [HttpPost]
        [Route("/instance/{instanceName}/instructors")]
        public async Task<IActionResult> CreateAsync(string instanceName, Instructor_CreateOptions options)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var instructor = await _instructorService.CreateAsync(options, true);
                return Ok(new { instructor, message = _localizer["instructorCreated"] });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/instructors/{id}")]
        public async Task<IActionResult> UpdateAsync(string instanceName, int id, Instructor_CreateOptions options)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var instructor = await _instructorService.UpdateAsync(id, options, true);
                return Ok( new { instructor, message = _localizer["instructorUpdated"] });
            }
            catch (ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/instructors/byEmail")]
        public async Task<IActionResult> UpdateByEmailAsync(string instanceName, Instructor_UpdateByEmailOptions options)
        {
            try
            {
                var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
                _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
                var instructor = await _instructorService.UpdateByEmailAsync(options);
                return Ok( new { instructor, message = _localizer["instructorUpdated"] });
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPut]
        [Route("/instance/{instanceName}/instructors/{id}/activate")]
        public async Task<IActionResult> ActivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            await _instructorService.ActivateAsync(id);
            return Ok( new { message = _localizer["instructorActivated"] });
        }

        [HttpPut]
        [Route("/instance/{instanceName}/instructors/{id}/deactivate")]
        public async Task<IActionResult> DeactivateAsync(string instanceName, int id)
        {
            var instanceSettings = await _instanceFetcher.GetInstanceSettingAsync(instanceName);
            _databaseResolver.SetConnectionString(instanceSettings.DatabaseName);
            var instructor = await _instructorService.DeactivateAsync(id);
            return Ok( new { instructor, message = _localizer["instructorDeactivated"] });
        }

    }
}
