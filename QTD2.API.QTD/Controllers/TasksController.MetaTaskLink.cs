using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// link meta task/s to task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/{id}/meta")]
        public async Task<IActionResult> LinkMetaTask(int id, TaskOptions options)
        {
            var result = await _taskService.LinkMetaTask(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            
            histOptions.ChangeNotes = options.TaskIds.Length + " Tasks Linked To Meta Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink meta task/s from task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{id}/meta")]
        public async Task<IActionResult> UnlinkMetaTask(int id, TaskOptions options)
        {
            await _taskService.UnlinkMetaTask(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);


            return Ok( new { message = _localizer["MetaTasksUnlinked"] });
        }

        /// <summary>
        /// Get All Task linked with Meta Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/meta/{id}")]
        public async Task<IActionResult> GetLinkedMetaTask(int id)
        {
            var metaTaskVM = await _taskService.GetLinkedMetaTasks(id);

            return Ok( new { metaTaskVM });
        }

    }
}
