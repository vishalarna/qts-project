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
        /// Get list of positions linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/positions/count")]
        public async Task<IActionResult> GetLinkedPositionsWithCountAsync(int id)
        {
            var taskPositions = await _taskService.GetLinkedPositionsAsync(id);
            return Ok( new { taskPositions });
        }


        [HttpGet]
        [Route("/tasks/{metaId}/positions/allcount")]
        public async Task<IActionResult> GetLinkedPositionWithMetaTask(int metaId)
        {
            var result = await _taskService.GetLinkedPositionToMetaTaskWithCountAsync(metaId);
            return Ok( new { result });
        }


        /// <summary>
        /// Get list of employees linked to a task positions
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/positions/employees")]
        public async Task<IActionResult> GetTaskPositionEmployeesAsync(int id)
        {
            var employees = await _taskService.GetTaskPositionEmployeesAsync(id);
            return Ok( new { employees });
        }

        [HttpGet]
        [Route("/tasks/{metaId}/positions/employees/allcount")]
        public async Task<IActionResult> GetLinkedEmployeeWithMetaTask(int metaId)
        {
            var result = await _taskService.GetLinkedEmployeeToMetaTaskWithCountAsync(metaId);
            return Ok( new { result });
        }



        /// <summary>
        /// Get list of positions linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/positions")]
        public async Task<IActionResult> GetTaskLinkedToPositionsAsync(int id)
        {
            var result = await _taskService.GetTaskLinkedToPosition(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/tasks/{id}/positions/without")]
        public async Task<IActionResult> LinkWithoutUnlinkAsync(int id, TaskOptions options)
        {
            var positions = await _taskService.LinkWithoutUnlinkPositions(id, options);
            if (options.PositionIds.Length > 0)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.TaskIds = new int[] { id };

                histOptions.ChangeNotes = options.PositionIds.Length + " Positions Linked To Task";
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                var task = await _taskService.GetAsync(id);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _historyService.SaveHistoryAsync(histOptions);
            }
            // var task = await _taskService.GetAsync(id);
            // await _versioningService.VersionTaskAsync(task, options.IsSignificant);
            // if (options.IsSignificant)
            // {
            //     var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //     foreach (var vTask in versionedTasks)
            //     {
            //         await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //         await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //     }
            // }
            return Ok( new { positions, message = _localizer["TaskPositionLinked"] });
        }

        /// <summary>
        /// Links a position to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/positions")]
        public async Task<IActionResult> LinkPositionsAsync(int id, TaskOptions options)
        {
            var result = await _taskService.LinkPositionAsync(id, options);
            if (result.HasChanges)
            {
                var histOptions = new Task_HistoryOptions();
                histOptions.TaskIds = new int[] { id };

                histOptions.ChangeNotes = $"Task linked to {result.LinkedIds.Count} position(s), unlinked from {result.UnlinkedIds.Count} position(s)";
                histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
                var task = await _taskService.GetAsync(id);
                var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
                histOptions.Version_TaskId = version.Id;
                await _historyService.SaveHistoryAsync(histOptions);
            }
            // var task = await _taskService.GetAsync(id);
            // await _versioningService.VersionTaskAsync(task, options.IsSignificant);
            // if (options.IsSignificant)
            // {
            //     var versionedTasks = await _versioningService.GetVersionedTasksAsync();
            //     foreach (var vTask in versionedTasks)
            //     {
            //         await _employeeTaskService.ArchiveEmployeeTaskAsync(vTask.TaskId, null, null);
            //         await _employeeTaskService.CreateAsync(new EmployeeTaskCreateOptions { TaskId = vTask.TaskId });
            //     }
            // }
            return Ok( new { message = _localizer["TaskPositionLinked"] });
        }

        /// <summary>
        /// Unlinks a position to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks/{id}/positions")]
        public async Task<IActionResult> UnlinkPositionsAsync(int id, TaskOptions options)
        {
            await _taskService.UnlinkPositionAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = System.DateTime.UtcNow.Date;
            histOptions.TaskIds = new int[] { id };

            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            //var task = await _taskService.GetAsync(id);
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

            return Ok( new { message = _localizer["PositionUnlinkedFromTask"] });
        }
    }
}
