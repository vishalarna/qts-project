using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ClassSchedule_Roster_TimeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSchedule_Roster_TimeRecordController : ControllerBase
    {

        private readonly IClassSchedule_Roster_TimeRecordService _classSchedule_Roster_TimeRecordService;
        private readonly IStringLocalizer<ClassSchedule_Roster_TimeRecordController> _localizer;

        public ClassSchedule_Roster_TimeRecordController(IClassSchedule_Roster_TimeRecordService classSchedule_Roster_TimeRecordService,
        IStringLocalizer<ClassSchedule_Roster_TimeRecordController> localizer)
        {
            _classSchedule_Roster_TimeRecordService = classSchedule_Roster_TimeRecordService;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("/classscheduleroster-timerecord")]
        public async Task<IActionResult> CreateTimeRecordAsync(ClassSchedule_RosterTimeRecord_VM options)
        {
           var result = await _classSchedule_Roster_TimeRecordService.CreateTimeRecordAsync(options);
            return Ok();
        }
    }
}
