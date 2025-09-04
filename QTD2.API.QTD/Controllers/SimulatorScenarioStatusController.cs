using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.Extensions.Localization;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorScenarioStatusController : ControllerBase
    {
        private readonly ISimulatorScenarioStatusService _simulatorScenarioStatusService;
        private readonly IStringLocalizer<SimulatorScenarioStatusController> _localizer;

        public SimulatorScenarioStatusController(ISimulatorScenarioStatusService simulatorScenarioStatusService,
        IStringLocalizer<SimulatorScenarioStatusController> localizer)
        {
            _localizer = localizer;
            _simulatorScenarioStatusService = simulatorScenarioStatusService;
        }


        [HttpGet]
        [Route("/simulatorScenarioStatuses")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _simulatorScenarioStatusService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
