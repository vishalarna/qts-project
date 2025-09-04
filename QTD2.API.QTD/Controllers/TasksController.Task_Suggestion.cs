using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_Suggestion;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Create new Task Suggestion
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/tasks/{taskId}/suggestion")]
        public async Task<IActionResult> CreateTaskSuggestionAsync(int taskId, Task_SuggestionOptions options)
        {
            var result = await _taskService.CreateTaskSuggestionAsync(taskId, options);

            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "New Suggestion Created For Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { result });
        }

        /// <summary>
        /// Get All Task Suggestions
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{taskId}/suggestion")]
        public async Task<IActionResult> GetAllSuggestionsAsync(int taskId)
        {
            var result = await _taskService.GetAllSuggestionsAsync(taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Suggestion Number
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{taskId}/suggestion/number")]
        public async Task<IActionResult> GetSuggestionNumber(int taskId)
        {
            var result = await _taskService.GetSuggestionNumberAsync(taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Update the Suggestion data for this task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="suggestionId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/{taskId}/suggestion/{suggestionId}")]
        public async Task<IActionResult> UpdateSuggestionAsync(int taskId, int suggestionId, Task_SuggestionOptions options)
        {
            await _taskService.UpdateSuggestionAsync(taskId, suggestionId, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "Suggestion Updated For Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["TaskSuggestionUpdated"] });
        }

        /// <summary>
        /// Update Order of suggestions
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/suggestion")]
        public async Task<IActionResult> UpdateSuggestionOrder(Task_SuggestionNumberOptions options)
        {
            await _taskService.UpdateSuggestionNumbers(options);
            return Ok( new { message = _localizer["SuggestionsOrderUpdated"] });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/suggestion")]
        public async Task<IActionResult> GetMetaTaskSuggestions(int id)
        {
            var result = await _taskService.GetMetaTaskSuggestionsAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Delete the Task Specific suggestion.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="suggestionId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/suggestion/{suggestionId}")]
        public async Task<IActionResult> DeleteSuggestionAsync(int taskId, int suggestionId, TaskOptions options)
        {
            switch (options.ActionType.Trim().ToLower())
            {
                case "active":
                    break;
                case "inactive":
                    break;
                case "delete":
                    await _taskService.DeleteSuggestionAsync(taskId, suggestionId);
                    break;
            }
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["TaskSuggestionDeleted"] });
        }
    }
}
