using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.StudentEvaluationForm;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEvaluationController : EMPController
    {

        private readonly IStudentEvaluationService _studentEvaluationService;
        private readonly IStringLocalizer<StudentEvaluationController> _localizer;

        public StudentEvaluationController(
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeService employeeService, 
            IStudentEvaluationService studentEvaluationService,
            IStringLocalizer<StudentEvaluationController> localizer) 
            : base(userManager, employeeService, httpContextAccessor)
        {
            _studentEvaluationService = studentEvaluationService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/emp/student-evaluations")]
        public async Task<IActionResult> GetEmployeeEvaluationsAsync()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _studentEvaluationService.GetEvaluationsByIdAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/student-evaluations/start/{evaluationId}")]
        public async Task<IActionResult> StartEmployeeEvaluationsAsync(int evaluationId)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _studentEvaluationService.StartEvaluationAsyncByIdAsync(evaluationId, employeeId);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/emp/student-evaluations/complete")]
        public async Task<IActionResult> CompleteEmployeeEvaluationsAsync(ClassSchedule_Evaluation_RosterOptions options)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _studentEvaluationService.CompleteEvaluationAsyncByIdAsync(options, employeeId);
            return Ok( new { result });
        }
    }
}
