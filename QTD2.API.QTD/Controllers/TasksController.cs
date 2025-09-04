using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Reports;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Version_Task;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IVersioningService _versioningService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly ITask_ReferenceService _taskRefService;
        private readonly ITask_CollaboratorInvitationService _taskColInvitService;
        private readonly IStringLocalizer<TasksController> _localizer;
        private readonly ITask_HistoryService _historyService;
        private readonly IVersion_TaskService _ver_taskService;
        private readonly IHasher _hasher;
        private readonly IReportsService _reportsService;
        private readonly IReportGeneratorService _reportGeneratorService;

        public TasksController(ITaskService taskService, IVersioningService versioningService, IEmployeeTaskService employeeTaskService, IStringLocalizer<TasksController> localizer, ITask_ReferenceService taskRefService, ITask_CollaboratorInvitationService taskColInvitService, ITask_HistoryService historyService, IVersion_TaskService ver_taskService, IHasher hasher, IReportsService reportsService, IReportGeneratorService reportGeneratorService)
        {
            _taskService = taskService;
            _versioningService = versioningService;
            _employeeTaskService = employeeTaskService;
            _localizer = localizer;
            _taskRefService = taskRefService;
            _taskColInvitService = taskColInvitService;
            _historyService = historyService;
            _ver_taskService = ver_taskService;
            _hasher = hasher;
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
        }

        /// <summary>
        /// Gets a list of tasks
        /// </summary>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks")]
        public async Task<IActionResult> GetAsync()
        {
            var tasks = await _taskService.GetAsync();
            return Ok(new { tasks });
        }

        /// <summary>
        /// Get Task number along with SDA number and DA number and title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/number")]
        public async Task<IActionResult> GetTaskNumberWithLetter(int id)
        {
            var result = await _taskService.GetTaskNumberWithLetter(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/review")]
        public async Task<IActionResult> GetPendingTasks()
        {
            var result = await _taskService.GetPendingTasks();
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var task = await _taskService.GetAsync(id);
            return Ok( new { task });
        }

        [HttpGet]
        [Route("/tasks/{id}/alldata")]
        public async Task<IActionResult> GetAllLinkDataOfTask(int id)
        {
            var result = await _taskService.GetAllLinkDataOfTaskAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a task
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks")]
        public async Task<IActionResult> CreateAsync(TaskCreateOptions options)
        {
            var task = await _taskService.CreateAsync(options);
            var histOptions = new Task_HistoryOptions();
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.TaskIds = new int[] { task.Id };
            var version = await _ver_taskService.CreateTaskVersion(task, 2,false);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { task, message = _localizer["taskCreated"] });
        }

        [HttpPost]
        [Route("/tasks/{id}/copy")]
        public async Task<IActionResult> CreateCopyAsync(int id,TaskCopyOptions options)
        {
            var result = await _taskService.CopyTask(id,options);
            var histOptions = new Task_HistoryOptions();
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.TaskIds = new int[] { result.Id };
            var version = await _ver_taskService.VersionAndCreateCopy(result, 2,false);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { result, message = _localizer["taskCreated"] });
        }

        /// <summary>
        /// Updates a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPut]
        [Route("/tasks/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskUpdateOptions options)
        {
            var task = await _taskService.GetAsync(id);
            bool rr = task.IsReliability;
            task = await _taskService.UpdateAsync(id, options);
            var histOptions = new Task_HistoryOptions();
            histOptions.ChangeNotes = (rr != options.IsReliability ? ("Change R-R flag " + (options.IsReliability ? "On " : "Off ")) : "") + options.ChangeNotes;
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.TaskIds = new int[] { task.Id };
            //await _historyService.SaveHistoryAsync(histOptions);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 2,false);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { task, message = _localizer["taskUpdated"] });
        }

        /// <summary>
        /// Gets all Task Data including linked data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/{id}/all")]
        public async Task<IActionResult> GetAllTaskDataWithLinks(int id)
        {
            var result = await _taskService.GetAllTaskDataWithLinks(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get the tasks data using subduty area Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/tasks/sda/{id}")]
        public async Task<IActionResult> GetTaskUsingSDAId(int id)
        {
            var result = await _taskService.getUsingSDAId(id);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/tasks/{id}/specific")]
        public async Task<IActionResult> EditSpecificField(int id, SpecificUpdateOptions option)
        {
            await _taskService.EditSpecificField(id, option);
            var histOptions = new Task_HistoryOptions();
            histOptions.ChangeNotes = "Task " + option.Field + " Updated";
            histOptions.EffectiveDate = DateTime.UtcNow;
            histOptions.TaskIds = new int[] { id};

            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["taskFieldUpdated"] }); ;
        }

        [HttpGet]
        [Route("/tasks/{id}/meta/linkedStats")]
        public async Task<IActionResult> GetLinkedMetaStats(int id)
        {
            var result = await _taskService.GetLinkedMetaStatsAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/sda/{sdaId}/withNum")]
        public async Task<IActionResult> GetTasksWithSDAId(int sdaId)
        {
            var result = await _taskService.GetTasksWithSDAIdAsync(sdaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/extras")]
        public async Task<IActionResult> GetCondCritRefForMeta(int id)
        {
            var result = await _taskService.GetCondCritRefForMetaAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/{id}/requal")]
        public async Task<IActionResult> GetRequalInfo(int id)
        {
            var result = await _taskService.GetRequalInfoAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with message</returns>
        [HttpDelete]
        [Route("/tasks")]
        public async Task<IActionResult> DeleteAsync(TaskOptions options)
        {
            var taskIds = new HashSet<int>();


            if (options.TaskIds != null && options.TaskIds.Length > 0)
            {
                taskIds.UnionWith(options.TaskIds);
            }

            foreach (var tId in taskIds)
            {
                var taskSnapshot = await _taskService.GetAsync(tId);
                var ver = await _ver_taskService.VersionAndCreateCopy(taskSnapshot,0);

                string changeNotes = "";

                switch (options.ActionType.ToLower())
                {
                    case "inactive":
                    default:
                        await _taskService.DeactivateAsync(tId);
                        changeNotes = "Inactive Task True";
                        ver.TaskActive = false;
                        break;
                    case "active":
                        changeNotes = "Inactive Task False";
                        await _taskService.ActivateAsync(tId);
                        ver.TaskActive = true;
                        break;
                    case "delete":
                        changeNotes = "Inactive Task True";
                        await _taskService.DeleteAsync(tId);
                        break;
                }
                var histOptions = new Task_HistoryOptions();
                histOptions.Version_TaskId = ver.Id;
                histOptions.ChangeNotes = changeNotes + " | " + options.ChangeNotes;
                histOptions.EffectiveDate = options.EffectiveDate;
                histOptions.TaskIds = new int[] { tId };

                await _historyService.SaveHistoryAsync(histOptions);
            }
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

            return Ok( new { message = _localizer["Task_" + options.ActionType.ToLower()] });
        }

        /// <summary>
        /// Gets linked stats count of a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/linkedStats")]
        public async Task<IActionResult> GetLinkedStatsAsync(int id)
        {
            var stats = await _taskService.GetTaskLinkedStats(id);
            return Ok( new { stats });
        }

        /// <summary>
        /// Gets not linked stats count of a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/stats")]
        public async Task<IActionResult> GetOverviewStatsAsync()
        {
            var stats = await _taskService.GetTaskNotLinkedStats();
            return Ok( new { stats });
        }

        [HttpGet]
        [Route("/tasks/{id}/canDeactive")]
        public async Task<IActionResult> CanTaskBeDeactivatedAsync(int id)
        {
            var result = await _taskService.CanTaskBeDeactivatedAsync(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Gets linkedIds of Task with modules
        /// </summary>
        /// <param name="option"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/linkedIds/{option}")]
        public async Task<IActionResult> GetLinkedIds(string option)
        {
            var result = await _taskService.GetLinkedIds(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/list/{option}")]
        public async Task<IActionResult> GetTaskActiveInactive(string option)
        {
            var result = await _taskService.GetTaskActiveInactive(option);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/tasks/getAllTasks")]
        public async Task<IActionResult> GetTasksWithDutySubDutyAreaAsync()
        {
            var result = await _taskService.GetTasksWithDutySubDutyAreaAsync();
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/tasks/historyByTasks/generateReport")]
        public async Task<IActionResult> GenerateReportAsync(ExportReportModel model)
        {
            var taskFilter = model.Options.Filters.FirstOrDefault(x => x.Name.ToUpper() == "SELECT TASKS");
            if (taskFilter != null && !string.IsNullOrEmpty(taskFilter.Value))
            {
                var encodedIds = taskFilter.Value.Split(",").ToList();
                taskFilter.Value = String.Join(",", encodedIds.Select(x => _hasher.Decode(x)));
            }
            else
            {
                throw new QTDServerException("Task Id is missing in the given input");
            }
            var report = await _reportsService.CreateReportAsync(model.Options, false);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

        [HttpPost]
        [Route("/tasks/versionTask")]
        public async Task<IActionResult> UpdateTaskAndVersionTaskAsync(VersionTaskModel options)
        {
             await _taskService.UpdateTaskAndVersionTaskAsync(options);
            return Ok(new { message = _localizer["TaskandVersionTaskupdatedsuccessfully"] });
        }
    }
}
