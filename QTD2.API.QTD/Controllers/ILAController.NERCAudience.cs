using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.ILA_NERCAudience_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the NERCAudience with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/nercaudience")]

        public async Task<IActionResult> LinkNERCAudienceAsync(int id, ILA_NERCAudience_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(options.ILAId);
            await _versioningService.VersionILAAsync(ila,"Nerc Audience Linked", DateTime.Now, 1);
            var result = await _ilaService.LinkNERCAudienceAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the NERCAudience with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/nercaudience/{linkId}")]
        public async Task<IActionResult> UnlinkNERCAudienceAsync(int id, int linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Nerc Audience Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkNERCAudienceAsync(id, linkId);
            return Ok( new { message = _localizer["NercAudienceUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the NERCAudience linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked  NERCAudience</returns>
        [HttpGet]
        [Route("/ila/{id}/nercaudience")]
        public async Task<IActionResult> GetLinkedNERCAudienceAsync(int id)
        {
            var result = await _ilaService.GetLinkedNERCAudienceAsync(id);
            return Ok( new { result });
        }
    }
}
