using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Procedure_RegRequirement_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Link Regulatory Reuirement with Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/procedures/{id}/rr")]
        public async Task<IActionResult> LinkRR(int id, Procedure_RR_LinkOptions options)
        {
            var result = await _procedureService.LinkRR(id, options);
            //foreach (var item in options.RegulatoryRequirementIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { id }, false, true, options.RegulatoryRequirementIds.Length + " RR Linked to Procedure", System.DateTime.Now));
            //}

            return Ok( new { result });
        }

        /// <summary>
        /// Get all Regulatory requirements linked to particular Procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/{id}/rr")]
        public async Task<IActionResult> GetLinkedRR(int id)
        {
            var result = await _procedureService.GetLinkedRR(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Procedures that regulatory requirement is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/rr/{id}")]
        public async Task<IActionResult> GetProceduresRRIsLinkedTo(int id)
        {
            var result = await _procedureService.GetProceduresRRIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink Regulatory Requirements from Procedure
        /// </summary>
        /// <param name="procId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/procedures/{procId}/rr")]
        public async Task<IActionResult> UnlinkRR(int procId, Procedure_RR_LinkOptions options)
        {
            await _procedureService.UnlinkRR(procId, options);
            //foreach (var item in options.RegulatoryRequirementIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { procId }, false, true, options.RegulatoryRequirementIds.Length + " RR UnLinked from Procedure", System.DateTime.Now));
            //}
            return Ok( new { message = _stringLocalizer["RegulatoryRequirementUnlinked"].Value });
        }
    }
}
