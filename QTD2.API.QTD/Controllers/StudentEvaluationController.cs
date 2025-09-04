using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.StudentEvaluation;
using QTD2.Infrastructure.Model.StudentEvaluationForm;
using QTD2.Infrastructure.Model.StudentEvaluationHistory;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class StudentEvaluationController : ControllerBase
    {
        private readonly IStudentEvaluationService _studentEvaluationService;
        private readonly IStudentEvaluationHistoryService _studentEvaluationHistoryService;
        private readonly IStringLocalizer<StudentEvaluationController> _localizer;

        public StudentEvaluationController(IStudentEvaluationService studentEvaluationService, IStringLocalizer<StudentEvaluationController> localizer, IStudentEvaluationHistoryService studentEvaluationHistoryService)
        {
            _studentEvaluationService = studentEvaluationService;
            _localizer = localizer;
            _studentEvaluationHistoryService = studentEvaluationHistoryService;
        }

        /// <summary>
        /// Gets a list of Student Evaluation
        /// </summary>
        /// <returns>Http Response Code with Student Evaluations</returns>
        [HttpGet]
        [Route("/studentEvaluations")]
        public async Task<IActionResult> GetStudentEvaluationsAsync()
        {

            var result = await _studentEvaluationService.GetAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/studentEvaluations/published")]
        public async Task<IActionResult> GetPublishedEvals()
        {
            var result = await _studentEvaluationService.GetPublishedEvalsAsync();
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/emp/evaluations")]
        public async Task<IActionResult> GetEmployeeEvaluationsAsync()
        {

            var result = await _studentEvaluationService.GetEvaluationsAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/evaluations/start/{evaluationId}")]
        public async Task<IActionResult> StartEmployeeEvaluationsAsync(int evaluationId)
        {

            var result = await _studentEvaluationService.StartEvaluationAsync(evaluationId);
            return Ok( new { result });
        }
        [HttpPut]
        [Route("/emp/evaluations/complete")]
        public async Task<IActionResult> CompleteEmployeeEvaluationsAsync(ClassSchedule_Evaluation_RosterOptions options)
        {
            var result = await _studentEvaluationService.CompleteEvaluationAsync(options);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new Student Evaluation
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/studentEvaluations")]
        public async Task<IActionResult> CreateStudentEvaluationAsync(StudentEvaluationCreateOptions options)
        {
            var result = await _studentEvaluationService.CreateAync(options);
            var histOptions = new StudentEvaluationHistoryCreateOptions();
            if (options.Mode == "Copy")
            {
                histOptions.EffectiveDate =options.EffectiveDate;
                histOptions.StudentEvaluationNotes = options.Notes == null || options.Notes == "" ? "Student Evaluation Copied":options.Notes;
                histOptions.StudentEvaluationId = result.Id;
                await _studentEvaluationHistoryService.CreateAsync(histOptions);

            }
            else
            {
                histOptions.EffectiveDate = DateTime.UtcNow;
                histOptions.StudentEvaluationNotes = "Student Evaluation Created with title : " + options.Title;
                histOptions.StudentEvaluationId = result.Id;
                await _studentEvaluationHistoryService.CreateAsync(histOptions);
            }

            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific Student Evaluation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Student Evaluation</returns>
        [HttpGet]
        [Route("/studentEvaluations/{id}")]
        public async Task<IActionResult> GetStudentEvaluationAsync(int id)
        {
            var result = await _studentEvaluationService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Gets a specific Student Evaluation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Student Evaluation</returns>
        [HttpGet]
        [Route("/studentEvaluations/{id}/scale")]
        public async Task<IActionResult> GetStudentEvaluationWithScaleAsync(int id)
        {
            var result = await _studentEvaluationService.GetWithRatingScale(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific Rating Scale by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/studentEvaluations/{id}")]
        public async Task<IActionResult> UpdateStudentEvaluationAsync(int id, StudentEvaluationCreateOptions options)
        {
            var result = await _studentEvaluationService.UpdateAsync(id, options);
            var histOptions = new StudentEvaluationHistoryCreateOptions();
            if (options.Mode == "Edit")
            {
                histOptions.EffectiveDate = options.EffectiveDate;
                histOptions.StudentEvaluationNotes = options.Notes == null || options.Notes == "" ? "Student Evaluation Edited": options.Notes;
                histOptions.StudentEvaluationId = result.Id;
                await _studentEvaluationHistoryService.CreateAsync(histOptions);

            }
            else
            {
                histOptions.EffectiveDate = DateTime.UtcNow;
                histOptions.StudentEvaluationNotes = "Student Evaluation Created with title : " + options.Title;
                histOptions.StudentEvaluationId = result.Id;
                await _studentEvaluationHistoryService.CreateAsync(histOptions);

            }
           
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Question by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/studentEvaluations/{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(int id, StudentEvaluationHistoryCreateOptions options)
        {
            foreach (var studentEval in options.StudentEvaluationIds)
            {
                switch (options.ActionType.ToLower())
                {
                    case "inactive":
                    default:
                        await _studentEvaluationService.DeactivateAsync(studentEval, options);
                        break;
                    case "active":
                        await _studentEvaluationService.ActivateAsync(studentEval, options);
                        break;
                    case "delete":
                        await _studentEvaluationService.DeleteAsync(studentEval, options);
                        break;
                }
                var histOptions = new StudentEvaluationHistoryCreateOptions();
                histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
                histOptions.StudentEvaluationNotes = options.StudentEvaluationNotes;
                histOptions.StudentEvaluationId = id;
                await _studentEvaluationHistoryService.CreateAsync(histOptions);
              
            }
            return Ok( new { message = _localizer[$"studentEvaluation-{options.ActionType.ToLower()}"].Value });
        }

        [HttpGet]
        [Route("/studentEvaluations/stats")]
        public async Task<IActionResult> GetStatsAsync()
        {
            var stats = await _studentEvaluationService.GetStatsCount();
            return Ok( new { stats });
        }
        [HttpPut]
        [Route("/studentEvaluations/{id}/publish")]
        public async Task<IActionResult> PublishEvaluation(int id, StudentEvaluationHistoryCreateOptions options)
        {
            var publishEval = await _studentEvaluationService.PublishEvaluation(id);
            var histOptions = new StudentEvaluationHistoryCreateOptions();
            histOptions.EffectiveDate = (DateTime)options.EffectiveDate;
            histOptions.StudentEvaluationNotes = options.StudentEvaluationNotes;
            histOptions.StudentEvaluationId = id;
            await _studentEvaluationHistoryService.CreateAsync(histOptions);
            return Ok( new { publishEval });
        }

        //active, publish, development
        [HttpGet]
        [Route("/studentEvaluations/{option}/list")]
        public async Task<IActionResult> GetList(string option)
        {
            var stats = await _studentEvaluationService.GetList(option);
            return Ok( new { stats });
        }

    }
}
