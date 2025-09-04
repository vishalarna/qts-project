using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenario_EnablingObjectives_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioPositon_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SimulatorScenarioController : ControllerBase
    {
        /// <summary>
        /// Links the Simulator Scenario with specific EO
        /// </summary>
        /// <returns>Http Response Code with result</returns>
        [HttpPost]
        [Route("/simScenario/{id}/eo")]

        public async Task<IActionResult> LinkSimulatorScenarioEOAsync(int id, SimulatorScenario_EnablingObjectives_LinkOptions options)
        {
            var result = await _simulatorScenarioService.LinkSimulatorScenarioEO(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the Simulator Scenario with specific EO
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/simScenario/{id}/eo")]
        public async Task<IActionResult> UnlinkSimulatorScenarioEOAsync(int id, SimulatorScenario_EnablingObjectives_LinkOptions options)
        {
            await _simulatorScenarioService.UnLinkSimulatorScenarioEO(id, options);
            return Ok( new { message = _localizer["EnablingObjectiveUnlinked"].Value });
        }

        /// <summary>
        /// Get the Simulator Scenario with specific EO
        /// </summary>
        /// <returns>Http Response Code with Linked EO</returns>
        [HttpGet]
        [Route("/simScenario/{id}/eo")]
        public async Task<IActionResult> GetLinkedSimulatorScenarioEOAsync(int id)
        {
            var result = await _simulatorScenarioService.GetLinkedSimulatorScenarioEOAsync(id);
            return Ok( new { result });
        }
    }
}
