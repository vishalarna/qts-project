using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ProviderLevel;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderLevelController : ControllerBase
    {
        private readonly IProviderLevelService _providerLevelService;
        private readonly IStringLocalizer<ProviderLevelController> _localizer;

        public ProviderLevelController(IProviderLevelService providerLevelService, IStringLocalizer<ProviderLevelController> localizer)
        {
            _providerLevelService = providerLevelService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Provider Levels
        /// </summary>
        /// <returns>Http Response Code with Provider Levels</returns>
        [HttpGet]
        [Route("/providerlevels")]
        public async Task<IActionResult> GetProviderLevelsAsync()
        {
            var providers = await _providerLevelService.GetAsync();
            return Ok( new { providers });
        }

        /// <summary>
        /// Creates a new Provider Level
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/providerlevels")]
        public async Task<IActionResult> CreateProviderLevelAsync(ProviderLevelCreateOptions options)
        {
            var result = await _providerLevelService.CreateAsync(options);
            return Ok( new { message = _localizer["providerLevelCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Provider Level by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Provider</returns>
        [HttpGet]
        [Route("/providerlevels/{id}")]
        public async Task<IActionResult> GetProviderLevelAsync(int id)
        {
            var provider = await _providerLevelService.GetAsync(id);
            return Ok( new { provider });
        }

        /// <summary>
        /// Updates  a specific Provider Level by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/providerlevels/{id}")]
        public async Task<IActionResult> UpdateProviderLevelAsync(int id, ProviderLevelUpdateOptions options)
        {
            var provider = await _providerLevelService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["providerLevelUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Provider Level by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/providerlevels/{id}")]
        public async Task<IActionResult> DeleteProviderLevelAsync(int id, ProviderLevelOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _providerLevelService.InActiveAsync(id);
                    break;
                case "active":
                    await _providerLevelService.ActiveAsync(id);
                    break;
                case "delete":
                    await _providerLevelService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"providerlevel-{options.ActionType.ToLower()}"].Value });
        }
    }
}
