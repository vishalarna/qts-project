using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_Procedure_Link;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        /// <summary>
        /// link procedure to safety hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/{id}/procedure")]
        public async Task<IActionResult> LinkProcedure(int id, SafetyHazard_Procedure_LinkOptions options)
        {
            var result = await _saftyHazard.LinkProcedure(id, options);
            var histOptions = new SaftyHazardOptions();
            histOptions.SaftyHazardIds = new int[] { id };
            //foreach (var procId in options.ProcedureIds)
            //{
                histOptions.EffectiveDate = options.EffectiveDate;
                histOptions.ChangeNotes = options.ProcedureIds.Length + " Procedure Linked to Safety Hazard";
            await _safetyHazardHistory.CreateSHHistory(histOptions);
            //}

            return Ok( new { result });
        }

        /// <summary>
        /// Get All Procedures Linked To Safety Hazard along with Linkage count for Procedures
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{id}/procedure/count")]
        public async Task<IActionResult> GetLinkedProcedureWithCount(int id)
        {
            var result = await _saftyHazard.GetLinkedProcedureWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/procedure/{id}")]
        public async Task<IActionResult> getSHLinkedToProcedure(int id)
        {
            var result = await _saftyHazard.getSHLinkedToProcedure(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink procedure from safety hazard
        /// </summary>
        /// <param name="shId"></param>
        /// <param name="procId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/saftyHazards/{shId}/procedure/")]
        public async Task<IActionResult> UnlinkProcedure(int shId, SafetyHazard_Procedure_LinkOptions proc)
        {
            await _saftyHazard.UnlinkProcedure(shId, proc);
            var histOptions = new SaftyHazardOptions();
            histOptions.EffectiveDate = proc.EffectiveDate;
            histOptions.ChangeNotes = proc.ProcedureIds.Length + " Procedure UnLinked to Safety Hazard";
            histOptions.SaftyHazardIds = new int[] { shId };
            await _safetyHazardHistory.CreateSHHistory(histOptions);
            return Ok( new { message = _localizer["ProcedureUnlinked"].Value });
        }
    }
}
