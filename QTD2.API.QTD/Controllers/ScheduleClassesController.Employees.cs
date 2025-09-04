using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using RazorEngine.Compilation.ImpromptuInterface;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ScheduleClassesController : ControllerBase
    {
        [HttpPost]
        [Route("/schedules/{id}/emp")]
        public async Task<IActionResult> LinkEmployee(int id, ClassSchedule_EmployeeCreateOptions options)
        {
            var result = await _classScheduleService.LinkEmployee(id, options);

            var linkEmployeeResult = new LinkEmployeeResult();

            foreach (var employeeId in options.employeeIds)
            {
                var classScheduleEmployee = result.ClassSchedule_Employee.Where(r => r.EmployeeId == employeeId).FirstOrDefault();

                if (classScheduleEmployee != null)
                    linkEmployeeResult.AddLinkedEmployee(employeeId, options.classScheduleId, classScheduleEmployee.Id);
            }

            return Ok(new { linkEmployeeResult });
        }

        [HttpGet]
        [Route("/schedules/{id}/emp")]
        public async Task<IActionResult> GetLinkedEmployees(int id)
        {
            var result = await _classScheduleService.GetLinkedEmployees(id);
            return Ok(new { result });
        }


        [HttpDelete]
        [Route("/schedules/{scheduleId}/emp/")]
        public async Task<IActionResult> UnlinkEmployee(int scheduleId, ClassSchedule_EmployeeCreateOptions options)
        {
            await _classScheduleService.UnlinkEmployee(scheduleId, options.employeeIds);
            return Ok(new { message = _localier["EmployeeUnlinked"].Value });
        }


        [HttpGet]
        [Route("/schedules/emp/{id}")]
        public async Task<IActionResult> GetSchedulesEmployeesIsLinkedTo(int id)
        {
            var result = await _classScheduleService.GetClassSchedulesEmployeeIsLinkedTo(id);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/schedules/{classId}/bulkgrade/emp/")]
        public async Task<IActionResult> UpdateBulkGradeAsync(int classId, ClassScheduleGradeCreateOptions options)
        {
            var result = await _classScheduleService.UpdateBulkGradeAsync(classId, options);
            return Ok(new { result });
        }


        [HttpPut]
        [Route("/schedules/{id}/grade/emp/")]
        public async Task<IActionResult> UpdateGradeAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var result = await _classScheduleService.UpdateGradeAsync(id, options);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/schedules/{id}/score/emp/")]
        public async Task<IActionResult> UpdateScoreAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var result = await _classScheduleService.UpdateScoreAsync(id, options);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/schedules/{id}/notes/emp/")]
        public async Task<IActionResult> UpdateNotesAsync(int id, ClassScheduleGradeCreateOptions options)
        {
            var result = await _classScheduleService.UpdateNotesAsync(id, options);
            return Ok(new { result });
        }

        [HttpPut]
        [Route("/schedules/{id}/enrollment/emp")]
        public async Task<IActionResult> UpdateEnrollmentAsync(int id, ClassScheduleEnrollmentOptions options)
        {
            var result = await _classScheduleService.UpdateClassScheduleEnrollmentOptions(id, options);
            return Ok(new { result });
        }
    }
}
