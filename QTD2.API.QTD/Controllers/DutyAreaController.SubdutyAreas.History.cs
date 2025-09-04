using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SubdutyArea;
using QTD2.Infrastructure.Model.SubDutyArea_History;

namespace QTD2.API.QTD.Controllers
{

    public partial class DutyAreaController : ControllerBase
    {
        /// <summary>
        /// Get all Duty Area History
        /// </summary>
        /// <returns> Http response code with list of  Duty Area History </returns>
        [HttpGet]
        [Route("/dutyAreas/subdutyArea/history")]
        public async Task<IActionResult> GetSubDutyAreaHistoryAll()
        {
            var result = await _subDutyArea_HistoryService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific Duty Area History by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Http response code with Duty Area History data </returns>
        [HttpGet]
        [Route("/dutyAreas/subdutyArea/history/{id}")]
        public async Task<IActionResult> GetSubDutyAreaHistoryByID(int id)
        {
            var result = await _subDutyArea_HistoryService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Issuing authority status history
        /// </summary>
        /// <param name="options"></param>
        /// <returns> Http Response code along with data of created Issuing authority </returns>
        [HttpPost]
        [Route("/dutyAreas/subdutyArea/history")]
        public async Task<IActionResult> CreateSubDutyAreaHistory(SubDutyArea_HistoryCreateOptions options)
        {
            var result = await _subDutyArea_HistoryService.CreateAsync(options);
            return Ok( new { result });
        }

    }
}
