using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_RegRequirement_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the RegulatoryRequirement with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/regRequirement")]
        public async Task<IActionResult> LinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.RegulatoryRequirementIds.Length.ToString() + " Regulatory Requirement(s) Linked", DateTime.Now, 1);
            await _ilaService.LinkRegulatoryRequirementAsync(id, options);
            return Ok(new { message = _localizer["RegulatoryRequirementsLinkedFromILA"].Value });
        }

        /// <summary>
        /// Unlinks the RegulatoryRequirement with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/regRequirement")]
        public async Task<IActionResult> UnlinkRegulatoryRequirementAsync(int id, ILA_RegRequirement_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, options.RegulatoryRequirementIds.Length.ToString() + " Regulatory Requirement(s) Unlinked", DateTime.Now, 0);
            await _ilaService.UnlinkRegulatoryRequirementAsync(id, options);
            return Ok( new { message = _localizer["RegulatoryRequirementsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the RegulatoryRequirements linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked RegulatoryRequirements</returns>
        [HttpGet]
        [Route("/ila/{id}/regRequirement")]
        public async Task<IActionResult> GetLinkedRegulatoryRequirementAsync(int id)
        {
            var result = await _ilaService.GetLinkedRegulatoryRequirementsAsync(id);
            return Ok( new { result });
        }
    }
}
