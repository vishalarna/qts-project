using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenario;
using QTD2.Infrastructure.Model.SimulatorScenarioTaskObjectives_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SimulatorScenarioController : ControllerBase
    {

        [HttpPost]
        [Route("/simScenario/scripts")]
        public async Task<IActionResult> CreateScriptAsync(SimulatorScenario_Script_VM options)
        {
            var result = await _simulatorScenarioService.CreateScriptAsync(options);
            return Ok(new { result });
        }

        [HttpGet]
        [Route("/simScenario/scripts/{scriptId}/events/{eventId}")]
        public async Task<IActionResult> GetEventAndScriptAsync(int scriptId, int eventId)
        {
            var result = await _simulatorScenarioService.GetScriptAsync(scriptId, eventId);
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/simScenario/scripts/{scriptId}/events/{eventId}/copy")]
        public async Task<IActionResult> CopyScriptAsync(int scriptId, int eventId)
        {
            var result = await _simulatorScenarioService.CopyScriptAsync(scriptId, eventId);
            return Ok(new { result });
        }

        [HttpDelete]
        [Route("/simScenario/scripts/events/{scriptId}/delete")]
        public async Task<IActionResult> DeleteEventAndScriptAsync(int scriptId)
        {
            await _simulatorScenarioService.DeleteScriptAsync(scriptId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("/simScenario/scripts/{scriptId}/events/{eventId}/update")]
        public async Task<IActionResult> UpdateEventAndScriptAsync(int scriptId, int eventId, SimulatorScenario_Script_VM options)
        {
            var result = await _simulatorScenarioService.UpdateScriptAsync(scriptId, eventId, options);
            return Ok(new { result });
        }
    }
}
