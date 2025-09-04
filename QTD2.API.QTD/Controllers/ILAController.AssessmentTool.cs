using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_AssessmentTool_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the AssessmentTool with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/assessmentTool")]
        public async Task<IActionResult> LinkAssessmentToolAsync(int id, ILA_AssessmentTool_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "An Assessment Tool link created", DateTime.Now,2);
            var result = await _ilaService.LinkAssessmentToolAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the AssessmentTool with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/assessmentTool/{linkId}")]
        public async Task<IActionResult> UnlinkAssessmentToolAsync(int id, int linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "An Assessment Tool link removed", DateTime.Now,0);
            await _ilaService.UnlinkAssessmentToolAsync(id, linkId);
            return Ok( new { message = _localizer["AssessmentToolsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the AssessmentTools linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked AssessmentTools</returns>
        [HttpGet]
        [Route("/ila/{id}/assessmentTool")]
        public async Task<IActionResult> GetLinkedAssessmentToolAsync(int id)
        {
            var result = await _ilaService.GetLinkedAssessmentToolsAsync(id);
            return Ok( new { result });
        }
    }
}
