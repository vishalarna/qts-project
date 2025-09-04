using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Version_Task;

namespace QTD2.API.QTD.Controllers
{

    public partial class TasksController
    {
        /// <summary>
        /// Gets 
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/history/{getTrimmed}")]
        public async Task<IActionResult> GetLatestActivity(bool getTrimmed)
        {
            var history = await _historyService.GetLatestActivity(getTrimmed);
            return Ok( new { history });
        }


        /// <summary>
        /// Get specific task history by taskId
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/history")]
        public async Task<IActionResult> GetTaskHistory(int id)
        {
            var history = await _historyService.GetLatestActivity(id);
            return Ok( new { history });
        }

        /// <summary>
        /// Get specific task history by taskId
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/version")]
        public async Task<IActionResult> GetAllVersions(int id)
        {
            var result = await _historyService.GetTaskVersions(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/tasks/{taskId}/history/{histId}")]
        public async Task<IActionResult> RestoreTaskHistory(int taskId, int histId)
        {
            await _historyService.RestoreHistory(taskId, histId);
            return Ok( new { message = _localizer["TaskRestored"] });
        }

    }
}
