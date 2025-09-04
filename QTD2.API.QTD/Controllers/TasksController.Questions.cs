using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Employee_Task;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_Question;

namespace QTD2.API.QTD.Controllers
{
    public partial class TasksController : ControllerBase
    {
        /// <summary>
        /// Get a list of procedure linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/tasks/{id}/questions")]
        public async Task<IActionResult> GetQuestionsAsync(int id)
        {
            var questions = await _taskService.GetTask_QuestionsAsync(id);
            return Ok( new { questions });
        }

        /// <summary>
        /// Get a list of procedure linked to a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/tasks/{id}/questions")]
        public async Task<IActionResult> AddQuestionAsync(int id, QuestionCreateOptions options)
        {
            var question = await _taskService.AddQuestionAsync(id, options);

            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.ChangeNotes =  "New Question Created For Task";
            histOptions.EffectiveDate = DateTime.UtcNow;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { question, message = _localizer["TaskQuestionAdded"] });
        }

        [HttpGet]
        [Route("/tasks/{id}/questions/num")]
        public async Task<IActionResult> GetQuestionNumber(int id)
        {
            var result = await _taskService.GetQuestionNumber(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Update a specific question data for a task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quesId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/{id}/questions/{quesId}")]
        public async Task<IActionResult> UpdateQuestionAsync(int id, int quesId, QuestionCreateOptions options)
        {
            await _taskService.UpdateQuestionAsync(id, quesId, options);

            var histOptions = new Task_HistoryOptions();
            histOptions.TaskIds = new int[] { id };
            histOptions.ChangeNotes = "Question Updated For Task";
            histOptions.EffectiveDate = DateTime.UtcNow;
            var task = await _taskService.GetAsync(id);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 1);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);
            return Ok( new { message = _localizer["TaskQuestionUpdated"] });
        }

        /// <summary>
        /// Reorder Questions in database
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/tasks/questions")]
        public async Task<IActionResult> UpdateQuestionNumbers(Task_QuestionNumberOptions options)
        {
            await _taskService.UpdateQuestionNumbers(options);
            return Ok( new { message = _localizer["QuestionOrderUpdated"] });
        }

        [HttpGet]
        [Route("/tasks/meta/{id}/question")]
        public async Task<IActionResult> GetMetaTaskQuestionData(int id)
        {
            var result = await _taskService.GetMetaTaskQuestionDataAsync(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Get a list of procedure linked to a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="questionId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpDelete]
        [Route("/tasks/{taskId}/questions/{questionId}")]
        public async Task<IActionResult> RemoveQuestionAsync(int taskId, int questionId, TaskOptions options)
        {
            await _taskService.RemoveQuestionAsync(taskId, questionId);
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

            var histOptions = new Task_HistoryOptions();
            histOptions.EffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.TaskIds = new int[] { taskId };
            var task = await _taskService.GetAsync(taskId);
            var version = await _ver_taskService.VersionAndCreateCopy(task, 0);
            histOptions.Version_TaskId = version.Id;
            await _historyService.SaveHistoryAsync(histOptions);

            return Ok( new { message = _localizer["QuestionRemovedFromTask"] });
        }
    }
}
