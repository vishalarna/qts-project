using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ILA_Segment_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class ILAController : ControllerBase
    {
        /// <summary>
        /// Links the Segment with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpPost]
        [Route("/ila/{id}/segment")]
        public async Task<IActionResult> LinkSegmentAsync(int id, ILA_Segment_LinkOptions options)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Segment link Created", DateTime.Now, 1);
            var result = await _ilaService.LinkSegmentAsync(id, options);
            await _ilaService.ReorderObjectiveLinks(id);
            return Ok();
        }
        [HttpPost]
        [Route("/ila/{ilaId}/segment/reorder")]
        public async Task<IActionResult> ReorderSegmentAsync(int ilaId, SegmentReorderOptions options)
        {
            foreach (var item in options.segmentIds)
            {
                await _ilaService.UnlinkSegmentAsync(ilaId, item);
                var result = await _ilaService.LinkSegmentAsync(ilaId, new ILA_Segment_LinkOptions
                {
                    ILAId = ilaId,
                    SegmentId=item
                });

            }
            await _ilaService.ReorderObjectiveLinks(ilaId);
            return Ok( new { message="Segments Reordered Successfully" });
        }

        /// <summary>
        /// Unlinks the Segment with specific ILA
        /// </summary>
        /// <returns>Http Response Code with ILAs</returns>
        [HttpDelete]
        [Route("/ila/{id}/segment/{linkId}")]
        public async Task<IActionResult> UnlinkSegmentAsync(int id, int linkId)
        {
            var ila = await _ilaService.GetAsync(id);
            await _versioningService.VersionILAAsync(ila, "Segment link Removed", DateTime.Now, 0);
            await _ilaService.UnlinkSegmentAsync(id, linkId);
            return Ok( new { message = _localizer["SegmentsUnlinkedFromILA"].Value });
        }

        /// <summary>
        /// Get the Segments linked with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked Segments</returns>
        [HttpGet]
        [Route("/ila/{id}/segment")]
        public async Task<IActionResult> GetLinkedSegmentAsync(int id)
        {
            var result = await _ilaService.GetLinkedSegmentsAsync(id);
            return Ok( new { result });
        }
    }
}
