using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.SafetyHazard_Task_Link;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SH_StatusHistory;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class SaftyHazardsController : ControllerBase
    {
        /// <summary>
        /// Create A link between specified task and Safety Hazard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/saftyHazards/{id}/task")]
        public async Task<IActionResult> LinkTask(int id, SafetyHazard_Task_LinkOptions options)
        {
            var result = await _saftyHazard.LinkTask(id, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes =options.TaskIds.Length +  " Task linked to Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SaftyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);
            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Safety Hazard Linked to Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }
            return Ok(new { result });
        }

        /// <summary>
        /// Unlink task specified by taskId from safty Hazard specified by shId
        /// </summary>
        /// <param name="shId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/saftyHazards/{shId}/task/")]
        public async Task<IActionResult> UnlinkTask(int shId, SafetyHazard_Task_LinkOptions options)
        {
            await _saftyHazard.UnlinkTask(shId, options);

            var hist = new SaftyHazardOptions();
            hist.EffectiveDate = options.EffectiveDate;
            hist.ChangeNotes = options.TaskIds.Length + " Task Unlinked to Safety Hazard";
            hist.SaftyHazardIds = new int[] { options.SaftyHazardId };
            await _safetyHazardHistory.CreateSHHistory(hist);
            foreach (var taskId in options.TaskIds)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                histOptions.TaskIds = new int[] { taskId };
                histOptions.ChangeNotes = "1 Safety Hazard Unlinked From Task";
                var task = await _taskService.GetAsync(taskId);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _task_histService.SaveHistoryAsync(histOptions);
            }
            return Ok(new { message = _localizer["TaskUnlinked"].Value });
        }

        /// <summary>
        /// Get the Tasks linked to the Safety Hazard given by id along with the number of links for that task
        /// </summary>
        /// <param name="shId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/saftyHazards/{shId}/task/count")]
        public async Task<IActionResult> GetTaskLinksWithCount(int shId)
        {
            var result = await _saftyHazard.GetLinkedTasksWithCount(shId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/saftyHazards/task/{id}")]
        public async Task<IActionResult> getSHLinkedToTask(int id)
        {
            var result = await _saftyHazard.getSHLinkedToTask(id);
            return Ok(new { result });
        }
    }
}
