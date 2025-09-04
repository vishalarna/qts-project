using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenarioPositon_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SimulatorScenarioController : ControllerBase
    {
        /// <summary>
        /// Links the Simulator Scenario with specific Position
        /// </summary>
        /// <returns>Http Response Code with result</returns>
        [HttpPost]
        [Route("/simScenario/{id}/position")]

        public async Task<IActionResult> LinkSimulatorScenarioPositionAsync(int id, SimulatorScenarioPositon_LinkOptions options)
        {
            var result = await _simulatorScenarioService.LinkSimulatorScenarioPosititon(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the Simulator Scenario with specific Position
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/simScenario/{id}/position")]
        public async Task<IActionResult> UnlinkSimulatorScenarioPositionAsync(int id, SimulatorScenarioPositon_LinkOptions options)
        {
            await _simulatorScenarioService.UnLinkSimulatorScenarioPosition(id, options);
            return Ok( new { message = _localizer["TestItemUnlinked"].Value });
        }

        /// <summary>
        /// Get the Simulator Scenario with specific Position
        /// </summary>
        /// <returns>Http Response Code with Linked Position</returns>
        [HttpGet]
        [Route("/simScenario/{id}/position")]
        public async Task<IActionResult> GetLinkedSimulatorScenarioPositionAsync(int id)
        {
            var result = await _simulatorScenarioService.GetLinkedSimulatorScenarioPositionAsync(id);
            return Ok( new { result });
        }
    }
}
