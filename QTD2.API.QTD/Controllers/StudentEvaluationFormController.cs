using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.StudentEvaluationForm;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEvaluationFormController : ControllerBase
    {
        private readonly IStudentEvaluationFormService _studentEvaluationFormService;
        private readonly IStringLocalizer<StudentEvaluationFormController> _localizer;

        public StudentEvaluationFormController(IStudentEvaluationFormService studentEvaluationFormService, IStringLocalizer<StudentEvaluationFormController> localizer)
        {
            _studentEvaluationFormService = studentEvaluationFormService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of StudentEvaluationForms
        /// </summary>
        /// <returns>Http Response Code with StudentEvaluationForms</returns>
        [HttpGet]
        [Route("/studentEvaluationForm")]
        public async Task<IActionResult> GetStudentEvaluationFormsAsync()
        {
            var result = await _studentEvaluationFormService.GetAsync();
            return Ok( new { result });
        }

        /// <summary>
        /// Creates a new StudentEvaluationForm
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/studentEvaluationForm")]
        public async Task<IActionResult> CreateStudentEvaluationFormAsync(StudentEvaluationFormCreateOptions options)
        {
            var result = await _studentEvaluationFormService.CreateAsync(options);
            return Ok( new { result, message = _localizer["StudentEvaluationFormCreated"] });
        }

        /// <summary>
        /// Gets a specific StudentEvaluationForm by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with StudentEvaluationForm</returns>
        [HttpGet]
        [Route("/studentEvaluationForm/{id}")]
        public async Task<IActionResult> GetStudentEvaluationFormAsync(int id)
        {
            var result = await _studentEvaluationFormService.GetAsync(id);
            return Ok( new { result });
        }

        /// <summary>
        /// Updates  a specific StudentEvaluationForm by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/studentEvaluationForm/{id}")]
        public async Task<IActionResult> UpdateStudentEvaluationFormAsync(int id, StudentEvaluationFormUpdateOptions options)
        {
            var result = await _studentEvaluationFormService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["StudentEvaluationFormUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific StudentEvaluationForm by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/studentEvaluationForm/{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationFormAsync(int id, StudentEvaluationFormOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _studentEvaluationFormService.InActiveAsync(id);
                    break;
                case "active":
                    await _studentEvaluationFormService.ActiveAsync(id);
                    break;
                case "delete":
                    await _studentEvaluationFormService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"StudentEvaluationForm-{options.ActionType.ToLower()}"].Value });
        }
    }
}
