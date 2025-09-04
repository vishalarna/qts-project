using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SegmentObjective_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SegmentController : ControllerBase
    {
        /// <summary>
        /// Links the SegmentObjective with specific Segment
        /// </summary>
        /// <returns>Http Response Code with Segments</returns>
        [HttpPost]
        [Route("/segments/{id}/objective")]
        public async Task<IActionResult> LinkSegmentObjectiveAsync(int id, UpdateSegmentObjectiveOrderListVM options)
        {
            await _segmentService.LinkObjective(id, options);
            return Ok();
        }

        /// <summary>
        /// Unlinks the SegmentObjective with specific Segment
        /// </summary>
        /// <returns>Http Response Code with Segments</returns>
        [HttpDelete]
        [Route("/segments/{id}/objective")]
        public async Task<IActionResult> UnlinkSegmentObjectiveAsync(int id, SegmentObjective_LinkOptions options)
        {
            var ilaSeg = await _iLASegmentLinkService.GetBySegmentId(id);
            await _segmentService.UnlinkObjective(id, options);
            await _iLAService.ReorderObjectiveLinks(ilaSeg.ILAId);
            return Ok( new { message = _localizer["SegmentObjectivesUnlinkedFromSegment"].Value });
        }

        /// <summary>
        /// Get All Objectives linked to segment
        /// </summary>
        /// <returns>Response object with all the objectives linked to segment
        [HttpGet]
        [Route("/segments/{id}/objective")]
        public async Task<IActionResult> GetLinkedSegmentObjectives(int id)
        {
            var result = await _segmentService.GetLinkedObjectives(id);
            return Ok( new { result });
        }

    }
}
