using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    public partial class ScheduleClassesController : ControllerBase
    {
        [HttpGet]
        [Route("/schedules/{id}/selfReg")]
        public async Task<IActionResult> GetClassScheduleSelfReg(int id)
        {
            var result = await _classScheduleService.GetClassSchedule_SelfRegistrationOptionsAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/schedules/selfReg")]
        public async Task<IActionResult> CreateClassScheduleSelfReg(ClassSchedule_RegistrationCreateOptions options)
        {
            var result = await _classScheduleService.CreateClassSchedule_SelfRegistrationAsync(options);
            return Ok( new { result });
        }
    }
}
