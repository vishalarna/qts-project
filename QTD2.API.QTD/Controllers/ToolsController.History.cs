using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Tool_StatusHistory;

namespace QTD2.API.QTD.Controllers
{
    public partial class ToolsController : ControllerBase
    { /// <summary>
      /// Gets a list of Tool_StatusHistories
      /// </summary>
      /// <returns>Http Response Code with Tool_StatusHistories</returns>
        [HttpGet]
        [Route("/tools/history")]
        public async Task<IActionResult> GetTool_StatusHistorysAsync()
        {
            var result = await _tool_statusHistoryService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a list of Tool_StatusHistories
        /// </summary>
        /// <returns>Http Response Code with Tool_StatusHistories</returns>
        [HttpGet]
        [Route("/tools/history/all")]
        public async Task<IActionResult> GetAllTool_StatusHistorysAsync()
        {
            var result = await _tool_statusHistoryService.GetAllToolHistories();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Tool_StatusHistory
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/tools/history")]
        public async Task<IActionResult> CreateTool_StatusHistoryAsync(Tool_StatusHistoryCreateOptions options)
        {
            await _tool_statusHistoryService.CreateAsync(options);
            return Ok( new { message = _localizer["Tool_StatusHistoryCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Tool_StatusHistory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Tool_StatusHistory</returns>
        [HttpGet]
        [Route("/tools/history/{id}")]
        public async Task<IActionResult> GetTool_StatusHistoryAsync(int id)
        {
            var result = await _tool_statusHistoryService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Tool_StatusHistory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tools/history/{id}")]
        public async Task<IActionResult> UpdateTool_StatusHistoryAsync(int id, Tool_StatusHistoryUpdateOptions options)
        {
            var result = await _tool_statusHistoryService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["Tool_StatusHistoryUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Tool_StatusHistory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tools/history/{id}")]
        public async Task<IActionResult> DeleteTool_StatusHistoryAsync(int id, Tool_StatusHistoryOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _tool_statusHistoryService.InActiveAsync(id);
                    break;
                case "active":
                    await _tool_statusHistoryService.ActiveAsync(id);
                    break;
                case "delete":
                    await _tool_statusHistoryService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"Tool_StatusHistory-{options.ActionType.ToLower()}"].Value });
        }
    }
}
