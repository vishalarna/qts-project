using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_RR_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Link Regulatory Requirement with task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/{id}/rr")]
        public async Task<IActionResult> LinkRR(int id, TaskOptions options)
        {
            await _taskService.LinkRR(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            
            histOptions.ChangeNotes = options.RegulatoryRequirementIds.Length + " Regulations Linked To Task";
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["Regulations Linked to Task"] });
        }

        /// <summary>
        /// Unlink Regulatory Requirement from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/rr")]
        public async Task<IActionResult> UnlinkRR(int taskId, TaskOptions options)
        {
            //var rrLinks = await _taskService.GetTaskRRLinks(taskId);
            //  await _versioningService.VersionTask_RR_LinkAsync(rrLinks);
            await _taskService.UnlinkRR(taskId, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { taskId };
            
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["RegulatoryRequirementUnlinked"].Value });
        }


        /// <summary>
        /// Unlink Regulatory Requirement from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/rr/count")]
        public async Task<IActionResult> GetLinkedRRWithCount(int id)
        {
            var result = await _taskService.GetLinkedRRWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/rr/allcount")]
        public async Task<IActionResult> GetLinkedRRWithMetaEO(int metaId)
        {
            var result = await _taskService.GetLinkedRRWithMetaTaskAsync(metaId);
            return Ok( new { result });
        }


        /// <summary>
        /// Unlink Regulatory Requirement from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/rr")]
        public async Task<IActionResult> GetLinkedRR(int id)
        {
            var result = await _taskService.GetTaskLinkedToRR(id);
            return Ok( new { result });
        }
    }
}
