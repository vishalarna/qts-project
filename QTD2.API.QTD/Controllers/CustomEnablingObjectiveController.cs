using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.CustomEnablingObjective;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomEnablingObjectiveController : ControllerBase
    {
        private readonly ICustomEnablingObjectiveService _customEOService;
        private readonly IStringLocalizer<CustomEnablingObjectiveController> _localizer;

        public CustomEnablingObjectiveController(ICustomEnablingObjectiveService customEOService, IStringLocalizer<CustomEnablingObjectiveController> localizer)
        {
            _customEOService = customEOService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of CustomEnablingObjectives
        /// </summary>
        /// <returns>Http Response Code with CustomEnablingObjectives</returns>
        [HttpGet]
        [Route("/customeo")]
        public async Task<IActionResult> GetCustomEnablingObjectivesAsync()
        {
            var result = await _customEOService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new CustomEnablingObjective
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/customeo")]
        public async Task<IActionResult> CreateCustomEnablingObjectiveAsync(CustomEnablingObjectiveCreateOptions options)
        {
            var result = await _customEOService.CreateAsync(options);
            return Ok( new { message = _localizer["CustomEnablingObjectiveCreated"].Value, result = result });
        }

        /// <summary>
        /// Gets a specific CustomEnablingObjective by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with CustomEnablingObjective</returns>
        [HttpGet]
        [Route("/customeo/{id}")]
        public async Task<IActionResult> GetCustomEnablingObjectiveAsync(int id)
        {
            var result = await _customEOService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific CustomEnablingObjective by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/customeo/{id}")]
        public async Task<IActionResult> UpdateCustomEnablingObjectiveAsync(int id, CustomEnablingObjectiveUpdateOptions options)
        {
            var result = await _customEOService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["CustomEnablingObjectiveUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific CustomEnablingObjective by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/customeo/{id}")]
        public async Task<IActionResult> DeleteCustomEnablingObjectiveAsync(int id, CustomEnablingObjectiveOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _customEOService.InActiveAsync(id);
                    break;
                case "active":
                    await _customEOService.ActiveAsync(id);
                    break;
                case "delete":
                    await _customEOService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"CustomEnablingObjective-{options.ActionType.ToLower()}"].Value });
        }
    }
}
