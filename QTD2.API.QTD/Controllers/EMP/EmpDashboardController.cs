using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers.EMP
{

    [Route("api/[controller]")]
    [ApiController]
    public partial class EmpDashboardController : EMPController
    {
        private readonly IStringLocalizer<EmpDashboardController> _localizer;
        private readonly IDashboardService _dashboardService;

        public EmpDashboardController(
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeService employeeService,
            IStringLocalizer<EmpDashboardController> localizer, 
            IDashboardService dashboardService) 
            : base(userManager, employeeService, httpContextAccessor)
        {
            _localizer = localizer;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("/emp/dashboard/statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dashboardService.GetDashboardStatisticsByIdAsync(employeeId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/emp/dashboard/checkCourseAvailability")]
        public async Task<IActionResult> CheckCourseAvailabilityForSelfRegestration()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dashboardService.CheckCourseAvailabilityForSelfRegestration(employeeId);
            return Ok( new { result });
        }


        [HttpGet]
        [Route("/emp/dashboard/curr/name")]
        public async Task<IActionResult> GetCurrentEmployeeName()
        {
            var employeeId = await GetEmployeeIdAsync();
            var result = await _dashboardService.GetCurrentEmployeeName(employeeId);
            return Ok( new { result });
        }
    }
}
