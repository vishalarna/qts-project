using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get a list of saftyHazards linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/saftyHazards")]
        public async Task<IActionResult> GetLinkedSaftyHazardAsync(int id)
        {
            var shList = await _taskService.GetLinkedSaftyHazardsAsync(id);
            return Ok( new { shList });
        }

        /// <summary>
        /// Get safety hazards linked to tasks along with their link count with tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/saftyHazards/count")]
        public async Task<IActionResult> GetLinkedSHWithCount(int id)
        {
            var result = await _taskService.GetLinkedSHWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/saftyHazards/allcount")]
        public async Task<IActionResult> GetLinkedSHWithMetaEO(int metaId)
        {
            var result = await _taskService.GetLinkedSHWithMetaTaskAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get all the tasks that safety hazard is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/saftyHazards/{id}")]
        public async Task<IActionResult> GetTasksSHIsLinkedTo(int id)
        {
            var result = await _taskService.GetTaskSHIsLinkedTo(id);
            return Ok( new { result });
        }

        /// <summary>
        ///  Links a saftyHazard to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/saftyHazards")]
        public async Task<IActionResult> LinkSaftyHazardAsync(int id, TaskOptions options)
        {
            await _taskService.LinkSaftyHazardsAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;

            histOptions.ChangeNotes = options.SafetyHazardIds.Length + " Safety Hazards Linked to Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["TaskSHLinked"] });
        }

        /// <summary>
        /// Unlinks an array of Safty Hazards From a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{id}/saftyHazards")]
        public async Task<IActionResult> UnlinkSaftyHazardAsync(int id, TaskOptions options)
        {
            await _taskService.UnlinkSaftyHazardAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.TaskIds = new int[] { id };
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["SaftyHazardUnlinkedFromTask"] });
        }
    }
}
