using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Dashboard;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Employee;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDashboardController : ControllerBase
    {

        private readonly IStringLocalizer<EmpDashboardController> _localizer;
        private readonly IDashboardService _dashboardService;

        public EmpDashboardController(IStringLocalizer<EmpDashboardController> localizer, IDashboardService dashboardService)
        {
            _localizer = localizer;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("/dashboard/trainingDue/{date}")]
        public async Task<IActionResult> GetDueTrainingsData(string date)
        {
            var result = await _dashboardService.GetDueTrainingsData(date);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/dashboard/getEMPSettings/{ilaId}")]
        public async Task<IActionResult> GetDueTrainingsData(int ilaId)
        {
            var result = await _dashboardService.GetEMPSettings(ilaId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/dashboard/checkCourseAvailability")]
        public async Task<IActionResult> CheckCourseAvailabilityForSelfRegestration()
        {
            var result = await _dashboardService.CheckCourseAvailabilityForSelfRegestration();
            return Ok( new { result });
        }
        

        [HttpGet]
        [Route("/dashboard/trainingSchedule/inProgress/{startDate}/{endDate}")]
        public async Task<IActionResult> GetInProgressSchedules(DateTime startDate, DateTime endDate)
        {
            var result = await _dashboardService.GetTrainingScheduleFinalInProgress(startDate,endDate);
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/dashboard/trainingSchedule/today/{date}")]
        public async Task<IActionResult> GetTodaysSchedules(DateTime date)
        {
            var result = await _dashboardService.GetTrainingScheduleToday(date);
            return Ok( new { result });
        }
        
        [HttpPost]
        [Route("/dashboard/trainingSchedule/final")]
        public async Task<IActionResult> GetTrainingScheduleFinal(GetDueTrainingOptions options)
        {
            var result = await _dashboardService.GetTrainingScheduleFinal(options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/dashboard/class/{id}/info")]
        public async Task<IActionResult> GetClassInfo(int id)
        {
            var result = await _dashboardService.GetClassInfoAsync(id);
            return Ok( new { result });
        }
        [HttpGet]
        [Route("/dashboard/ila/{id}/info")]
        public async Task<IActionResult> GetCourseInfo(int id)
        {
            var result = await _dashboardService.GetCourseInfoByILAId(id);
            return Ok( new { result });
        }

    }
}
