using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Step;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class EnablingObjectivesController : ControllerBase
    {
        /// <summary>
        /// Get List of steps added to a eo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives/{id}/steps")]
        public async Task<IActionResult> GetStepsAsync(int id)
        {
            var steps = await _enablingObjectiveService.GetEO_StepsAsync(id);
            return Ok( new { steps });
        }

        /// <summary>
        /// Adds a step to the eo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/steps")]
        public async Task<IActionResult> CreateStepAsync(int id, EnablingObjective_StepCreateOptions options)
        {
            var step = await _enablingObjectiveService.CreateStepAsync(id, options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = id;
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeNotes = "New Step Added To SQ";
            var eo = await _enablingObjectiveService.GetAsync(id);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            //var task = await _taskService.GetAsync(id);
            //await _versioningService.VersionTaskAsync(task, options.isSignificant);
            //if (options.isSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            return Ok( new { step, message = _localizer["enablingObjectiveStepCreated"] });
        }

        /// <summary>
        /// Get the latest number for the task step (It is already incremented)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/enablingObjectives/{id}/steps/num")]
        public async Task<IActionResult> GetStepNumber(int id)
        {
            var result = await _enablingObjectiveService.GetEOStepNumber(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates a step on a task
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="stepId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/enablingObjectives/{eoId}/steps/{stepId}")]
        public async Task<IActionResult> UpdateStepAsync(int eoId, int stepId, EnablingObjective_StepUpdateOptions options)
        {
            //var stepSnapshot = _enablingObjectiveService.GetEO_StepsAsync(eoId).Result.Where(x => x.Id == stepId).FirstOrDefault();
            //await _versioningService.VersionEnablingObjective_StepAsync(stepSnapshot);
            var step = await _enablingObjectiveService.UpdateStepAsync(eoId, stepId, options);
            //var task = await _taskService.GetAsync(taskId);
            //await _versioningService.VersionTaskAsync(task, options.isSignificant);
            //if (options.isSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}

            return Ok( new { step, message = _localizer["taskStepUpdated"] });
        }

        //[HttpPut]
        //[Route("/enablingObjectives/steps")]
        //public async Task<IActionResult> UpdateStepNumbers(Task_StepNumberOptions options)
        //{
        //    await _taskService.UpdateTask_StepNumber(options.Numbers, options.StepIds);
        //    return Ok( new { message = _localizer["StepNumbersUpdated"] });
        //}

        /// <summary>
        /// Removes a step from a task
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="stepId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/steps/{stepId}")]
        public async Task<IActionResult> DeleteStepAsync(int eoId, int stepId, EnablingObjectiveOptions options)
        {
            var stepSnapshot = (await _enablingObjectiveService.GetEO_StepsAsync(eoId)).Where(x => x.Id == stepId).FirstOrDefault();
            await _versioningService.VersionEnablingObjective_StepAsync(stepSnapshot);
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _enablingObjectiveService.DeactivateStepAsync(eoId, stepId);
                    break;
                case "active":
                    await _enablingObjectiveService.ActivateStepAsync(eoId, stepId);
                    break;
                case "delete":
                    await _enablingObjectiveService.RemoveStepAsync(eoId, stepId);
                    var histOptions = new EnablingObjectiveHistoryCreateOptions();
                    histOptions.EnablingObjectiveId = eoId;
                    histOptions.NewStatus = true;
                    histOptions.OldStatus = false;
                    histOptions.ChangeNotes = "Step Removed From SQ";
                    var eo = await _enablingObjectiveService.GetAsync(eoId);
                    var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo,0);
                    histOptions.Version_EnablingObjectiveId = version.Id;
                    await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
                    break;
            }

            //var task = await _taskService.GetAsync(taskId);
            //await _versioningService.VersionTaskAsync(task, options.IsSignificant);
            //if (options.IsSignificant)
            //{
            //    var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //    foreach (var vTask in versionedTasks)
            //    {
            //        await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //        await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //    }
            //}
            //var histOptions = new Task_HistoryOptions();
            //histOptions.EffectiveDate = options.EffectiveDate;
            //histOptions.ChangeNotes = options.ChangeNotes;
            //histOptions.TaskIds = new int[] { eoId };
            //await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["TaskStepDeleted"] });
        }
    }
}
