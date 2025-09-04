using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.AssessmentTool;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentToolController : ControllerBase
    {
        private readonly IAssessmentToolService _assessmentToolService;
        private readonly IStringLocalizer<AssessmentToolController> _localizer;

        public AssessmentToolController(IAssessmentToolService assessmentToolService, IStringLocalizer<AssessmentToolController> localizer)
        {
            _assessmentToolService = assessmentToolService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of AssessmentTools
        /// </summary>
        /// <returns>Http Response Code with AssessmentTools</returns>
        [HttpGet]
        [Route("/assessmentTool")]
        public async Task<IActionResult> GetAssessmentToolsAsync()
        {
            var result = await _assessmentToolService.GetAsync();
            return Ok(new { result });
        }

        /// <summary>
        /// Creates a new AssessmentTool
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/assessmentTool")]
        public async Task<IActionResult> CreateAssessmentToolAsync(AssessmentToolCreateOptions options)
        {
            var result = await _assessmentToolService.CreateAsync(options);
            return Ok(new { message = _localizer["AssessmentToolCreated"].Value });
        }

        /// <summary>
        /// Gets a specific AssessmentTool by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with AssessmentTool</returns>
        [HttpGet]
        [Route("/assessmentTool/{id}")]
        public async Task<IActionResult> GetAssessmentToolAsync(int id)
        {
            var result = await _assessmentToolService.GetAsync(id);
            return Ok(new { result });
        }

        /// <summary>
        /// Updates  a specific AssessmentTool by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/assessmentTool/{id}")]
        public async Task<IActionResult> UpdateAssessmentToolAsync(int id, AssessmentToolUpdateOptions options)
        {
            var result = await _assessmentToolService.UpdateAsync(id, options);
            return Ok(new { message = _localizer["AssessmentToolUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific AssessmentTool by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/assessmentTool/{id}")]
        public async Task<IActionResult> DeleteAssessmentToolAsync(int id, AssessmentToolOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _assessmentToolService.InActiveAsync(id);
                    break;
                case "active":
                    await _assessmentToolService.ActiveAsync(id);
                    break;
                case "delete":
                    await _assessmentToolService.DeleteAsync(id);
                    break;
            }

            return Ok(new { message = _localizer[$"AssessmentTool-{options.ActionType.ToLower()}"].Value });
        }
    }
}
