using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RR_SafetyHazard_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Link Safety Hazard to the Regulatory Requirement
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/{id}/safetyHazard")]
        public async Task<IActionResult> LinkSafetyHazard(int id, RR_SafetyHazard_LinkOptions options)
        {
            var result = await _regulatoryRequirementService.LinkSafetyHazardAsync(id, options);

            //foreach (var item in options.SafetyHazardIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = System.DateTime.Now,
                    ChangeNotes =options.SafetyHazardIds.Length +  " SH Linked to RR",
                    RegulatoryRequirementId = id,
                });
            //}

            return Ok( new { result });
        }

        /// <summary>
        /// Get All the Safety Linked to Regulatory Requirement along with count of the safety hazard link
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{id}/safetyHazard")]
        public async Task<IActionResult> GetLinkedSh(int id)
        {
            var result = await _regulatoryRequirementService.GetSafetyHazardLinkedToRRWithCount(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All Regulatory Requirements That Safety Hazard is Linked To
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/safetyHazard/{id}")]
        public async Task<IActionResult> getRRLinkedToSH(int id)
        {
            var result = await _regulatoryRequirementService.getRRLinkedToSH(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink Safety Hazard linked to The regulatory Requirement
        /// </summary>
        /// <param name="rrId"></param>
        /// <param name="safetyHazards"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/rr/{rrId}/safetyHazard")]
        public async Task<IActionResult> UnlinkSafetyHazard(int rrId, RR_SafetyHazard_LinkOptions safetyHazards)
        {
            await _regulatoryRequirementService.UnlinkSafetyHazardAsync(rrId, safetyHazards);
            var result = await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions(rrId, true, false, safetyHazards.EffectiveDate, safetyHazards.SafetyHazardIds.Length + "SH UnLinked from RR"));
            return Ok( new { message = _localizer["SafetyHazardUnlinked"].Value });
        }
    }
}
