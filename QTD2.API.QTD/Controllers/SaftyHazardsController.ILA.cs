using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_SafetyHazard_Link;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpPost]
        [Route("/saftyHazards/{id}/ila")]
        public async Task<IActionResult> LinkIla(int id, ILASafetyHazardOptions options)
        {
            var result = await _saftyHazard.LinkILA(id, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes =options.ILAIds.Length + " ILA Linked to Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SafetyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);

            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/{id}/ila")]
        public async Task<IActionResult> UnlinkILA(int id, ILASafetyHazardOptions options)
        {
            await _saftyHazard.UnlinkILA(id, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.ILAIds.Length + " ILA UnLinked from Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SafetyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);

            return Ok( new { message = _localizer["ILAUnlinked"].Value });
        }

        /// <summary>
        /// Get the ILAs linked to the Safety Hazard given by id along with the number of links for that ILA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{id}/ila/count")]
        public async Task<IActionResult> GetILALinksWithCount(int id)
        {
            var result = await _saftyHazard.GetLinkedILAsWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/ila/{id}")]
        public async Task<IActionResult> getSHLinkedToILA(int id)
        {
            var result = await _saftyHazard.getSHLinkedToILA(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/provider/ila")]
        public async Task<IActionResult> GetproviderWithILAs()
        {
            var result = await _saftyHazard.GetProviderWithILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/topic/ila")]
        public async Task<IActionResult> GettopicWithILAs()
        {
            var result = await _saftyHazard.GetTopicWithILAs();
            return Ok( new { result });
        }
    }
}
