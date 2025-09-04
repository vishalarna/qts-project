using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ScheduleClassesController : ControllerBase
    {
        [HttpPost]
        [Route("/schedules/roster")]
        public async Task<IActionResult> CreateRoster(ClassRoasterModel options)
        {
            var result = await _classRoster.CreateRoster(options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/schedules/roster/fetch/type")]
        public async Task<IActionResult> GetRoasterEmployeesAsync(RosterFetchOptions options)
        {
            var result = await _classRoster.GetRoasterEmployeesAsync(options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/{classId}/roster/overview")]
        public async Task<IActionResult> GetRosterOverviewData(int classId)
        {
            var result = await _classRoster.GetRosterOverviewDataAsync(classId);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/schedules/{rosterId}/bulkgrade/roster/{testId}")]
        public async Task<IActionResult> UpdateBulkGradeAsync(int rosterId, int testId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.UpdateBulkGradeAsync(rosterId, testId, options);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/schedules/{empId}/grade/roster/")]
        public async Task<IActionResult> UpdateGradeAsync(int empId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.UpdateGradeAsync(empId, options);
            return Ok( new { result });
        }
        [HttpPut]
        [Route("/schedules/{rosterId}/score/roster/")]
        public async Task<IActionResult> UpdateScoreAsync(int rosterId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.UpdateScoreAsync(rosterId, options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/schedules/{rosterId}/compDate/roster/")]
        public async Task<IActionResult> UpdateCompDateAsync(int rosterId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.UpdateCompDateAsync(rosterId, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/schedules/{classId}/roster/{testId}/{testType}/employee/{empId}")]
        public async Task<IActionResult> RemoveEmployeeFromRoster(int classId, int testId, string testType,int empId)
        {
            await _classRoster.RemoveEmployeeAsync(classId, testId, testType, empId);
            return Ok( new { message = _localier["EmployeeRemovedFromRoster"] });
        }

        [HttpPut]
        [Route("/schedules/{empId}/release/")]
        public async Task<IActionResult> ReleaseTestAsync(int empId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.ReleaseTestAsync(empId, options);
            return Ok( new { result });
        }
        [HttpPut]
        [Route("/schedules/{empId}/recall/")]
        public async Task<IActionResult> RecallTestAsync(int empId, ClassRoasterUpdateOptions options)
        {
            var result = await _classRoster.RecallTestAsync(empId, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/schedules/emp/evaluation")]
        public async Task<IActionResult> SubmitEvaluation(StudentEvaluationSubmitOptions options)
        {
            await _classRoster.SubmitEvaluationAsync(options.ClassId,options.EvaluationId,options.EmployeeId);
            return Ok( new { message = _localier["EvaluationSubmitted"] });
        }
    }
}
