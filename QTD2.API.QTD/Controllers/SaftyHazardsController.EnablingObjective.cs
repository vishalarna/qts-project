using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_EO_Link;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpPost]
        [Route("/saftyHazards/{id}/eo")]
        public async Task<IActionResult> LinkEO(int id, SafetyHazard_EO_LinkOptions options)
        {
            var result = await _saftyHazard.LinkEO(id, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.EOIDs.Length + " EO linked to Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SafetyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);

            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/{shId}/eo")]
        public async Task<IActionResult> UnlinkEO(int shId, SafetyHazard_EO_LinkOptions options)
        {
            await _saftyHazard.UnlinkEO(shId, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.EOIDs.Length + " EO Unlinked from Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SafetyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);

            return Ok( new { message = _localizer["EOUnlinked"].Value });
        }

        /// <summary>
        /// Get the EOs linked to the Safety Hazard given by id along with the number of links for that eo
        /// </summary>
        /// <param name="shId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{shId}/eo/count")]
        public async Task<IActionResult> GetEOLinksWithCount(int shId)
        {
            var result = await _saftyHazard.GetLinkedEOsWithCount(shId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/eo/{id}")]
        public async Task<IActionResult> getSHLinkedToEO(int id)
        {
            var result = await _saftyHazard.getSHLinkedToEO(id);
            return Ok( new { result });
        }
    }
}
