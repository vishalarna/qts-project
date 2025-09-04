using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.ILA_Procedure_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the Procedure with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/procedure")]
        public async Task<IActionResult> LinkProcedureAsync(int id, ILA_Procedure_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.ProcedureIds.Length.ToString() + " Procedure(s) Linked", DateTime.Now, 1);
            await _ilaService.LinkProcedureAsync(id, options);
            return Ok(new { message = _localizer["ProcedureslinkedFromILA"].Value });
        }

        /// <summary>
        /// Unlinks the Procedure with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/procedure")]
        public async Task<IActionResult> UnlinkProcedureAsync(int id, ILA_Procedure_LinkOptions linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, linkId.ProcedureIds.Length.ToString() + " Procedure(s) Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkProcedureAsync(id, linkId);
            return Ok( new { message = _localizer["ProceduresUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the Procedures linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked Procedures</returns>
        [HttpGet]
        [Route("/ila/{id}/procedure")]
        public async Task<IActionResult> GetLinkedProcedureAsync(int id)
        {
            var result = await _ilaService.GetLinkedProceduresAsync(id);
            return Ok( new { result });
        }
    }
}
