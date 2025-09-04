using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;
using Microsoft.Extensions.Localization;
using QTD2.Domain;

namespace QTD2.API.QTD.Controllers.EMP
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassScheduleController : EMPController
    {
        private readonly IStringLocalizer<ClassScheduleController> _localizer;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassScheduleController(
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<ClassScheduleController> localizer,
            IEmployeeService employeeService,
            IClassScheduleService classScheduleService) 
            : base(userManager, employeeService, httpContextAccessor)
        {
            _localizer = localizer;
            _classScheduleService = classScheduleService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("/emp/classSchedule/{classScheduleId}/startCourse")]
        public async Task<IActionResult> StartCourseAsync(int classScheduleId)
        {
           var userClaims = _httpContextAccessor.HttpContext?.User.Claims;
           var employeeClaim = userClaims?.FirstOrDefault(r => r.Type == CustomClaimTypes.EmployeeId);
           var employeeId = employeeClaim == null ? -1 : Convert.ToInt32(employeeClaim.Value);
           var cbtRegistration = await _classScheduleService.GetCBT_ScormRegistrationAsync(classScheduleId, employeeId);

           return Ok( cbtRegistration);
        }
    }
}
