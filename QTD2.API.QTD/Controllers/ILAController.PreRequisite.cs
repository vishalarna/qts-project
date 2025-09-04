using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_PreRequisite_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the PreRequisite with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/preReq")]
        public async Task<IActionResult> LinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.PreRequisiteIds.Length.ToString() + " PreRequisite(s) Linked", DateTime.Now, 1);
            await _ilaService.LinkPreRequisiteAsync(id, options);
            return Ok(new { message = _localizer["PreRequisitesLinkedFromILA"].Value });
        }

        [HttpGet]
        [Route("/ila/{id}/preReq/data")]
        public async Task<IActionResult> GetPreReqData(int id)
        {
            var result = await _ilaService.GetPreReqDataAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the PreRequisite with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/preReq")]
        public async Task<IActionResult> UnlinkPreRequisiteAsync(int id, ILA_PreRequisite_LinkOptions linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, linkId.PreRequisiteIds.Length.ToString() + " PreRequisite(s) Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkPreRequisiteAsync(id, linkId);
            return Ok( new { message = _localizer["PreRequisitesUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the PreRequisites linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked PreRequisites</returns>
        [HttpGet]
        [Route("/ila/{id}/preReq")]
        public async Task<IActionResult> GetLinkedPreRequisiteAsync(int id)
        {
            var result = await _ilaService.GetLinkedPreRequisitesAsync(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/ila/{id}/prerequisites")]
        public async Task<IActionResult> UpdatePreRequisitesAsync(int id, ILAPrerequisitesOptions options)
        {
            await _ilaService.UpdatePreRequisitesAsync(id, options);
            return Ok( new { message = _localizer["ILAPrerequisitesUpdated"] });
        }

        [HttpGet]
        [Route("/ila/{id}/prerequisites")]
        public async Task<IActionResult> GetPreRequisitesAsync(int id)
        {
            var result = await _ilaService.GetPreRequisitesAsync(id);
            return Ok( new { result });
        }
    }
}
