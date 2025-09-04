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
    public class SimulatorScenarioCollaboratorPermissionsController : ControllerBase
    {
        private readonly ISimulatorScenarioCollaboratorPermissionsService _simulatorScenarioCollaboratorPermissionsService;
        private readonly IStringLocalizer<SimulatorScenarioCollaboratorPermissionsController> _localizer;

        public SimulatorScenarioCollaboratorPermissionsController(ISimulatorScenarioCollaboratorPermissionsService simulatorScenarioCollaboratorPermissionsService,
        IStringLocalizer<SimulatorScenarioCollaboratorPermissionsController> localizer)
        {
            _localizer = localizer;
            _simulatorScenarioCollaboratorPermissionsService = simulatorScenarioCollaboratorPermissionsService;
        }


        [HttpGet]
        [Route("/simulatorScenarioCollaboratorPermissions")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _simulatorScenarioCollaboratorPermissionsService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
