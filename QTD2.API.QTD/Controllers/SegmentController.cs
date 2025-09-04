using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Infrastructure.Model.Segment;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class SegmentController : ControllerBase
    {
        private readonly ISegmentService _segmentService;
        private readonly IILASegmentLinkService _iLASegmentLinkService;
        private readonly IILAService _iLAService;
        private readonly IStringLocalizer<SegmentController> _localizer;

        public SegmentController(ISegmentService segmentService, IStringLocalizer<SegmentController> localizer, IILASegmentLinkService iLASegmentLinkService, IILAService iLAService)
        {
            _segmentService = segmentService;
            _localizer = localizer;
            _iLASegmentLinkService = iLASegmentLinkService;
            _iLAService = iLAService;
        }

        /// <summary>
        /// Gets a list of Segments
        /// </summary>
        /// <returns>Http Response Code with Segments</returns>
        [HttpGet]
        [Route("/segments")]
        public async Task<IActionResult> GetSegmentsAsync()
        {
            var result = await _segmentService.GetAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Creates a new Segment
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/segments")]
        public async Task<IActionResult> CreateSegmentAsync(SegmentCreateOptions options)
        {
            var result = await _segmentService.CreateAsync(options);
            return Ok(new { result });
        }

        /// <summary>
        /// Gets a specific Segment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Segment</returns>
        [HttpGet]
        [Route("/segments/{id}")]
        public async Task<IActionResult> GetSegmentAsync(int id)
        {
            var result = await _segmentService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Return Segment With Objectives
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/segments/{id}/links")]
        public IActionResult GetWithObjectives(int id)
        {
            var result = _segmentService.GetWithObjectives(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Segment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/segments/{id}")]
        public async Task<IActionResult> UpdateSegmentAsync(int id, SegmentUpdateOptions options)
        {
            var result = await _segmentService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["SegmentUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Segment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/segments/{id}")]
        public async Task<IActionResult> DeleteSegmentAsync(int id, SegmentOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _segmentService.InActiveAsync(id);
                    break;
                case "active":
                    await _segmentService.ActiveAsync(id);
                    break;
                case "delete":
                    await _segmentService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Segment-{options.ActionType.ToLower()}"].Value });
        }
    }
}
