using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Provider;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        private readonly IStringLocalizer<ProviderController> _localizer;

        public ProviderController(IProviderService providerService, IStringLocalizer<ProviderController> localizer)
        {
            _providerService = providerService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Providers
        /// </summary>
        /// <returns>Http Response Code with providers</returns>
        [HttpGet]
        [Route("/providers")]
        public async Task<IActionResult> GetProvidersAsync()
        {
            var providers = await _providerService.GetAsync();
            return Ok( new { providers });
        }

        [HttpGet]
        [Route("/providers/withoutIncludes")]
        public async Task<IActionResult> GetProviderWithoutIncludesAsync()
        {
            var result = await _providerService.GetProviderWithoutIncludes();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of active Providers
        /// </summary>
        /// <returns>Http Response Code with providers</returns>
        [HttpGet]
        [Route("/providers/active")]
        public async Task<IActionResult> GetActiveProvidersAsync()
        {
            var providers = await _providerService.GetActiveProvidersAsync();
            return Ok( new { providers });
        }

        /// <summary>
        /// Creates a new Provider
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/providers")]
        public async Task<IActionResult> CreateProviderAsync(ProviderCreateOptions options)
        {
            var result = await _providerService.CreateAsync(options);
            return Ok( new { message = _localizer["ProviderCreated"].Value });
        }

        /// <summary>
        /// Gets a specific provider by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Provider</returns>
        [HttpGet]
        [Route("/providers/{id}")]
        public async Task<IActionResult> GetProviderAsync(int id)
        {
            var provider = await _providerService.GetAsync(id);
            return Ok( new { provider });
        }
        
        [HttpGet]
        [Route("/providers/{id}/onlyprov")]
        public async Task<IActionResult> GetOnlyProviderAsync(int id)
        {
            var result = await _providerService.GetOnlyProviderAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific provider by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/providers/{id}")]
        public async Task<IActionResult> UpdateProviderAsync(int id, ProviderUpdateOptions options)
        {
            var provider = await _providerService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["providerUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific provider by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/providers/{id}")]
        public async Task<IActionResult> DeleteProviderAsync(int id, ProviderOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _providerService.InActiveAsync(id);
                    break;
                case "active":
                    await _providerService.ActiveAsync(id);
                    break;
                case "delete":
                    await _providerService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"provider-{options.ActionType.ToLower()}"].Value });
        }
    }
}
