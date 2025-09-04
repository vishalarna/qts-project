using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILACollaborator;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the Collaborator with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/collaborator")]
        public async Task<IActionResult> LinkCollaboratorAsync(int id, ILACollaboratorOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.CollaboratorInviteIds.Length.ToString() + " Collaborator Links Created", DateTime.Now,1);
            var result = await _ilaService.LinkCollaboratorAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the Collaborator with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/collaborator/")]
        public async Task<IActionResult> UnlinkCollaboratorAsync(int id, ILACollaboratorOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.CollaboratorInviteIds.Length.ToString() + " Collaborator Links Removed", DateTime.Now,2);
            await _ilaService.UnlinkCollaboratorAsync(id, options);
            return Ok( new { message = _localizer["CollaboratorsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the Collaborators linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked Collaborators</returns>
        [HttpGet]
        [Route("/ila/{id}/collaborator")]
        public async Task<IActionResult> GetLinkedCollaboratorAsync(int id)
        {
            var result = await _ilaService.GetCollaboratorsAsync(id);
            return Ok( new { result });
        }
    }
}
