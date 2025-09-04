using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Tool;

namespace QTD2.API.QTD.Controllers
{
    public partial class ToolGroupsController : ControllerBase
    {
        /// <summary>
        /// Adds a tool to a tool group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/toolGroups/{id}/tools")]
        public async Task<IActionResult> AddToolAsync(int id, ToolAddOptions options)
        {
            var tool = await _toolService.AddTooltoToolGroupAsync(id, options);
            return Ok( new { tool, message = _lcoalizer["toolAddedToTG"] });
        }

        /// <summary>
        /// Removes a tool from a tool group
        /// </summary>
        /// <param name="toolGroupId"></param>
        /// <param name="toolId"></param>
        /// <returns>Http response code with data</returns>
        [HttpDelete]
        [Route("/toolGroups/{toolGroupId}/tools/{toolId}")]
        public async Task<IActionResult> RemoveToolAsync(int toolGroupId, int toolId)
        {
            await _toolService.RemoveToolFromToolGroupAsync(toolGroupId, toolId);
            return Ok( new { message = _lcoalizer["toolRemovedFromToolGroup"] });
        }
    }
}
