using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_Tool_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        [HttpPost]
        [Route("/saftyHazards/{id}/tool")]
        public async Task<IActionResult> LinkTools(int id, SafetyHazard_Tool_LinkOptions options)
        {
            var result = await _saftyHazard.LinkPPETool(id, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/saftyHazards/{shId}/tool")]
        public async Task<IActionResult> UnlinkTools(int shId, SafetyHazard_Tool_LinkOptions options)
        {
            await _saftyHazard.UnlinkPPETool(shId, options);
            return Ok( new { message = _localizer["ILAUnlinked"].Value });
        }
    }
}
