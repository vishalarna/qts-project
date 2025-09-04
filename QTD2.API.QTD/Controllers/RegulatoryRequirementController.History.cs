using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.RR_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class RegulatoryRequirementController : ControllerBase
    {
        /// <summary>
        /// Create a new RR_Status History
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/rr/history")]
        public async Task<IActionResult> Create(RR_StatusHistoryCreateOptions options)
        {
            var result = await _rr_historyService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets regulatory requirement history by 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/rr/history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _regulatoryRequirementService.GetHistoryAsync();
            return Ok( new { history });
        }
    }
}
