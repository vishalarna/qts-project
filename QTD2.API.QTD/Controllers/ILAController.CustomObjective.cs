using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILACustomObjective_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the CustomObjective with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/customeo")]
        public async Task<IActionResult> LinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.CustomObjIds.Length.ToString() + " Custom Objectives Links Created", DateTime.Now, 2);
            var result = await _ilaService.LinkCustomObjectiveAsync(id, options);
            await _ilaService.ReorderObjectiveLinks(id);
            return Ok( new { message = _localizer["CustomObjectiveslinkedFromILA"].Value });
        }

        /// <summary>
        /// Unlinks the CustomObjective with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/customeo")]
        public async Task<IActionResult> UnlinkCustomObjectiveAsync(int id, ILACustomObjective_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.CustomObjIds.Length.ToString() + " Custom Objectives Links Removed", DateTime.Now, 0);
            await _ilaService.UnlinkCustomObjectiveAsync(id, options);
            return Ok( new { message = _localizer["CustomObjectivesUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the CustomObjectives linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked CustomObjectives</returns>
        [HttpGet]
        [Route("/ila/{id}/customeo")]
        public async Task<IActionResult> GetLinkedCustomObjectiveAsync(int id)
        {
            var result = await _ilaService.GetLinkedCustomObjectivesAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/ila/{id}/customeo/withnum")]
        public async Task<IActionResult> GetLinkedCustomEOWithNumber(int id)
        {
            var result = await _ilaService.GetLinkedCustomEOWithNumberAsync(id);
            return Ok( new { result });
        }
    }
}
