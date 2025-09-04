using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjective_Question;
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
        /// Get a list of procedure linked to a enablingObjective
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http response code with data</returns>
        [HttpGet]
        [Route("/enablingObjectives/{id}/questions")]
        public async Task<IActionResult> GetQuestionsAsync(int id)
        {
            var questions = await _enablingObjectiveService.GetEO_QuestionsAsync(id);
            return Ok( new { questions });
        }

        /// <summary>
        /// Get a list of procedure linked to a enablingObjectives
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpPost]
        [Route("/enablingObjectives/{id}/questions")]
        public async Task<IActionResult> AddQuestionAsync(int id, EnablingObjective_QuestionCreateOptions options)
        {
            var question = await _enablingObjectiveService.AddQuestionAsync(id, options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = id;
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeNotes = "New Question Added To SQ";
            var eo = await _enablingObjectiveService.GetAsync(id);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 1);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
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

            return Ok( new { question, message = _localizer["EOQuestionAdded"] });
        }

        [HttpGet]
        [Route("/enablingObjectives/{id}/questions/num")]
        public async Task<IActionResult> GetQuestionNumber(int id)
        {
            var result = await _enablingObjectiveService.GetQuestionNumber(id);
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
        [Route("/enablingObjectives/{id}/questions/{quesId}")]
        public async Task<IActionResult> UpdateQuestionAsync(int id, int quesId, EnablingObjective_QuestionCreateOptions options)
        {
            //var question = _enablingObjectiveService.GetEO_QuestionsAsync(id).Result.Where(x => x.Id == quesId).FirstOrDefault();
            //await _versioningService.VersionEnablingObjective_QuestionAsync(question);
            await _enablingObjectiveService.UpdateQuestionAsync(id, quesId, options);
            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.EnablingObjectiveId = id;
            histOptions.NewStatus = true;
            histOptions.OldStatus = false;
            histOptions.ChangeNotes = "Question Updated In SQ";
            var eo = await _enablingObjectiveService.GetAsync(id);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 2);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);
            return Ok( new { message = _localizer["EOQuestionUpdated"] });
        }

        /// <summary>
        /// Reorder Questions in database
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/enablingObjectives/questions")]
        public async Task<IActionResult> UpdateQuestionNumbers(EnablingObjective_QuestionNumberOptions options)
        {
            await _enablingObjectiveService.UpdateQuestionNumbers(options);
            return Ok( new { message = _localizer["QuestionOrderUpdated"] });
        }


        /// <summary>
        /// Get a list of procedure linked to a enablingObjectives
        /// </summary>
        /// <param name="eoId"></param>
        /// <param name="questionId"></param>
        /// <param name="options"></param>
        /// <returns>Http response code with data</returns>
        [HttpDelete]
        [Route("/enablingObjectives/{eoId}/questions/{questionId}")]
        public async Task<IActionResult> RemoveQuestionAsync(int eoId, int questionId, EnablingObjectiveOptions options)
        {
            //var question = _enablingObjectiveService.GetEO_QuestionsAsync(eoId).Result.Where(x => x.Id == questionId).FirstOrDefault();
            //await _versioningService.VersionEnablingObjective_QuestionAsync(question);
            await _enablingObjectiveService.RemoveQuestionAsync(eoId, questionId);
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

            var histOptions = new EnablingObjectiveHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = options.EffectiveDate;
            histOptions.ChangeNotes = options.ChangeNotes;
            histOptions.EnablingObjectiveId = eoId;
            histOptions.OldStatus = true;
            histOptions.NewStatus = false;
            var eo = await _enablingObjectiveService.GetAsync(eoId);
            var version = await _versionEnablingObjectiveService.VersionAndCreateCopy(eo, 0);
            histOptions.Version_EnablingObjectiveId = version.Id;
            await _enablingObjectiveHistoryService.CreateEOHistory(histOptions);


            return Ok( new { message = _localizer["QuestionRemovedFromEO"] });
        }
    }
}
