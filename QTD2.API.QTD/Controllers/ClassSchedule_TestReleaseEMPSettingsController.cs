using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ClassSchedule_TestRelease_EmpSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSchedule_TestReleaseEMPSettingsController : ControllerBase
    {
        private readonly IClassSchedule_TestReleaseEMPSettingsService _classSchedule_TestReleaseEMPSettingsService;
        public ClassSchedule_TestReleaseEMPSettingsController(IClassSchedule_TestReleaseEMPSettingsService classSchedule_TestReleaseEMPSettingsService) {
            _classSchedule_TestReleaseEMPSettingsService = classSchedule_TestReleaseEMPSettingsService;
        }

        [HttpGet]
        [Route("/classschedule/testSettings/{id}")]

        public async Task<IActionResult> GetClassScheduleTestEmpSettings(int id)
        {
            var result = await _classSchedule_TestReleaseEMPSettingsService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/classschedule/testSettings/{classScheduleId}")]

        public async Task<IActionResult> CreateTestReleaseSettings(int classScheduleId)
        {
            try
            {
                var result = await _classSchedule_TestReleaseEMPSettingsService.CreateAsync(classScheduleId);
                return Ok( new { result });
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("/classschedule/testSettings/{classScheduleId}")]

        public async Task<IActionResult> UpdateTestReleaseSettings(int classScheduleId, ClassScheduleTestReleaseEmpSettingsCreateOptions options)
        {
            try
            {
                var result = await _classSchedule_TestReleaseEMPSettingsService.UpdateAsync(classScheduleId, options);
                return Ok( new { result });
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, new { ex.Message });
            }
        }

    }
}
