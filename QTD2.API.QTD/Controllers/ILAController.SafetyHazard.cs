using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.ILA_SafetyHazard_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the SafetyHazard with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/safetyHazard")]
        public async Task<IActionResult> LinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.SafetyHazardIds.Length.ToString() + " Safety Hazard(s) linked", DateTime.Now, 1);
            await _ilaService.LinkSafetyHazardAsync(id, options);
            return Ok(new { message = _localizer["SafetyHazardsLinkedFromILA"].Value });
        }

        /// <summary>
        /// Unlinks the SafetyHazard with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/safetyHazard")]
        public async Task<IActionResult> UnlinkSafetyHazardAsync(int id, ILA_SafetyHazard_LinkOptions linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, linkId.SafetyHazardIds.Length.ToString() + " Safety Hazard(s) Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkSafetyHazardAsync(id, linkId);
            return Ok( new { message = _localizer["SafetyHazardsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the SafetyHazards linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked SafetyHazards</returns>
        [HttpGet]
        [Route("/ila/{id}/safetyHazard")]
        public async Task<IActionResult> GetLinkedSafetyHazardAsync(int id)
        {
            var result = await _ilaService.GetLinkedSafetyHazardsAsync(id);
            return Ok( new { result });
        }
    }
}
