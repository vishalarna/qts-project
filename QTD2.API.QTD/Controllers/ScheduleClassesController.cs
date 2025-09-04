using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.ClassScheduleHistory;
using QTD2.Infrastructure.Model.Reports;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ScheduleClassesController : ControllerBase
    {
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClassScheduleHistoryService _classScheduleHistoryService;
        private readonly IClassRosterService _classRoster;
        private readonly IStudentEvaluationWithoutEmpService _studentEvaluationWithoutEmp;
        private readonly IStringLocalizer<PositionsController> _localier;
        private readonly IHasher _hasher;
        private readonly IReportsService _reportsService;
        private readonly IReportGeneratorService _reportGeneratorService;

        public ScheduleClassesController(
           IClassScheduleService classScheduleService,
           IStringLocalizer<PositionsController> localier,
           IClassScheduleHistoryService classScheduleHistoryService, IClassRosterService classRoster, IStudentEvaluationWithoutEmpService studentEvaluationWithoutEmp, IHasher hasher, IReportsService reportsService, IReportGeneratorService reportGeneratorService)
        {
            _classScheduleService = classScheduleService;
            _classScheduleHistoryService = classScheduleHistoryService;
            _classRoster = classRoster;
            _localier = localier;
            _studentEvaluationWithoutEmp = studentEvaluationWithoutEmp;
            _hasher = hasher;
            _reportsService = reportsService;
            _reportGeneratorService = reportGeneratorService;
        }

        [HttpGet]
        [Route("/schedules/startDate/{startDateTime}/endDate/{endDateTime}")]
        public async Task<IActionResult> GetAsync(DateTime startDateTime, DateTime endDateTime)
        {
            var schedules = await _classScheduleService.GetByStartDateAndEndDateAsync(startDateTime,endDateTime);
            return Ok( new { schedules });
        }

        [HttpGet]
        [Route("/schedules/byILA/{ilaId}")]
        public async Task<IActionResult> GetByILAId(int ilaId)
        {
            var schedules = await _classScheduleService.GetByILAIdAsync(ilaId);
            return Ok( new { schedules });
        }

   
        [HttpGet]
        [Route("/schedules/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var schedule = await _classScheduleService.GetAsync(id);
            return Ok( new { schedule });
        }

        [HttpGet]
        [Route("/schedules/stats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _classScheduleService.GetStatsAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/{ilaId}/eval/{classId}")]
        public async Task<IActionResult> GetLinkedStudentEvals(int ilaId,int classId)
        {
            var result = await _classScheduleService.GetLinkedStudentEvalsAsync(ilaId,classId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/selfreg")]
        public async Task<IActionResult> GetSelfRegNeedingApproval()
        {
            var result = await _classScheduleService.GetSelfRegNeedingApprovalAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/rerelease")]
        public async Task<IActionResult> GetTestNeedingReRelease()
        {
            var result = await _classScheduleService.GetTestNeedingReReleaseAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/retake/release")]
        public async Task<IActionResult> GetRetakeToRelease()
        {
            var result = await _classScheduleService.GetRetakeToReleaseAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/schedules/needsc")]
        public async Task<IActionResult> GetILANeedingToBeScheduled()
        {
            var result = await _classScheduleService.GetILANeedingToBeScheduled();
            return Ok( new {  result });
        }

        [HttpGet]
        [Route("/schedules/waiting")]
        public async Task<IActionResult> GetWaitlistedData()
        {
            var result = await _classScheduleService.GetWaitlistedDataAsync();
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/schedules/addto/waitlist")]
        public async Task<IActionResult> WaitlistStudent(ClassScheduleEnrollOptions options)
        {
            await _classScheduleService.WaitListStudentAsync(options);
            return Ok( new { message = _localier["EmployeeWaitListed"] });
        }

        [HttpPut]
        [Route("/schedules/addto/enroll")]
        public async Task<IActionResult> EnrollStudent(ClassScheduleEnrollOptions options)
        {
            await _classScheduleService.EnrollStudentAsync(options);
            return Ok( new { message = _localier["EmployeeEnrolled"] });
        }
        [HttpPut]
        [Route("/schedules/addto/enroll/extendclasssize")]
        public async Task<IActionResult> EnrollStudentWithClassSizeByPass(ClassScheduleEnrollOptions options)
        {
            await _classScheduleService.EnrollStudentWithClassSizeByPassAsync(options);
            return Ok( new { message = _localier["EmployeeEnrolled"] });
        }

        [HttpDelete]
        [Route("/schedules/decline/emp")]
        public async Task<IActionResult> DeclineEmployee(ClassScheduleEnrollOptions options)
        {
            await _classScheduleService.DeclineEmployee(options);
            return Ok( new { message = _localier["EmployeeDeclinedEnrollment"] });
        }


        [HttpPost]
        [Route("/schedules")]
        public async Task<IActionResult> CreateAsync(ClassScheduleCreateOptions options)
        {
            var schedule = await _classScheduleService.CreateAsync(options);
            var histOptions = new ClassScheduleHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "New Class Schedule Created for ILA" + schedule.ILA.Name;
            histOptions.ClassScheduleId = schedule.Id;
            await _classScheduleHistoryService.CreateAsync(histOptions);
            var classData = _classScheduleService.MapClassScheduleToClassScheduleDetailVM(schedule);
            return Ok( new { classData, message = _localier["ClassScheduleCreated"] });
        }

        [HttpGet]
        [Route("/schedules/{classId}/recurrence/{includeCurrentClass}")]
        public async Task<IActionResult> GetRecurrences(int classId,bool includeCurrentClass)
        {
            var result = await _classScheduleService.GetRecurrencesAsync(classId, includeCurrentClass);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/schedules/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ClassScheduleCreateOptions options)
        {
         
            var schedule = await _classScheduleService.UpdateAsync(id, options);

            var histOptions = new ClassScheduleHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "Class Schedule Updated for ILA" + schedule.ILA.Name;
            histOptions.ClassScheduleId = schedule.Id;
            await _classScheduleHistoryService.CreateAsync(histOptions);
            var classData = _classScheduleService.MapClassScheduleToClassScheduleDetailVM(schedule);
            return Ok( new { classData, message = _localier["ScheduleUpdated"] });
        }

        [HttpDelete]
        [Route("/schedules/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
      
            var schedule = await _classScheduleService.DeleteAsync(id);
            var histOptions = new ClassScheduleHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "Class Schedule Deleted for ILA" + schedule.ILA.Name;
            histOptions.ClassScheduleId = schedule.Id;
            await _classScheduleHistoryService.CreateAsync(histOptions);
            return Ok( new { message = _localier["ScheduleDeleted"] });
        }

        [HttpGet]
        [Route("/schedules/class/{ilaId}")]
        public async Task<IActionResult> GetClassSchedulesByILA(int ilaId)
        {
            var schedule = await _classScheduleService.GetClassSchedulesByILA(ilaId);
            return Ok( new { schedule });
        }

        [HttpPut]
        [Route("/schedules/{id}/training")]
        public async Task<IActionResult> UpdateTrainingAsync(int id, ClassScheduleCreateOptions options)
        {

            var schedule = await _classScheduleService.UpdateTrainingAsync(id, options);

            var histOptions = new ClassScheduleHistoryCreateOptions();
            histOptions.ChangeEffectiveDate = DateTime.UtcNow;
            histOptions.ChangeNotes = "Class Schedule Updated for ILA" + schedule.ILA.Name;
            histOptions.ClassScheduleId = schedule.Id;
            await _classScheduleHistoryService.CreateAsync(histOptions);

            return Ok( new { message = _localier["ScheduleUpdated"] }); 
        }

        [HttpPut]
        [Route("/schedules/rerelease")]
        public async Task<IActionResult> ReReleaseTest(ReReleaseOptions options)
        {
            await _classScheduleService.ReReleaseTest(options);
            return Ok( new { message = _localier["Test Released Again"] });
        }

        [HttpGet]
        [Route("/schedules/class/{classId}/ila/{ilaId}")]
        public async Task<IActionResult> GetClassScheduleReviewData(int classId,int ilaId)
        {
            var schedule = await _classScheduleService.GetClassScheduleReviewData(classId,ilaId);
            return Ok( new { schedule });
        }

        [HttpPost]
        [Route("/schedules/generateReport/classroster")]
        public async Task<IActionResult> GenerateClassRosterReportAsync(ExportReportModel model)
        {
            var classFilter = model.Options.Filters.FirstOrDefault(x => x.Name.ToUpper() == "TRAINING CLASSES");
            if (classFilter != null && !string.IsNullOrEmpty(classFilter.Value))
            {
                var encodedIds = classFilter.Value.Split(",").ToList();
                classFilter.Value = String.Join(",", encodedIds.Select(x => _hasher.Decode(x)));
            }
            else
            {
                throw new QTDServerException("Class Id is missing in the given input");
            }
            var report = await _reportsService.CreateReportAsync(model.Options, false);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

        [HttpPost]
        [Route("/schedules/generateReport/classsigninsheet")]
        public async Task<IActionResult> GenerateClassSignInSheetReportAsync(ExportReportModel model)
        {
            var classFilter = model.Options.Filters.FirstOrDefault(x => x.Name.ToUpper() == "TRAINING CLASSES");
            if (classFilter != null && !string.IsNullOrEmpty(classFilter.Value))
            {
                var encodedIds = classFilter.Value.Split(",").ToList();
                classFilter.Value = String.Join(",", encodedIds.Select(x => _hasher.Decode(x)));
            }
            else
            {
                throw new QTDServerException("Class Id is missing in the given input");
            }
            var report = await _reportsService.CreateReportAsync(model.Options, false);
            var file = await _reportGeneratorService.ExportReportAsync(model.ExportType, report);
            var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = Path.GetFileName(file) }.ToString());
            return File(
                fileStream: fs,
                contentType: System.Net.Mime.MediaTypeNames.Application.Octet,
                fileDownloadName: Path.GetFileName(file));
        }

    }
}
