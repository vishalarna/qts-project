using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_TrainingGroup;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get all training Groups linked to task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/trainingGroups")]
        public async Task<IActionResult> GetLinkedTrainingGroupsAsync(int id)
        {
            var result = await _taskService.GetLinkedTrainingGroups(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Link Training Groups to task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/{id}/trainingGroups")]
        public async Task<IActionResult> LinkTrainingGroups(int id, Task_TrainingGroupOptions options)
        {
            await _taskService.LinkTrainingGroupsAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.EffectiveDate = DateTime.UtcNow;
            
            histOptions.ChangeNotes = options.TrainingGroupIds.Length + " Training Groups Linked to Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);


            return Ok( new { message = _localizer["TrainingGroupsLinked"] });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/tg")]
        public async Task<IActionResult> GetMetaTaskTrainingGroups(int id)
        {
            var result = await _taskService.GetMetaTaskTrainingGroups(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlink Training Groups
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{id}/trainingGroups")]
        public async Task<IActionResult> UnlinkTrainingGroupsAsync(int id, Task_TrainingGroupOptions options)
        {
            await _taskService.UnlinkTrainingGroupsAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.EffectiveDate = DateTime.UtcNow;
            
            histOptions.ChangeNotes = options.TrainingGroupIds.Length + " Training Groups Unlinked From Task";
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);


            return Ok( new { message = _localizer["TrainingGroupsUnlinked"] });
        }
    }
}
