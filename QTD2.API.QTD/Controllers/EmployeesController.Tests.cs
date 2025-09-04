using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QTD2.Infrastructure.Model.EmployeeTest;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        [HttpGet]
        [Route("/employees/tests")]
        public async Task<IActionResult> GetEmployeesTestAsync()
        {
            var employeeTest = await _classScheduleRosterService.GetEmployeesTestAsync();
            return Ok( new { employeeTest });
        }
        [HttpPost]
        [Route("/employees/tests")]
        public async Task<IActionResult> SaveTestResponseAsync(EmpTestCreateOptions options)
        {
            var employeeTest = await _classScheduleRosterService.CreateRosterResponseAsync(options);
            return Ok( new { employeeTest, message = _localizer["empTestresponseCreated"] });
        }

        [HttpGet]
        [Route("/employees/class/{classId}/roster/{rosterId}/test/{testId}/question/{questionId}")]
        public async Task<IActionResult> GetEmployeesTestItemWithAnswerAsync(int classId, int testId, int questionId, int rosterId)
        {
            var employeeTest = await _classScheduleRosterService.GetTestAnswerAsync(classId, testId, questionId, rosterId);
            return Ok( new { employeeTest });
        }
        [HttpGet]
        [Route("/employees/class/roster/{rosterId}")]
        public async Task<IActionResult> ReviewTestAsync(int rosterId)
        {
            var employeeTest = await _classScheduleRosterService.ReviewTestAsync(rosterId);
            return Ok( new { employeeTest });
        }

        [HttpGet]
        [Route("/employees/class/{classId}/roster/{rosterId}/submittest/{testId}/compDate/{completionDate}")]
        public async Task<IActionResult> SubmitTestAsync(int classId, int testId, int rosterId, DateTime completionDate)
        {
            var employeeTest = await _classScheduleRosterService.SubmitTestAsync(classId, testId, rosterId, completionDate);
            return Ok( new { employeeTest });
        }

        [HttpGet]
        [Route("/employees/class/exit/roster/{rosterId}")]
        public async Task<IActionResult> ExitTestAsync(int rosterId)
        {
            var employeeTest = await _classScheduleRosterService.ExitTestAsync(rosterId);
            return Ok( new { employeeTest });
        }
    }
}
