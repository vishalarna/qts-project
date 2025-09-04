using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenarioTaskObjectives_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SimulatorScenarioController : ControllerBase
    {
        /// <summary>
        /// Links the Simulator Scenario with specific Task
        /// </summary>
        /// <returns>Http Response Code with result</returns>
        [HttpPost]
        [Route("/simScenario/{id}/task")]

        public async Task<IActionResult> LinkSimulatorScenarioTaskAsync(int id, SimulatorScenarioTaskObjectives_LinkOptions options)
        {
            var result = await _simulatorScenarioService.LinkSimulatorScenarioTask(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the Simulator Scenario with specific Task
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/simScenario/{id}/task")]
        public async Task<IActionResult> UnlinkSimulatorScenarioTaskAsync(int id, SimulatorScenarioTaskObjectives_LinkOptions options)
        {
            await _simulatorScenarioService.UnLinkSimulatorScenarioTask(id, options);
            return Ok( new { message = _localizer["TaskObjectiveUnlinked"].Value });
        }

        /// <summary>
        /// Get the Simulator Scenario with specific Task
        /// </summary>
        /// <returns>Http Response Code with Linked Task</returns>
        [HttpGet]
        [Route("/simScenario/{id}/task")]
        public async Task<IActionResult> GetLinkedSimulatorScenarioTaskAsync(int id)
        {
            var result = await _simulatorScenarioService.GetLinkedSimulatorScenarioTaskAsync(id);
            return Ok( new { result });
        }
    }
}
