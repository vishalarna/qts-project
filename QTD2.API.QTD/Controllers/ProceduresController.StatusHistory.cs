using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Procedure_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ProceduresController : ControllerBase
    {
        /// <summary>
        /// Creates procedure history
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/procedures/history")]
        public async Task<IActionResult> StoreHistory(Procedure_StatusHistoryCreateOptions options)
        {
            await _procedureService.StoreStatusHistoryAsync(options);
            return Ok( new { message = "ProcedureStatusHistoryCreated" });
        }

        /// <summary>
        /// Gets procedure history by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/procedures/history/latest/{getLatest}")]
        public async Task<IActionResult> GetHistory(bool getLatest)
        {
            var history = await _procedureService.GetHistoryAsync(getLatest);
            return Ok( new { history });
        }
    }
}
