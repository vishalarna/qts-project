using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RR_Procedure_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Link Procedure to The Regulatory Requirement whose id is given
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/{id}/procedure")]
        public async Task<IActionResult> LinkProcedure(int id, RR_Procedure_LinkOptions options)
        {
            var result = await _regulatoryRequirementService.LinkProcedure(id, options);

            //foreach (var item in options.ProcedureIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = System.DateTime.Now,
                    ChangeNotes =options.ProcedureIds.Length + " Procedure Linked to RR",
                    RegulatoryRequirementId = id,
                });
           // }

            return Ok( new { result });
        }

        /// <summary>
        /// Remove RR_Procedure_Link for given id's
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/rr/{id}/procedure")]
        public async Task<IActionResult> UnlinkProcedure(int id, RR_Procedure_LinkOptions options)
        {
            await _regulatoryRequirementService.UnlinkProcedure(id, options);
            //foreach (var item in options.ProcedureIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = options.EffectiveDate,
                    ChangeNotes = options.ProcedureIds.Length + " Procedure UnLinked to RR",
                    RegulatoryRequirementId = id,
                });
            //}
            return Ok( new { message = _localizer["RR_Procedure_LinkCreated"].Value });
        }

        /// <summary>
        /// Get all Procedures linked to particular Regulatory requirement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/{id}/procedure")]
        public async Task<IActionResult> GetLinkedProcedure(int id)
        {
            var result = await _regulatoryRequirementService.GetProcedureLinkedToRR(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get regulatory requirements that  Procedure  is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/procedure/{id}")]
        public async Task<IActionResult> GetRRLinkedWithProcedures(int id)
        {
            var result = await _regulatoryRequirementService.GetRRLinkedWithProcedure(id);
            return Ok( new { result });
        }

    }
}
