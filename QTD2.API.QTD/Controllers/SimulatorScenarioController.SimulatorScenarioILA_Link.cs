using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenarioILA_Link;

namespace QTD2.API.QTD.Controllers
{
    public partial class SimulatorScenarioController : ControllerBase
    {
        /// <summary>
        /// Links the Simulator Scenario with specific ILA
        /// </summary>
        /// <returns>Http Response Code with result</returns>
        [HttpPost]
        [Route("/simScenario/{id}/ila")]

        public async Task<IActionResult> LinkSimulatorScenarioILAAsync(int id, SimulatorScenarioILA_LinkOptions options)
        {
            var result = await _simulatorScenarioService.LinkSimulatorScenarioILA(id, options);
            return Ok( new { result });
        }

        /// <summary>
        /// Unlinks the Simulator Scenario with specific ILA
        /// </summary>
        /// <returns>Http Response Code with message</returns>
        [HttpDelete]
        [Route("/simScenario/{id}/ila")]
        public async Task<IActionResult> UnlinkSimulatorScenarioILAAsync(int id, SimulatorScenarioILA_LinkOptions options)
        {
            await _simulatorScenarioService.UnLinkSimulatorScenarioILA(id, options);
            return Ok( new { message = _localizer["TestItemUnlinked"].Value });
        }

        /// <summary>
        /// Get the Simulator Scenario with specific ILA
        /// </summary>
        /// <returns>Http Response Code with Linked ILA</returns>
        [HttpGet]
        [Route("/simScenario/{id}/ila")]
        public async Task<IActionResult> GetLinkedSimulatorScenarioILAAsync(int id)
        {
            var result = await _simulatorScenarioService.GetLinkedSimulatorScenarioILAAsync(id);
            return Ok( new { result });
        }
    }
}
