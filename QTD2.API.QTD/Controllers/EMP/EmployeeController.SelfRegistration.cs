using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{
   
    public partial class EmployeeController : EMPController
    {
        [HttpGet]
        [Route("/emp/selfreg/available")]
        public async Task<IActionResult> GetSelfRegAvailableCourses()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _classScheduleService.GetSelfRegAvailableCoursesByIdAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/selfreg/dropped")]
        public async Task<IActionResult> GetSelfRegDroppedCourses()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _classScheduleService.GetSelfRegEmployeeDroppedCoursesByIdAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/selfreg/approved")]
        public async Task<IActionResult> GetSelfRegApprovedCourses()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _classScheduleService.GetSelfRegEmployeeApprovedCoursesByIdAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/selfreg/denied")]
        public async Task<IActionResult> GetSelfRegDeniedCourses()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _classScheduleService.GetSelfRegEmployeeDeniedCoursesByIdAsync(employeeId);
            return Ok( new { result });
        }
    }
}
