using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.EvalutionType;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeEvaluationTypeController : ControllerBase
    {
        private readonly ITraineeEvaluationTypeService _evaluationTypeService;
        private readonly IStringLocalizer<TraineeEvaluationTypeController> _localizer;

        public TraineeEvaluationTypeController(ITraineeEvaluationTypeService evaluationTypeService, IStringLocalizer<TraineeEvaluationTypeController> localizer)
        {
            _evaluationTypeService = evaluationTypeService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of EvaluationTypes
        /// </summary>
        /// <returns>Http Response Code with EvaluationTypes</returns>
        [HttpGet]
        [Route("/traineEvalTypes")]
        public async Task<IActionResult> GetEvaluationTypesAsync()
        {
            var result = await _evaluationTypeService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new TraineeEvaluationType
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/traineEvalTypes")]
        public async Task<IActionResult> CreateEvaluationTypeAsync(TraineeEvaluationTypeCreateOptions options)
        {
            var result = await _evaluationTypeService.CreateAsync(options);
            return Ok( new { message = _localizer["EvaluationTypeCreated"].Value });
        }

        /// <summary>
        /// Gets a specific TraineeEvaluationType by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with TraineeEvaluationType</returns>
        [HttpGet]
        [Route("/traineEvalTypes/{id}")]
        public async Task<IActionResult> GetEvaluationTypeAsync(int id)
        {
            var result = await _evaluationTypeService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific TraineeEvaluationType by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/traineEvalTypes/{id}")]
        public async Task<IActionResult> UpdateEvaluationTypeAsync(int id, TraineeEvaluationTypeUpdateOptions options)
        {
            var result = await _evaluationTypeService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["EvaluationTypeUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific TraineeEvaluationType by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/traineEvalTypes/{id}")]
        public async Task<IActionResult> DeleteEvaluationTypeAsync(int id, TraineeEvaluationTypeOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _evaluationTypeService.InActiveAsync(id);
                    break;
                case "active":
                    await _evaluationTypeService.ActiveAsync(id);
                    break;
                case "delete":
                    await _evaluationTypeService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"TraineeEvaluationType-{options.ActionType.ToLower()}"].Value });
        }
    }
}
