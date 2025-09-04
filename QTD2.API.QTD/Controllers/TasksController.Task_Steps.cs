using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_Step;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get List of steps added to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/steps")]
        public async Task<IActionResult> GetStepsAsync(int id)
        {
            var steps = await _taskService.GetTask_StepsAsync(id);
            return Ok( new { steps });
        }

        /// <summary>
        /// Adds a step to the task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/steps")]
        public async Task<IActionResult> CreateStepAsync(int id, Task_StepCreateOptions options)
        {
            var step = await _taskService.CreateStepAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "New Step Created For Task";
            histOptions.TaskIds = new int[] { id };
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { step, message = _localizer["taskStepCreated"] });
        }

        /// <summary>
        /// Get the latest number for the task step (It is already incremented)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/steps/num")]
        public async Task<IActionResult> GetStepNumber(int id)
        {
            var result = await _taskService.GetTaskStepNumber(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates a step on a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stepId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/tasks/{taskId}/steps/{stepId}")]
        public async Task<IActionResult> UpdateStepAsync(int taskId, int stepId, Task_StepUpdateOptions options)
        {
            var step = await _taskService.UpdateStepAsync(taskId, stepId, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "Step Updated For Task";
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { step, message = _localizer["taskStepUpdated"] });
        }

        [HttpPut]
        [Route("/tasks/steps")]
        public async Task<IActionResult> UpdateStepNumbers(Task_StepNumberOptions options)
        {
            await _taskService.UpdateTask_StepNumber(options.Numbers, options.StepIds);
            return Ok( new { message = _localizer["StepNumbersUpdated"] });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/steps")]
        public async Task<IActionResult> GetMetaTaskSteps(int id)
        {
            var result = await _taskService.GetMetaTaskStepsAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Removes a step from a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="stepId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/steps/{stepId}")]
        public async Task<IActionResult> DeleteStepAsync(int taskId, int stepId, TaskOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _taskService.DeactivateStepAsync(taskId, stepId);
                    break;
                case "active":
                    await _taskService.ActivateStepAsync(taskId, stepId);
                    break;
                case "delete":
                    await _taskService.RemoveStepAsync(taskId, stepId);
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

            return Ok( new { message = _localizer["TaskStepDeleted"] });
        }
    }
}
