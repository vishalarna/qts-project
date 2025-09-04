using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ToolGroup;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ToolGroupsController : ControllerBase
    {
        private readonly IToolService _toolService;
        private readonly IStringLocalizer<ToolGroupsController> _lcoalizer;

        public ToolGroupsController(IToolService toolService, IStringLocalizer<ToolGroupsController> lcoalizer)
        {
            _toolService = toolService;
            _lcoalizer = lcoalizer;
        }

        /// <summary>
        /// Gets a list of tool groups
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/toolGroups")]
        public async Task<IActionResult> GetAsync()
        {
            var tgList = await _toolService.GetToolGroupsAsync();
            return Ok( new { tgList });
        }

        /// <summary>
        /// Gets a tool group
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/toolGroups/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tg = await _toolService.GetToolGroupAsync(id);
            return Ok( new { tg });
        }

        /// <summary>
        ///  Create a tool group
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/toolGroups")]
        public async Task<IActionResult> CreateAsync(ToolGroupCreateOptions options)
        {
            var tg = await _toolService.CreateToolGroupAsync(options);
            return Ok( new { tg, message = _lcoalizer["TGCreated"] });
        }

        /// <summary>
        /// Deletes a tool group
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/toolGroups/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _toolService.DeleteAsync(id);
            return Ok( new { message = _lcoalizer["toolGroupDeleted"] });
        }
    }
}
