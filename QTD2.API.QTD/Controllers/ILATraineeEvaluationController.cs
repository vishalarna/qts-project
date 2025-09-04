using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.EvalutionType;
using QTD2.Infrastructure.Model.ILATraineeEvaluation;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ILATraineeEvaluationController : ControllerBase
    {
        private readonly IILATraineeEvaluationService _iLATraineeEvaluationService;
        private readonly IStringLocalizer<ILATraineeEvaluationController> _localizer;
        private readonly IDiscussionQuestionService _discussionQuestionService;

        public ILATraineeEvaluationController(IILATraineeEvaluationService iLATraineeEvaluationService, IStringLocalizer<ILATraineeEvaluationController> localizer, IDiscussionQuestionService discussionQuestionService)
        {
            _iLATraineeEvaluationService = iLATraineeEvaluationService;
            _localizer = localizer;
            _discussionQuestionService = discussionQuestionService;
        }

        /// <summary>
        /// Gets a list of ILA Trainee Evaluation
        /// </summary>
        /// <returns>Http Response Code with ILA Trainee Evaluations</returns>
        [HttpGet]
        [Route("/ilatraineeevaluation")]
        public async Task<IActionResult> GetILATraineeEvaluationAsync()
        {
            var iLATraineeEvaluation = await _iLATraineeEvaluationService.GetAsync();
            return Ok( new { iLATraineeEvaluation });
        }

        [HttpGet]
        [Route("/ilatraineeevaluation/ila/{id}")]
        public async Task<IActionResult> GetTraineeEvaluationByILA(int id)
        {
            var result = await _iLATraineeEvaluationService.GetTraineeEvaluationByILAAsync(id);
            return Ok( new { result });
        }
        [HttpPost]
        [Route("/ilatraineeevaluation/status")]
        public async Task<IActionResult> ChangeTraineeEvaluationStatus(TraineeEvaluationStatusVM data)
        {
            var result = await _iLATraineeEvaluationService.ChangeTraineeEvaluationStatus(data);
            return Ok( new { result });
        }
        
        [HttpDelete]
        [Route("/ilatraineeevaluation/perform/ila/{id}")]
        public async Task<IActionResult> RemovePerformTraineeEvaluationByILA(int id)
        {
            var result = await _iLATraineeEvaluationService.RemoveTraineeTypeIlaIdAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new ILA Trainee Evaluation
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/ilatraineeevaluation")]
        public async Task<IActionResult> CreateILATraineeEvaluationAsync(ILATraineeEvaluationCreateOptions options)
        {
            var result = await _iLATraineeEvaluationService.CreateAsync(options);
            return Ok( new { result, message = _localizer["iLATraineeEvaluationCreated"].Value });
        }

        /// <summary>
        /// Gets a specific ILA Trainee Evaluation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with ILA Trainee Evaluation</returns>
        [HttpGet]
        [Route("/ilatraineeevaluation/{id}")]
        public async Task<IActionResult> GetILATraineeEvaluationTypeAsync(int id)
        {
            var iLATraineeEvaluation = await _iLATraineeEvaluationService.GetAsync(id);
            return Ok( new { iLATraineeEvaluation });
        }

        /// <summary>
        /// Updates  a specific ILA Trainee Evaluation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/ilatraineeevaluation/{id}")]
        public async Task<IActionResult> UpdateILATraineeEvaluationAsync(int id, ILATraineeEvaluationCreateOptions options)
        {
            var iLATraineeEvaluation = await _iLATraineeEvaluationService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["iLATraineeEvaluationUpdated"].Value });
        }

        [HttpGet]
        [Route("/ilatraineeevaluation/{id}/copy")]
        public async Task<IActionResult> CopyTraineeEvaluation(int id)
        {
            var result = await _iLATraineeEvaluationService.CopyTraineeEvaluationAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific ILA Trainee Evaluation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/ilatraineeevaluation/{id}")]
        public async Task<IActionResult> DeleteILATraineeEvaluationAsync(int id, ILATraineeEvaluationOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _iLATraineeEvaluationService.InActiveAsync(id);
                    break;
                case "active":
                    await _iLATraineeEvaluationService.ActiveAsync(id);
                    break;
                case "delete":
                    await _iLATraineeEvaluationService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"iLATraineeEvaluation-{options.ActionType.ToLower()}"].Value });
        }
    }
}
