using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.StudentEvaluationAudience;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEvaluationAudienceController : Controller
    {
        private readonly IStudentEvaluationAudienceService _studentEvaluationAudienceService;
        private readonly IStringLocalizer<StudentEvaluationAudienceController> _localizer;

        public StudentEvaluationAudienceController(IStudentEvaluationAudienceService studentEvaluationAudienceService, IStringLocalizer<StudentEvaluationAudienceController> localizer)
        {
            _studentEvaluationAudienceService = studentEvaluationAudienceService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Student Evaluation Audience
        /// </summary>
        /// <returns>Http Response Code with Student Evaluation Audiences</returns>
        [HttpGet]
        [Route("/studentevaluationaudience")]
        public async Task<IActionResult> GetStudentEvaluationAudienceAsync()
        {
            var studentEvaluationAudience = await _studentEvaluationAudienceService.GetAsync();
            return Ok( new { studentEvaluationAudience });
        }

        /// <summary>
        /// Creates a new Student Evaluation Audience
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/studentevaluationaudience")]
        public async Task<IActionResult> CreateStudentEvaluationAudienceAsync(StudentEvaluationAudienceCreateOptions options)
        {
            var result = await _studentEvaluationAudienceService.CreateAsync(options);
            return Ok( new { message = _localizer["studentEvaluationAudienceCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Student Evaluation Audience by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Student Evaluation Audience</returns>
        [HttpGet]
        [Route("/studentevaluationaudience/{id}")]
        public async Task<IActionResult> GetStudentEvaluationAudienceTypeAsync(int id)
        {
            var studentEvaluationAudience = await _studentEvaluationAudienceService.GetAsync(id);
            return Ok( new { studentEvaluationAudience });
        }

        /// <summary>
        /// Updates  a specific Student Evaluation Audience by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/studentevaluationaudience/{id}")]
        public async Task<IActionResult> UpdateStudentEvaluationAudienceAsync(int id, StudentEvaluationAudienceUpdateOptions options)
        {
            var studentEvaluationAudience = await _studentEvaluationAudienceService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["studentEvaluationAudienceUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Student Evaluation Audience by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/studentevaluationaudience/{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationAudienceAsync(int id, StudentEvaluationAudienceOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _studentEvaluationAudienceService.InActiveAsync(id);
                    break;
                case "active":
                    await _studentEvaluationAudienceService.ActiveAsync(id);
                    break;
                case "delete":
                    await _studentEvaluationAudienceService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"studentEvaluationAudience-{options.ActionType.ToLower()}"].Value });
        }
    }
}
