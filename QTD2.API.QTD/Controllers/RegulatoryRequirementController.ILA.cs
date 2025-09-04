using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RR_ILA_Link;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Link RR with ILA
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/{id}/ila")]
        public async Task<IActionResult> LinkILA(int id, RR_ILA_LinkOptions options)
        {
            var result = await _regulatoryRequirementService.LinkILA(id, options);
            //foreach (var item in options.IlaIds)
            //{
                await _rr_historyService.CreateAsync(new RR_StatusHistoryCreateOptions
                {
                    ChangeEffectiveDate = System.DateTime.Now,
                    ChangeNotes =options.IlaIds.Length + " ILA Linked to RR",
                    RegulatoryRequirementId = id,
                });
            //}

            return Ok( new { result });
        }

        /// <summary>
        /// Unlink RR From ILA
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/rr/{id}/ila")]
        public async Task<IActionResult> UnlinkIla(int id, RR_ILA_LinkOptions options)
        {
            await _regulatoryRequirementService.UnlinkILA(id, options);
            return Ok( new { message = _localizer["ILAUnlinked"].Value });
        }

        [HttpGet]
        [Route("/rr/provider/ila")]
        public async Task<IActionResult> GetproviderWithILAs()
        {
            var result = await _regulatoryRequirementService.GetProviderWithILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/rr/topic/ila")]
        public async Task<IActionResult> GettopicWithILAs()
        {
            var result = await _regulatoryRequirementService.GetTopicWithILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/rr/{id}/ila")]
        public async Task<IActionResult> GetLinkedILAs(int id)
        {
            var result = await _regulatoryRequirementService.GetLinkedILAs(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all rr that the ILA is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/ila/{id}")]
        public async Task<IActionResult> GetRRILAIsLinkedTo(int id)
        {
            var result = await _regulatoryRequirementService.GetRRILAIsLinkedTo(id);
            return Ok( new { result });
        }
    }
}
