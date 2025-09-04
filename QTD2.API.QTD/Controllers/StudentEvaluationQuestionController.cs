using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.StudentEvaluationQuestion;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEvaluationQuestionController : ControllerBase
    {
        private readonly IStudentEvaluationQuestionService _studentEvaluationQuestionService;
        private readonly IStringLocalizer<StudentEvaluationQuestionController> _localizer;

        public StudentEvaluationQuestionController(IStudentEvaluationQuestionService studentEvaluationQuestionService, IStringLocalizer<StudentEvaluationQuestionController> localizer)
        {
            _studentEvaluationQuestionService = studentEvaluationQuestionService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of StudentEvaluationQuestion
        /// </summary>
        /// <returns>Http Response Code with StudentEvaluationQuestion</returns>
        [HttpGet]
        [Route("/studentEvaluationQuestion")]
        public async Task<IActionResult> GetStudentEvaluationQuestionAsync()
        {
            var result = await _studentEvaluationQuestionService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new StudentEvaluationQuestion
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/studentEvaluationQuestion")]
        public async Task<IActionResult> CreateStudentEvaluationQuestionAsync(StudentEvaluationQuestionCreateOptions options)
        {
            var result = await _studentEvaluationQuestionService.CreateAsync(options);
            return Ok( new { message = _localizer["StudentEvaluationQuestionCreated"].Value });
        }

        /// <summary>
        /// Gets a specific StudentEvaluationQuestion by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with StudentEvaluationQuestion</returns>
        [HttpGet]
        [Route("/studentEvaluationQuestion/{id}")]
        public async Task<IActionResult> GetStudentEvaluationQuestionAsync(int id)
        {
            var result = await _studentEvaluationQuestionService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/studentEvaluationQuestion/eval/{id}")]
        public async Task<IActionResult> GetStudentEvalQuestionsForEvalAsync(int id)
        {
            var result = await _studentEvaluationQuestionService.GetStudentEvalQuestionsForEvalAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific StudentEvaluationQuestion by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/studentEvaluationQuestion/{id}")]
        public async Task<IActionResult> UpdateStudentEvaluationQuestionAsync(int id, StudentEvaluationQuestionUpdateOptions options)
        {
            var result = await _studentEvaluationQuestionService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["StudentEvaluationQuestionUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific StudentEvaluationQuestion by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/studentEvaluationQuestion/{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationQuestionAsync(int id, StudentEvaluationQuestionOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _studentEvaluationQuestionService.InActiveAsync(id);
                    break;
                case "active":
                    await _studentEvaluationQuestionService.ActiveAsync(id);
                    break;
                case "delete":
                    await _studentEvaluationQuestionService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"StudentEvaluationQuestion-{options.ActionType.ToLower()}"].Value });
        }
    }
}
