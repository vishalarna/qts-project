using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Procedure_ILA_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Link the ILA with Procedure whose Id is provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http Response code along with linked data </returns>
        [HttpPost]
        [Route("/procedures/{id}/ila")]
        public async Task<IActionResult> LinkILA(int id, Procedure_ILA_LinkCreateOptions options)
        {
            var result = await _procedureService.LinkILA(id, options);
            //foreach (var item in options.ILAIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { id }, false, true, options.ILAIds.Length + " ILA Linked to Procedure", System.DateTime.Now));
            //}
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedures/{id}/ila")]
        public async Task<IActionResult> GetLinkedILAs(int id)
        {
            var result = await _procedureService.GetLinkedILAs(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink specific ILAs linked to procedure provided by procId
        /// </summary>
        /// <param name="procId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/procedures/{procId}/ila/")]
        public async Task<IActionResult> UnlinkILA(int procId, Procedure_ILA_LinkCreateOptions options)
        {
            await _procedureService.UnlinkILA(procId, options.ILAIds);
            //foreach (var item in options.ILAIds)
            //{
                await _procedureStatusHistoryService.CreateAsync(new QTD2.Infrastructure.Model.Procedure_StatusHistory.Procedure_StatusHistoryCreateOptions(new int[] { procId }, false, true, options.ILAIds.Length + " ILA UnLinked from Procedure", System.DateTime.Now));
            //}
            return Ok( new { message = _stringLocalizer["ILAUnlinked"].Value });
        }

        [HttpGet]
        [Route("/procedures/provider/ila")]
        public async Task<IActionResult> GetproviderWithILAs()
        {
            var result = await _procedureService.GetProviderWithILAs();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/procedures/topic/ila")]
        public async Task<IActionResult> GettopicWithILAs()
        {
            var result = await _procedureService.GetTopicWithILAs();
            return Ok( new { result });
        }

        /// <summary>
        /// Get all procedures that the task is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/ila/{id}")]
        public async Task<IActionResult> GetProcILAIsLinkedTo(int id)
        {
            var result = await _procedureService.GetProcILAIsLinkedTo(id);
            return Ok( new { result });
        }
    }
}
