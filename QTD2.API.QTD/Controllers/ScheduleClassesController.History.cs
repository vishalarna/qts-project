using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ScheduleClassesController : ControllerBase
    {

        [HttpGet]
        [Route("/schedules/latestActivity")]
        public async Task<IActionResult> GetLatestActivity()
        {
            var result = await _classScheduleHistoryService.GetLatestActivityAsync();
            return Ok(new { result });
        }
    }
}
