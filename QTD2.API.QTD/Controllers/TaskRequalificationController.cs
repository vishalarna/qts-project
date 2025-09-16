using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.FilterOptions;
using QTD2.Infrastructure.Model.Task_Requalification;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TaskRequalificationController : ControllerBase
    {
        private readonly ITaskRequalificationService _taskRequalService;
        private readonly ITaskReQualificationEmp_SuggestionService _taskRequalempSuggestionService;
        private readonly ITaskReQualificationEmp_StepService _taskRequalempStepService;
        private readonly ITaskReQualificationEmp_QuestionAnswerService _taskRequalempquestionService;
        private readonly ITaskReQualificationEmp_SignOffService _empSignOffService;
        private readonly IStringLocalizer<TaskRequalificationController> _localizer;

        public TaskRequalificationController(
            ITaskRequalificationService taskRequalService, IStringLocalizer<TaskRequalificationController> localizer, ITaskReQualificationEmp_SuggestionService taskRequalempSuggestionService, ITaskReQualificationEmp_StepService taskRequalempStepService, ITaskReQualificationEmp_QuestionAnswerService taskRequalempquestionService, ITaskReQualificationEmp_SignOffService empSignOffService)
        {
            _taskRequalService = taskRequalService;
            _localizer = localizer;
            _taskRequalempSuggestionService = taskRequalempSuggestionService;
            _taskRequalempStepService = taskRequalempStepService;
            _taskRequalempquestionService = taskRequalempquestionService;
            _empSignOffService = empSignOffService;
        }

        /// <summary>
        /// Get Task Qualification Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _taskRequalService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/taskQualification/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _taskRequalService.DeleteAsync(id);
            return Ok( new { message = _localizer["TaskQualificationDeleted"] });
        }

        /// <summary>
        /// Get all qualifications data for Employee
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/allEmp/{empId}/task/{taskId}")]
        public async Task<IActionResult> GetQualificationsForEmp(int empId,int taskId)
        {
            var result = await _taskRequalService.GetAllQualificationsForEmp(empId,taskId);
            return Ok( new { result });
        }

        /// <summary>
        /// Update Task Qualification Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/taskQualification/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskQualificationCreateOptions options)
        {
            var result = await _taskRequalService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualifications by positions
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/position")]
        public async Task<IActionResult> FilterByPosition(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterByPositionAsync(options);

            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualifications By Employee
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/emp")]
        public async Task<IActionResult> FilterByEMP(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterByEMPAsync(options);

            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualifications By Evaluators
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/eval")]
        public async Task<IActionResult> FilterByEval(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterByEvalAsync(options);

            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskQualification/task/posFilter")]
        public async Task<IActionResult> GetTaskTreeWithPositionIds(EMPFilterOptions option)
        {
            var result = await _taskRequalService.GetTaskTreeDataWithPositionIdsAsync(option);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskQualification/emp/filter")]
        public async Task<IActionResult> GetEmpDataForPositions(EMPFilterOptions options)
        {
            var result = await _taskRequalService.GetEmpDataForPositionsAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Get released Task Qualifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/released")]
        public async Task<IActionResult> GetTQReleasedToEMP()
        {
            var result = await _taskRequalService.GetTQReleasedToEMP();
            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualifications By Training Groups
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/tg")]
        public async Task<IActionResult> FilterByTG(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterByTGAsync(options);

            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualification By Skill Qualifications
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/sq")]
        public async Task<IActionResult> FilterBySQ(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterBySQAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Filter Task Qualifications by tasks
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/taskQualification/task")]
        public async Task<IActionResult> FilterByTask(TaskRequalificationFilterOptions options)
        {
            var result = await _taskRequalService.FilterByTaskAsync(options);

            return Ok( new { result });
        }

        /// <summary>
        /// Get a Task Qualification Information along with number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/withNumber/{id}")]
        public async Task<IActionResult> GetTaskWithNumber(int id)
        {
            var result = await _taskRequalService.GetTaskWithNumberAsync(id);
            return Ok( new { result });
        }


        /// <summary>
        /// Get All Employees that have pending task quali
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/pending")]
        public async Task<IActionResult> GetPendingTaskQualifications()
        {
            var result = await _taskRequalService.GetPendingQualifications();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskQualification/without")]
        public async Task<IActionResult> GetEmployeesWithoutTQRecords()
        {
            var result = await _taskRequalService.GetEmployeesWithoutTQRecordsAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Evaluators with Qualification Count
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/eval")]
        public async Task<IActionResult> GetEvaluatorWithCount()
        {
            var result = await _taskRequalService.GetEvaluatorWithCount();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Tasks For the Given Evaluator
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/emp/{id}/tasks")]
        public async Task<IActionResult> GetRequalTasksForEvalAsync(int id)
        {
            var result = await _taskRequalService.GetRequalTasksForEval(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskQualification/emp/{id}/pending")]
        public async Task<IActionResult> GetRequalTasksForEmp(int id)
        {
            var result = await _taskRequalService.GetRequalTasksForEmp(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Employees for the evaluator along with tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/eval/{id}/empwithtasks")]
        public async Task<IActionResult> GetEmpWithTasksForTQEvaluator(int id)
        {
            var result = await _taskRequalService.GetEmpWithTasksForTQEvaluator(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Employees linked to task qualification along with extra information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/{id}/emp")]
        public async Task<IActionResult> GetEmpLinkedToTask(int id)
        {
            var result = await _taskRequalService.GetEmpLinkedToTaskAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Get Employees that are not TQ Evaluator and Do not have a qualification Date
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/withoutPosDate")]
        public async Task<IActionResult> GetEmpsWithoutPosQualDate()
        {
            var result = await _taskRequalService.GetEmpsWithoutPosQualDateAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Get Stats for Task Requalification Overview screen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/taskQualification/stats")]
        public async Task<IActionResult> GetTaskRequalificationStats()
        {
            var result = await _taskRequalService.GetStatsAsync();
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskQualification")]
        public async Task<IActionResult> CreateTaskQualification(TaskQualificationCreateOptions options)
        {
            var result = await _taskRequalService.CreateTaskQualificationAsync(options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskQualification/release/byTaskAndSkill")]
        public async Task<IActionResult> CreateAndReleaseTaskAndSkillQualifications(TQReleaseByTaskAndSkillOptions options)
        {
            await _taskRequalService.CreateAndReleaseTaskAndSkillQualifications(options);
            return Ok( new { message = _localizer["TQsCreatedAndReleased"] });
        }

        [HttpGet]
        [Route("/taskQualification/emp/{empId}")]
        public async Task<IActionResult> GetTQLinkedToEMP(int empId)
        {
            var result = await _taskRequalService.GetTQLinkedToEMPAsync(empId);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/taskQualification/released")]
        public async Task<IActionResult> UpdateReleasedTQ(ReleasedTQAndSQUpdateOptions options)
        {
            await _taskRequalService.UpdateReleasedTQ(options);
            var custMessage = "";
            switch (options.Action.Trim().ToLower()) 
            {
                case "date":
                    custMessage = "Release And Due Date Updated";
                    break;
                case "reassign":
                    custMessage = "Task Qualification Reassigned";
                    break;
                case "recall":
                    custMessage = "Task Qualification(s) Recalled";
                    break;
            }

            return Ok( new { message = custMessage });
        }

        [HttpDelete]
        [Route("/taskQualification/reassign")]
        public async Task<IActionResult> ReassignTaskQualifications(ReassignTQVM options)
        {
            await _taskRequalService.ReassignTaskQualification(options);
            return Ok( new { message = _localizer["TaskQualificationsReassigned"] });
        }

        [HttpDelete]
        [Route("/taskQualification/eval/{id}")]
        public async Task<IActionResult> RemoveEvaluatorAsync(int id)
        {
            await _taskRequalService.RemoveEvaluatorAsync(id);
            return Ok( new { message = _localizer["EvaluatorRemoved"] });
        }

        [HttpGet]
        [Route("/taskQualification/recent")]
        public async Task<IActionResult> GetRecentTaskQualification()
        {
            var result = await _taskRequalService.GetRecentTQAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskQualification/showTaskSuggestion/{qualificationId}")]
        public async Task<IActionResult> IsShowSuggestionBit(int qualificationId)
        {
            var result = await _taskRequalempSuggestionService.GetShowSuggestionBit(qualificationId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskQualification/showTaskQuestions/{qualificationId}")]
        public async Task<IActionResult> IsShowQuestionBit(int qualificationId)
        {
            var result = await _taskRequalempSuggestionService.GeQuestionAnswerBit(qualificationId);
            return Ok( new { result });
        }

        
        [HttpPost]
        [Route("/taskQualification/eo/posFilter")]
        public async Task<IActionResult> GetEoTreeWithPositionIds(EMPFilterOptions option)
        {
            var result = await _taskRequalService.GetEoTreeWithPositionIds(option);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/taskQualification/showSkillSuggestion/{skillqualificationId}")]
        public async Task<IActionResult> IsShowSuggestionSQBit(int skillqualificationId)
        {
            var result = await _taskRequalempSuggestionService.IsShowSuggestionSQBit(skillqualificationId);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/taskQualification/showSkillQuestion/{skillqualificationId}")]
        public async Task<IActionResult> IsShowQuestionSQBit(int skillqualificationId)
        {
            var result = await _taskRequalempSuggestionService.IsShowQuestionSQBit(skillqualificationId);
            return Ok(new { result });
        }

    }
}
