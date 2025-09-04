using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings;
using System;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSchedule_TQEMPSettingsController : ControllerBase
    {
        private readonly IClassSchedule_TQEMPSettingsService _classScheduleTQEMPSettingsService;
        private readonly IClassScheduleService _classScheduleService;
        private readonly IClassSchedule_Evaluator_LinksService _classSchedule_Evaluator_LinksService;
        private readonly IStringLocalizer<ClassSchedule_TQEMPSettingsController> _localizer;

        public ClassSchedule_TQEMPSettingsController(IClassSchedule_TQEMPSettingsService classScheduleTQEMPSettingsService, IClassScheduleService classScheduleService, IClassSchedule_Evaluator_LinksService classSchedule_Evaluator_LinksService, IStringLocalizer<ClassSchedule_TQEMPSettingsController> localizer)
        {
            _classScheduleTQEMPSettingsService = classScheduleTQEMPSettingsService;
            _classScheduleService = classScheduleService;
            _classSchedule_Evaluator_LinksService = classSchedule_Evaluator_LinksService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/classschedule/tqEMPSettings/{id}")]
        public async Task<IActionResult> GetClassScheduleTQEMPSettings(int id)
        {
            var result = await _classScheduleTQEMPSettingsService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/classschedule/tqEMPSettings/{classScheduleId}")]
        public async Task<IActionResult> CreateClassScheduleTQEMPSettings(int classScheduleId)
        {
            var result = await _classScheduleTQEMPSettingsService.CreateAsync(classScheduleId);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/classschedule/tqEMPSettings/{classScheduleId}")]
        public async Task<IActionResult> UpdateClassScheduleTQEMPSettings(int classScheduleId, ClassSchedule_TQEMPSettingsCreateOptions options)
        {
            var result = await _classScheduleTQEMPSettingsService.UpdateAsync(classScheduleId, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/classschedule/{classScheduleId}/tqEMPSettings/linkEvaluators")]
        public async Task<IActionResult> LinkEvaluatorsFromClassSchedule(ClassScheduleEvaluatorLinksVM options)
        {
            await _classSchedule_Evaluator_LinksService.LinkEvaluators(options);
            return Ok( new { message = _localizer["Evaluators Linked To ClassSchedule"] });
        }

        [HttpDelete]
        [Route("/classschedule/{classScheduleId}/tqEMPSettings/unlinkEvaluators")]
        public async Task<IActionResult> UnlinkEvaluatorsFromClassSchedule(ClassScheduleEvaluatorLinksVM options)
        {
            await _classSchedule_Evaluator_LinksService.UnlinkEvaluator(options);
            return Ok( new { message = _localizer["Evaluators Linked To ClassSchedule"] });
        }

        [HttpPost]
        [Route("/classschedule/tqEvaluators/{classScheduleId}")]
        public async Task<IActionResult> CreateClassScheduleTQEvaluatorsFromILA(int classScheduleId)
        {
            var result = await _classSchedule_Evaluator_LinksService.LinkEvaluatorsFromILA(classScheduleId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/classschedule/tqEvaluators/{classScheduleId}")]
        public async Task<IActionResult> GetClassScheduleTQEvaluators(int classScheduleId)
        {
            var result = await _classSchedule_Evaluator_LinksService.GetClassScheduleTQEvaluators(classScheduleId);
            return Ok( new { result } );
        }
    }
}
