using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TaskReQualificationEmp;

namespace QTD2.API.QTD.Controllers
{
    public partial class TaskRequalificationController : ControllerBase
    {
        [HttpGet]
        [Route("/empTaskQualification/pending")]
        public async Task<IActionResult> GetEmployeePendingTaskRequalification()
        {
            var result = await _taskRequalService.GetEmployeePendingTaskRequalification();
            return Ok( new { result });
        }
        
        [HttpGet]
        [Route("/empTaskQualification/suggestions/{qualificationId}/task/{taskId}/emp/{employeeId}")]
        public async Task<IActionResult> GetSuggestionData(int qualificationId, int taskId, int employeeId)
        {
            var result = await _taskRequalempSuggestionService.GetSuggestionData(qualificationId,taskId,employeeId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/empTaskQualification/suggestions")]
        public async Task<IActionResult> CreateOrUpdateSuggestionsAsync(TaskReQualificationEmpSuggestionVM options)
        {
             await _taskRequalempSuggestionService.CreateOrUpdateSuggestionsAsync(options);
             return Ok( new { message = _localizer["TaskReQualificationSuggestionCreated"] });
        }

        [HttpGet]
        [Route("/empTaskQualification/steps/{qualificationId}/task/{taskId}/emp/{employeeId}")]
        public async Task<IActionResult> GetStepsData(int qualificationId, int taskId, int employeeId)
        {
                var result = await _taskRequalempStepService.GetStepsData(qualificationId, taskId, employeeId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/empTaskQualification/steps")]
        public async Task<IActionResult> CreateOrUpdateStepsAsync(TaskReQualificationEmpStepVM options)
        {
            await _taskRequalempStepService.CreateOrUpdateStepsAsync(options);
            return Ok( new { message = _localizer["TaskReQualificationStepsCreated"] });
        }

        [HttpGet]
        [Route("/empTaskQualification/questions/{qualificationId}/task/{taskId}/emp/{employeeId}")]
        public async Task<IActionResult> GetQuestionsData(int qualificationId, int taskId, int employeeId)
        {
            var result = await _taskRequalempquestionService.GetQuestionsData(qualificationId, taskId, employeeId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/empTaskQualification/questions")]
        public async Task<IActionResult> CreateOrUpdateQuestionsDataAsync(TaskReQualificationEmpQuestionVM options)
        {
            await _taskRequalempquestionService.CreateOrUpdateQuestionsDataAsync(options);
            return Ok( new { message = _localizer["TaskReQualificationStepsCreated"] });
        }
        [HttpGet]
        [Route("/empTaskQualification/signoff/{qualificationId}/emp/{employeeId}")]
        public async Task<IActionResult> GetEvaluatorSignOffData(int qualificationId, int employeeId)
        {
            var result = await _empSignOffService.GetSignOffData(qualificationId, employeeId);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/empTaskQualification/signOff")]
        public async Task<IActionResult> CreateOrUpdateSignOffAsync(TaskReQualificationEmpSignOffVM options)
        {
            await _empSignOffService.CreateOrUpdateSignOffAsync(options);
            return Ok( new { message = _localizer["SignOffCreated"] });
        }

        [HttpGet]
        [Route("/empTaskQualification/completed/isEvaluator/{isEvaluator}")]
        public async Task<IActionResult> GetCompletedTaskRequalification(bool isEvaluator)
        {
            var result = await _empSignOffService.GetCompletedTaskRequalifications(isEvaluator);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/empTaskQualification/feedback/qualification/{qualificationId}/trainee/{traineeId}")]
        public async Task<IActionResult> GetFeedBackData(int qualificationId, int traineeId)
        {
            var result = await _empSignOffService.GetFeedBackData(qualificationId,traineeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/empTaskQualification/suggestions/{skillqualificationId}/skill/{skillId}/emp/{employeeId}")]
        public async Task<IActionResult> GetSuggestionSQData(int skillqualificationId, int skillId, int employeeId)
        {
           var result = await _taskRequalempSuggestionService.GetSuggestionSQData(skillqualificationId, skillId, employeeId);
           return Ok(new { result });
        }

        [HttpGet]
        [Route("/empTaskQualification/steps/{skillqualificationId}/skill/{skillId}/emp/{employeeId}")]
        public async Task<IActionResult> GetStepsSQData(int skillqualificationId, int skillId, int employeeId)
        {
            var result = await _taskRequalempStepService.GetStepsSQData(skillqualificationId, skillId, employeeId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/empTaskQualification/questions/{skillqualificationId}/skill/{skillId}/emp/{employeeId}")]
        public async Task<IActionResult> GetQuestionsSQData(int skillqualificationId, int skillId, int employeeId)
        {
            var result = await _taskRequalempquestionService.GetQuestionsSQData(skillqualificationId, skillId, employeeId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/empTaskQualification/signoff/{skillqualificationId}/emp/{employeeId}/skill")]
        public async Task<IActionResult> GetSQEvaluatorSignOffData(int skillqualificationId, int employeeId)
        {
            var result = await _empSignOffService.GetSQEvaluatorSignOffDataAsync(skillqualificationId, employeeId);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/empTaskQualification/signOff/skill")]
        public async Task<IActionResult> CreateOrUpdateSQSignOffAsync(TaskReQualificationEmpSignOffVM options)
        {
            await _empSignOffService.CreateOrUpdateSQSignOffAsync(options);
            return Ok(new { message = _localizer["SignOffCreated"] });
        }

        [HttpGet]
        [Route("/empTaskQualification/feedback/skillqualification/{skillqualificationId}/trainee/{traineeId}")]
        public async Task<IActionResult> GetFeedBackSQData(int skillqualificationId, int traineeId)
        {
            var result = await _empSignOffService.GetFeedBackSQData(skillqualificationId, traineeId);
            return Ok(new { result });
        }
    }
}
