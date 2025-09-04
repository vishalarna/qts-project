using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace QTD2.API.QTD.Controllers
{
    public partial class EmployeesController : ControllerBase
    {
        [HttpGet]
        [Route("/employees/selfreg/available/currentDateTime/{currentDateTime}")]
        public async Task<IActionResult> GetSelfRegAvailableCourses(DateTime currentDateTime)
        {
            var result = await _classScheduleService.GetSelfRegAvailableCoursesAsync(currentDateTime);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/selfreg/{classId}/ila/{ilaId}")]
        public async Task<IActionResult> RegisterCourse(int classId, int ilaId)
        {
            await _classScheduleService.RegisterBySelfRegistrationAsync(classId,ilaId);
            return Ok( new { message = _localizer["EmployeeRegistered"]});
        }

        [HttpGet]
        [Route("/employees/selfreg/approved")]
        public async Task<IActionResult> GetSelfRegApprovedCourses()
        {
            var result = await _classScheduleService.GetSelfRegEmployeeAprovedCoursesAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/employees/selfreg/denied")]
        public async Task<IActionResult> GetSelfRegDeniedCourses()
        {
            var result = await _classScheduleService.GetSelfRegEmployeeDeniedCoursesAsync();
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/employees/selfreg/dropped")]
        public async Task<IActionResult> GetSelfRegDroppedCourses()
        {
            var result = await _classScheduleService.GetSelfRegEmployeeDroppedCoursesAsync();
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/employees/drop/{classId}/ila/{ilaId}")]
        public async Task<IActionResult> DropCourse(int classId, int ilaId)
        {
            await _classScheduleService.DropCourseAsync(classId, ilaId);
            return Ok( new { message = _localizer["EmployeeDropped"] });
        }
        [HttpPut]
        [Route("/employees/joiWaitlist/{classId}/ila/{ilaId}")]
        public async Task<IActionResult> JoinWaitlist(int classId, int ilaId)
        {
            await _classScheduleService.JoinWaitListAsync(classId, ilaId);
            return Ok( new { message = _localizer["EmployeeJoinedWaitList"] });
        }

        [HttpGet]
        [Route("/employees/class/{classId}/ila/{ilaId}")]
        public async Task<IActionResult> GetILAAndClassDetails(int classId, int ilaId)
        {
            var result = await _classScheduleService.ViewILAAndClassDetailAsync(classId,ilaId);
            return Ok( new { result });
        }
    }
}
