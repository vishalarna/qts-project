using System;
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
        /// Get list of enabling objective of a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/enablingObjectives")]
        public async Task<IActionResult> GetLinkedEnablingObjectivesAsync(int id)
        {
            var tasksEO = await _taskService.GetLinkedEnablingObjectivesAsync(id);
            return Ok( new { tasksEO });
        }

        [HttpGet]
        [Route("/tasks/{id}/enablingObjectives/count")]
        public async Task<IActionResult> GetLinkedEOWIthCount(int id)
        {
            var result = await _taskService.GetLinkedEOWithCount(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/enablingObjectives/allcount")]
        public async Task<IActionResult> GetLinkedEOWithMetaTask(int metaId)
        {
            var result = await _taskService.GetLinkedEOWithMetaTaskAsync(metaId);
            return Ok( new { result });
        }




        /// <summary>
        /// Links an enabling objective to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/enablingObjectives")]
        public async Task<IActionResult> LinkEnablingObjectivesAsync(int id, TaskOptions options)
        {
            var eoList = await _taskService.LinkEnablingObjectiveAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            histOptions.TaskIds = new int[] { id };
            
            histOptions.ChangeNotes = options.EnablingObjectiveIds.Length + " Enabling Objectives Linked to Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);


            return Ok( new { eoList, message = _localizer["TaskEOLinked"] });
        }

        [HttpGet]
        [Route("/tasks/enablingObjectives/{id}")]
        public async Task<IActionResult> GetTasksEOIsLinkedTo(int id)
        {
            var result = await _taskService.GetTasksEOIsLinkedToAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks an enabling objective to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{id}/enablingObjectives")]
        public async Task<IActionResult> UnlinkEnablingObjectivesAsync(int id, TaskOptions options)
        {
            await _taskService.UnlinkEnablingObjectiveAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["EOUnlinkedFromTask"] });
        }
    }
}
