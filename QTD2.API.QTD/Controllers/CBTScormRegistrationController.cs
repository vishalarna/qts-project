using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Reports.Generation.Models;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CBTScormRegistrationController : ControllerBase
    {
        private readonly ICbtScormRegistrationService _cbtScormRegistrationService;

        public CBTScormRegistrationController(ICbtScormRegistrationService cbtScormRegistrationService)
        {
            _cbtScormRegistrationService = cbtScormRegistrationService;
        }

        [HttpPut]
        [Route("/cbtScormRegistrations/{classScheduleId}/bulkUpdate")]
        public async Task<IActionResult> BulkUpdateCbtRegistrationsAsync(int classScheduleId, ClassRoasterUpdateOptions options)
        {
            await _cbtScormRegistrationService.BulkUpdateCbtRegistrationsAsync(classScheduleId, options);
            return Ok();
        }


        [HttpPut]
        [Route("/cbtScormRegistrations/{employeeId}")]
        public async Task<IActionResult> UpdateCbtRegistrationAsync(int employeeId, ClassRoasterUpdateOptions options)
        {
            await _cbtScormRegistrationService.UpdateCbtRegistrationAsync(employeeId, options);
            return Ok();
        }
    }
}
