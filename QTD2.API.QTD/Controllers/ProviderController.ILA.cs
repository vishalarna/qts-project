using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProviderController : ControllerBase
    {
        /// <summary>
        /// Gets a list of Providers with all ILAs
        /// </summary>
        /// <returns>Http Response Code with providers and ILAs</returns>
        [HttpGet]
        [Route("/providers/ilas")]
        public async Task<IActionResult> GetProvidersWithILAAsync()
        {
            var providers = await _providerService.GetWithILAAsync();
            return Ok( new { providers });
        }

        /// <summary>
        /// Gets a list of Providers with all ILAs
        /// </summary>
        /// <returns>Http Response Code with providers and ILAs</returns>
        [HttpGet]
        [Route("/providers/ilascount")]
        public async Task<IActionResult> GetProvidersWithILACountAsync()
        {
            var providers = await _providerService.GetWithILACountAsync();
            return Ok( new { providers });
        }

        [HttpPost]
        [Route("/providers/ilascount/withFilter")]
        public async Task<IActionResult> GetProviderWithFilterAndILACount(FilterByOptions filterOptions)
        {
            var result = await _providerService.GetProviderWithFilterAndILACount(filterOptions);
            return Ok( new { result });
        }
    }
}
