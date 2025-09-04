using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_NercStandard_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the NercStandard with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/nercstandard")]
        public async Task<IActionResult> LinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila, "Nerc Standard Linked", DateTime.Now, 1);
            await _ilaService.LinkNercStandardAsync(id, options);
            return Ok(new { message = _localizer["NercStandardsLinkedFromILA"].Value });
        }

        /// <summary>
        /// Unlinks the NercStandard with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/nercstandard")]
        public async Task<IActionResult> UnlinkNercStandardAsync(int id, ILA_NercStandard_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila, "Nerc Standard Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkNercStandardAsync(id, options);
            return Ok( new { message = _localizer["NercStandardsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the NercStandards linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked NercStandards</returns>
        [HttpGet]
        [Route("/ila/{id}/nercstandard")]
        public async Task<IActionResult> GetLinkedNercStandardAsync(int id)
        {
            var result = await _ilaService.GetLinkedNercStandardAsync(id);
            return Ok( new { result });
        }
    }
}
