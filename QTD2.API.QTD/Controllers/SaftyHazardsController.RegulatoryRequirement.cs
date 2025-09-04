using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SaftyHazard_RR_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {

        /// <summary>
        /// Link Regulatory Requiremment to Safety Hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/{id}/rr")]
        public async Task<IActionResult> LinkIla(int id, SaftyHazard_RR_LinkOptions options)
        {
            var result = await _saftyHazard.LinkRR(id, options);
            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.RegulatoryRequirementIds.Length + "RR linked to Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SafetyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All RR Linked To SH with count
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{id}/rr/count")]
        public async Task<IActionResult> GetLinkedRRWithCountAsync(int id)
        {
            var result = await _saftyHazard.getLinkedRRWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/rr/{id}")]
        public async Task<IActionResult> getSHLinkedToRR(int id)
        {
            var result = await _saftyHazard.getSHLinkedToRR(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink Regulatory Requirements From Safety Hazard
        /// </summary>
        /// <param name="shId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/saftyHazards/{shId}/rr")]
        public async Task<IActionResult> UnlinkRegRequirement(int shId, SaftyHazard_RR_LinkOptions options)
        {
            await _saftyHazard.UnlinkRR(shId, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.RegulatoryRequirementIds.Length + "RR UnLinked from Safety Hazard"; ;
            hist.SaftyHazardIds = new int[] { shId };
            await _safetyHazardHistory.CreateSHHistory(hist);
            return Ok( new { message = _localizer["RegulatoryRequirementUnlinked"].Value });
        }
    }
}
