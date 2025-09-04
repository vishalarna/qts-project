using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.DutyArea_History;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class DutyAreaController : ControllerBase
    {
        /// <summary>
        /// Get all Duty Area History
        /// </summary>
        /// <returns> Http response code with list of  Duty Area History </returns>
        [HttpGet]
        [Route("/dutyAreaHistory")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _historyService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get specific Duty Area History by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Http response code with Duty Area History data </returns>
        [HttpGet]
        [Route("/dutyAreaHistory/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _historyService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Create Issuing authority status history
        /// </summary>
        /// <param name="options"></param>
        /// <returns> Http Response code along with data of created Issuing authority </returns>
        [HttpPost]
        [Route("/dutyAreaHistory")]
        public async Task<IActionResult> Create(DutyArea_HistoryCreateOptions options)
        {
            var result = await _historyService.CreateAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Update The specific issuing authority status history by id
        /// </summary>
        ///  /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http response code along with updated data </returns>
        [HttpPut]
        [Route("/dutyAreaHistory/{id}")]
        public async Task<IActionResult> Update(int id, DutyArea_HistoryCreateOptions options)
        {
            var result = await _historyService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Delete the specific Issuing authority history
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns> Http response code along with delete option information message </returns>
        [HttpDelete]
        [Route("/dutyAreaHistory/{id}")]
        public async Task<IActionResult> Delete(int id, DutyArea_HistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _historyService.DeactivateAsync(id);
                    break;
                case "active":
                    await _historyService.ActiveAsync(id);
                    break;
                case "delete":
                    await _historyService.DeleteAsync(id);
                    break;
            }
            return Ok( new { message = _localization[$"DutyAreaHistory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
