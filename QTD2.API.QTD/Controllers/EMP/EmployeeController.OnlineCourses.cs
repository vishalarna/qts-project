using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model;

namespace QTD2.API.QTD.Controllers.EMP
{
    public partial class EmployeeController : EMPController
    {
        [HttpGet]
        [Route("/emp/onlinecourse/completed")]
        [ProducesResponseType(typeof(PagedResult<ClassSchedule_Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompletedCoursesAsync([FromQuery] PagedQuery query)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _onlineCourseService.GetCompletedCoursesAsync(employeeId, query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("/emp/onlinecourse/pending")]
        [ProducesResponseType(typeof(PagedResult<ClassSchedule_Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPendingCoursesAsync([FromQuery] PagedQuery query)
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _onlineCourseService.GetPendingCoursesAsync(employeeId, query);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
