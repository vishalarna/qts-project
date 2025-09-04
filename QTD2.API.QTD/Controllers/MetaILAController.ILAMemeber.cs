using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Meta_ILAMembers_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class MetaILAController : ControllerBase
    {
        /// <summary>
        /// Links the ILAMemeber with specific MetaILA
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with MetaILAs</returns>
        [HttpPost]
        [Route("/metailas/{id}/link")]

        public async Task<IActionResult> LinkILAMemebersAsync(Meta_ILAMembers_ListOptions options)
        {
            var result = await _metaIlaService.LinkILAMemeberAsync(options);
            return Ok(new {result});
        }

        /// <summary>
        /// Unlinks the ILAMemeber with specific MetaILA
        /// </summary>
        /// <returns>Http Response Code with MetaILAs</returns>
        [HttpDelete]
        [Route("/metailas/{id}/ilamemeber/{linkId}")]
        public async Task<IActionResult> UnlinkILAMemebersAsync(int id, int linkId)
        {
            await _metaIlaService.UnlinkILAMemeberAsync(id, linkId);
            return Ok( new { message = _localizer["ILAMemeberUnlinkedFromMetaILA"].Value });
        }

        /// <summary>
        /// Get the NERCAudience linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked  NERCAudience</returns>
        [HttpGet]
        [Route("/metailas/ilamemeber")]
        public async Task<IActionResult> GetLinkedILAMemebersAsync()
        {
            var result = await _metaIlaService.GetLinkedILAAsync();
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/metailas/ilamember")]
        public async Task<IActionResult> UpdateILAMembersAsync(Meta_ILAMembers_LinkOptions options)
        {
            var result = await _metaIlaService.UpdateILAMembersLinkAsync(options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/metailas/{id}/ilamemeber/{linkedId}/remove")]
        public async Task<IActionResult> RemoveILAMemebersAsync(int id, int linkedId)
        {
            await _metaIlaService.RemoveILAMemeberAsync(id, linkedId);
            return Ok( new { message = _localizer["ILAMemeberRemovedFromMetaILA"].Value });
        }

        [HttpGet]
        [Route("/metailas/{id}/employee/{empId}/idp/ilamember")]

        public async Task<IActionResult> GetLinkedILAMembersForIDPAsync(int id,int empId)
        {
            var result = await _metaIlaService.GetLinkedMetaILAsMembersByMetaILAIdForIDPAsync(id,empId);
            return Ok(new { result });
        }
    }
}
