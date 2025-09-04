using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.Task_History;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Create Link Between Task and EO and also save history
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/task")]
        public async Task<IActionResult> LinkTasks(EO_LinkOptions options)
        {
            await _enablingObjectiveService.LinkTaskAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();

            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.ChangeEffectiveDate = DateTime.Now;
            histOptions.OldStatus = false;
            histOptions.NewStatus = true;
            histOptions.ChangeNotes = options.TaskIds.Length + " Tasks Linked To EO.";
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);

            foreach (var taskId in options.TaskIds)
            {
                var taskHistOptions = new Task_HistoryOptions();
                taskHistOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                taskHistOptions.TaskIds = new int[] { taskId };

                taskHistOptions.ChangeNotes = "1 Enabling Objective Linked To Task";
                var task = await _taskService.GetAsync(taskId);
                var taskVersion = await _ver_taskService.VersionAndCreateCopy(task, 1);
                taskHistOptions.Version_TaskId = taskVersion.Id;
                await _task_histService.SaveHistoryAsync(taskHistOptions);
            }

            return Ok( new { message = _localizer["TasksLinkedToEO"] });
        }

        /// <summary>
        /// Get tasks linked with EO
        /// </summary>
        /// <param name="eoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{eoId}/task/count")]
        public async Task<IActionResult> GetLinkedTasksWithCount(int eoId)
        {
            var result = await _enablingObjectiveService.GetLinkedTasksWithCountAsync(eoId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/enablingObjectives/{metaId}/task/allcount")]
        public async Task<IActionResult> GetLinkedTaskWithMetaEOs(int metaId)
        {
            var result = await _enablingObjectiveService.GetLinkedTaskWithMetaEOs(metaId);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink tasks from EOs
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/enablingObjectives/{id}/task")]
        public async Task<IActionResult> UnlinkTasks(EO_LinkOptions options)
        {
            await _enablingObjectiveService.UnlinkTasksAsync(options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EnablingObjectiveId = options.EOId;
            histOptions.NewStatus = false;
            histOptions.OldStatus = true;
            var eo = await _enablingObjectiveService.GetAsync(options.EOId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);


            foreach (var taskId in options.TaskIds)
            {
                var taskHistOptions = new Task_HistoryOptions();
                taskHistOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                taskHistOptions.TaskIds = new int[] { taskId };

                taskHistOptions.ChangeNotes = "1 Enabling Objective Unlinked From Task";
                var task = await _taskService.GetAsync(taskId);
                var taskVersion = await _ver_taskService.VersionAndCreateCopy(task, 1);
                taskHistOptions.Version_TaskId = taskVersion.Id;
                await _task_histService.SaveHistoryAsync(taskHistOptions);
            }
            return Ok( new { message = _localizer["TasksUnlinkedFromEO"] });
        }
    }
}
