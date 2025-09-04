using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Tool;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Links a tool to a specific task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/tools")]
        public async Task<IActionResult> AddToolsAsync(int id, ToolAddOptions options)
        { 

            var tools = await _taskService.AddToolAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = options.ToolIds.Length + " Tools linked To Task";
            histOptions.TaskIds = new int[] { id };
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { tools, message = _localizer["TaskToolAdded"] });
        }

        /// <summary>
        /// Get all the tools for a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{taskId}/tools")]
        public async Task<IActionResult> GetToolsAsync(int taskId)
        {
            var result = await _taskService.GetToolsAsync(taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Save the updated tools for task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/{taskId}/tools")]
        public async Task<IActionResult> UpdateTools(int taskId,TaskOptions options)
        {
            await _taskService.UpdateToolsAsync(taskId, options);

            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = options.ToolIds.Length + " Tools Updated for Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { _message = _localizer["TaskToolsUpdated"] });
        }

        /// <summary>
        /// Unlink Multiple Tools from task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/tools")]
        public async Task<IActionResult> RemoveTools(int taskId, TaskOptions options)
        {
            await _taskService.RemoveTools(taskId, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = options.ToolIds.Length + " Tools Unlinked From Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { _message = _localizer["ToolsUnlinked"] });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/tool")]
        public async Task<IActionResult> GetMetaToolsData(int id)
        {
            var result = await _taskService.GetMetaToolsDataAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks a tool from a specific task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="toolId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/tools/{toolId}")]
        public async Task<IActionResult> RemoveToolsAsync(int taskId, int toolId, TaskOptions options)
        {
            await _taskService.RemoveToolAsync(taskId, toolId);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = options.ToolIds.Length + " Tools Removed From Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["ToolsRemovedFromTask"] });
        }
    }
}
