using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RegRequirement_EO_Link;
using QTD2.Infrastructure.Model.RR_EO_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Link RR To Enabling Objective
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/{id}/eo")]
        public async Task<IActionResult> LinkEO(int id, RegRequirement_EO_LinkOptions options)
        {
            var result = await _regulatoryRequirementService.LinkEnablingObjective(id, options);
            //foreach (var item in options.EOIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = System.DateTime.Now,
                    ChangeNotes =options.EOIds.Length + "RR Linked to EO",
                    RegulatoryRequirementId = id,
                });
            //}

            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/rr/{rrId}/eo")]
        public async Task<IActionResult> UnlinkEO(int rrId, RR_EO_LinkOptions options)
        {
            await _regulatoryRequirementService.UnlinkEnablingObjective(rrId, options);
            return Ok( new { message = _localizer["EnablingObjectiveUnlinked"].Value });
        }

        [HttpGet]
        [Route("/rr/{id}/eo")]
        public async Task<IActionResult> GetLinkedEnablingObjectivessAsync(int id)
        {
            var result = await _regulatoryRequirementService.GetEnablingObjectiveLinkedToRR(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get the EO linked to the Regulatory Requirement given by id along with the number of links for that EO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{id}/eo/count")]
        public async Task<IActionResult> GetEOLinksWithCount(int id)
        {
            var result = await _regulatoryRequirementService.GetLinkedEOsWithCount(id);
            return Ok( new { result });
        }
    }
}
