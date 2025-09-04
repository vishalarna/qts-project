using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.PublicClassScheduleRequest;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    
    public class PublicClassScheduleRequestController : ControllerBase
    {
        private readonly IPublicClassScheduleRequestService _publicClassScheduleRequestService;

        public PublicClassScheduleRequestController(IPublicClassScheduleRequestService publicClassScheduleRequestService)
        {
            _publicClassScheduleRequestService = publicClassScheduleRequestService;
        }

        [HttpGet]
        [Route("/publicClassScheduleRequest")]
        public async Task<IActionResult> GetAllPublicClassScheduleRequestsAsync()
        {
            var result = await _publicClassScheduleRequestService.GetAllActiveRequestsAsync();
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/publicClassScheduleRequest/{id}")]
        public async Task<IActionResult> UpdatePublicRequestAsync(int id, [FromBody] ModifyPublicClassScheduleRequestModel options)
        {
            var result = await _publicClassScheduleRequestService.UpdatePublicClassScheduleRequestAsync(id, options);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/publicClassScheduleRequest/stats")]
        public async Task<IActionResult> GetRequestStatsAsync()
        {
            var result = await _publicClassScheduleRequestService.GetPublicRequestStatsAsync();
            return Ok(new { result });
        }
    }
}
