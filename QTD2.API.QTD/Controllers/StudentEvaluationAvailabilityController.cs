using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.StudentEvaluationAvailability;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEvaluationAvailabilityController : ControllerBase
    {
        private readonly IStudentEvaluationAvailabilityService _studentEvaluationAvailabilityService;
        private readonly IStringLocalizer<StudentEvaluationAvailabilityController> _localizer;

        public StudentEvaluationAvailabilityController(IStudentEvaluationAvailabilityService studentEvaluationAvailabilityService, IStringLocalizer<StudentEvaluationAvailabilityController> localizer)
        {
            _studentEvaluationAvailabilityService = studentEvaluationAvailabilityService;
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a list of Student Evaluation Availability
        /// </summary>
        /// <returns>Http Response Code with  Student Evaluation Availabilities</returns>
        [HttpGet]
        [Route("/studentevaluationavailability")]
        public async Task<IActionResult> GetStudentEvaluationAvailabilityAsync()
        {
            var studentEvaluationAvailabilities = await _studentEvaluationAvailabilityService.GetAsync();
            return Ok( new { studentEvaluationAvailabilities });
        }

        /// <summary>
        /// Creates a new Student Evaluation Availability
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/studentevaluationavailability")]
        public async Task<IActionResult> CreateStudentEvaluationAvailabilityAsync(StudentEvaluationAvailabilityCreateOptions options)
        {
            var result = await _studentEvaluationAvailabilityService.CreateAsync(options);
            return Ok( new { message = _localizer["StudentEvaluationAvailabilityCreated"].Value });
        }

        /// <summary>
        /// Gets a specific Student Evaluation Availability by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http Response Code with Student Evaluation Availability</returns>
        [HttpGet]
        [Route("/studentevaluationavailability/{id}")]
        public async Task<IActionResult> GetStudentEvaluationAvailabilityAsync(int id)
        {
            var studentEvaluationAvailability = await _studentEvaluationAvailabilityService.GetAsync(id);
            return Ok( new { studentEvaluationAvailability });
        }

        /// <summary>
        /// Updates  a specific Student Evaluation Availability by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/studentevaluationavailability/{id}")]
        public async Task<IActionResult> UpdateStudentEvaluationAvailabilityAsync(int id, StudentEvaluationAvailabilityUpdateOptions options)
        {
            var studentEvaluationAvailability = await _studentEvaluationAvailabilityService.UpdateAsync(id, options);
            return Ok( new { message = _localizer["studentEvaluationAvailabilityUpdated"].Value });
        }

        /// <summary>
        /// Deletes, Inactive or active  a specific Student Evaluation Availability by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/studentevaluationavailability/{id}")]
        public async Task<IActionResult> DeleteStudentEvaluationAvailabilityAsync(int id, StudentEvaluationAvailabilityOptions options)
        {
            switch (options.ActionType.ToLower())
            {
                case "inactive":
                default:
                    await _studentEvaluationAvailabilityService.InActiveAsync(id);
                    break;
                case "active":
                    await _studentEvaluationAvailabilityService.ActiveAsync(id);
                    break;
                case "delete":
                    await _studentEvaluationAvailabilityService.DeleteAsync(id);
                    break;
            }

            return Ok( new { message = _localizer[$"studentEvaluationAvailability-{options.ActionType.ToLower()}"].Value });
        }
    }
}
