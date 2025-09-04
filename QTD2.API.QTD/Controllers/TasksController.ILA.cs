using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_ILA_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Link ila to Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/{id}/ila")]
        public async Task<IActionResult> LinkILA(int id, TaskOptions options)
        {
            await _taskService.LinkILA(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            
            histOptions.ChangeNotes = options.ILAIds.Length + " ILAs Linked To Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);


            return Ok( new { message = _localizer["ILAs Linked to Task"] });
        }

        /// <summary>
        /// Get all ILA Linked to task along with Count of Links ILA has with tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/ila/count")]
        public async Task<IActionResult> GetLinkedILAWithCount(int id)
        {
            var result = await _taskService.GetLinkedILAWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/ila/allcount")]
        public async Task<IActionResult> GetLinkedILAToMetaTaskWithCount(int metaId)
        {
            var result = await _taskService.GetLinkedILAToMetaTaskWithCountAsync(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All Tasks that ila is linked to
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/ila/{id}")]
        public async Task<IActionResult> GetTasksILAIsLinkedTo(int id)
        {
            var result = await _taskService.GetTasksILAIsLinkedWith(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink ila from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/ila")]
        public async Task<IActionResult> UnlinkILA(int taskId, TaskOptions options)
        {
            await _taskService.UnlinkILA(taskId, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["ILAUnlinked"].Value });
        }
    }
}
