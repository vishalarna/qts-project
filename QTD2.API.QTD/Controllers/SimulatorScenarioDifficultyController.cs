using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorScenarioDifficultyController : ControllerBase
    {
        private readonly ISimulatorScenarioDifficultyService _simulatorScenarioDifficultiesService;
        private readonly IStringLocalizer<SimulatorScenarioDifficultyController> _localizer;

        public SimulatorScenarioDifficultyController(ISimulatorScenarioDifficultyService simulatorScenarioDifficultiesService, 
            IStringLocalizer<SimulatorScenarioDifficultyController> localizer)
        {
            _localizer = localizer;
            _simulatorScenarioDifficultiesService = simulatorScenarioDifficultiesService;
        }


        [HttpGet]
        [Route("/simulatorScenarioDifficulties")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _simulatorScenarioDifficultiesService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
