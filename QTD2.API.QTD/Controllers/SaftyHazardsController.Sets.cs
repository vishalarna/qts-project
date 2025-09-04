using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_Set;
using QTD2.Infrastructure.Model.SafetyHazard_Set_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpPost]
        [Route("/saftyHazards/{id}/set")]
        public async Task<IActionResult> LinkSafetyHazardSet(int id, SafetyHazard_Set_LinkOptions options)
        {
            var result = await _saftyHazard.LinkSet(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/{shId}/set/{shSetId}")]
        public async Task<IActionResult> UnlinkSafetyHazardSet(int shId, int shSetId)
        {
            await _saftyHazard.UnlinkSet(shId, shSetId);
            return Ok( new { message = _localizer["SetUnlinked"].Value });
        }
    }
}
