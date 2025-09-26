using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.SimulatorScenario;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class SimulatorScenarioController : ControllerBase
    {
        private readonly ISimulatorScenarioService _simulatorScenarioService;
        private readonly IStringLocalizer<SimulatorScenarioController> _localizer;

        public SimulatorScenarioController(ISimulatorScenarioService simulatorScenarioService, IStringLocalizer<SimulatorScenarioController> localizer)
        {
            _localizer = localizer;
            _simulatorScenarioService = simulatorScenarioService;
        }

      
        [HttpGet]
        [Route("/simulatorScenarios/overview")]
        public async Task<IActionResult> GetOverviewAsync()
        {
            var result = await _simulatorScenarioService.GetOverviewAsync();
            return Ok(new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios")]
        public async Task<IActionResult> CreateAsync(SimulatorScenario_VM options)
        {
            var result = await _simulatorScenarioService.CreateAsync(options);
            return Ok( new { result });
        }


        [HttpDelete]
        [Route("/simulatorScenarios/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
          await _simulatorScenarioService.DeleteSimScenarioById(id);
          return StatusCode(StatusCodes.Status200OK);
        }


        [HttpGet]
        [Route("/simulatorScenarios/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _simulatorScenarioService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/simulatorScenarios/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SimulatorScenario_VM options)
        {
            var result = await _simulatorScenarioService.UpdateAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}")]
        public async Task<IActionResult> CopyAsync(int id)
        {
            var result = await _simulatorScenarioService.CopyAsync(id);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/simulatorScenarios/{id}/active")]
        public async Task<IActionResult> ActiveAsync(int id)
        {
            await _simulatorScenarioService.ActiveAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("/simulatorScenarios/{id}/inactive")]
        public async Task<IActionResult> InactiveAsync(int id)
        {
            await _simulatorScenarioService.InActiveAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }


        [HttpPost]
        [Route("/simulatorScenarios/{id}/positions")]
        public async Task<IActionResult> UpdatePositionsAsync(int id, SimulatorScenario_UpdatePositions_VM options)
        {
            var result = await _simulatorScenarioService.UpdatePositionsAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/tasks")]
        public async Task<IActionResult> UpdateTasksAsync(int id, SimulatorScenario_UpdateTasks_VM options)
        {
            var result = await _simulatorScenarioService.UpdateTaskAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/enablingObjectives")]
        public async Task<IActionResult> UpdateEnablingObjectivesAsync(int id, SimulatorScenario_UpdateEnablingObjectives_VM options)
        {
            var result = await _simulatorScenarioService.UpdateEnablingObjectivesAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/procedures")]
        public async Task<IActionResult> UpdateProceduresAsync(int id, SimulatorScenario_UpdateProcedures_VM options)
        {
            var result = await _simulatorScenarioService.UpdateProceduresAsync(id, options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/simulatorScenarios/{id}/positions/{positionId}/taskCriterias")]
        public async Task<IActionResult> GetTaskCriteriasForPositionAsync(int id,int positionId)
        {
            var result = await _simulatorScenarioService.GetTaskCriteriasForPositionAsync(id, positionId);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/simulatorScenarios/{id}/positions/allTaskCriterias")]
        public async Task<IActionResult> GetAllTaskCriteriasForPositionAsync(int id)
        {
            var result = await _simulatorScenarioService.GetAllTaskCriteriasForPositionAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/taskCriterias")]
        public async Task<IActionResult> CreateTaskCriteriaAsync(int id, SimulatorScenario_Task_Criteria_VM options)
        {
            var result = await _simulatorScenarioService.CreateTaskCriteriaAsync(id, options);
            return Ok( new { result });
        }

        [HttpPut]
        [Route("/simulatorScenarios/{id}/taskCriterias/{simulatorScenarioTaskCriteriaId}")]
        public async Task<IActionResult> UpdateTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId, SimulatorScenario_Task_Criteria_VM options)
        {
            var result = await _simulatorScenarioService.UpdateTaskCriteriaAsync(id, simulatorScenarioTaskCriteriaId, options);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/simulatorScenarios/{id}/taskCriterias/{simulatorScenarioTaskCriteriaId}")]
        public async Task<IActionResult> DeleteTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId)
        {
            await _simulatorScenarioService.DeleteTaskCriteriaAsync(id, simulatorScenarioTaskCriteriaId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/events")]
        public async Task<IActionResult> CreateEventAsync(int id, SimulatorScenario_Event_VM options)
        {
            var result = await _simulatorScenarioService.CreateEventAsync(id, options);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/simulatorScenarios/{id}/events/{eventId}")]
        public async Task<IActionResult> GetEventAsync(int id,int eventId)
        {
            var result = await _simulatorScenarioService.GetEventAsync(id, eventId);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/events/{eventId}")]
        public async Task<IActionResult> CopyEventAsync(int id, int eventId)
        {
            var result = await _simulatorScenarioService.CopyEventAsync(id, eventId);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/simulatorScenarios/{id}/events/{eventId}")]
        public async Task<IActionResult> DeleteEventAndAsync(int id, int eventId)
        {
            await _simulatorScenarioService.DeleteEventAsync(id, eventId);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [Route("/simulatorScenarios/{id}/events/{eventId}")]
        public async Task<IActionResult> UpdateEventAsync(int id, int eventId, SimulatorScenario_Event_VM options)
        {
            var result = await _simulatorScenarioService.UpdateEventAsync(id, eventId, options);
            return Ok( new { result });
        }


        [HttpPut]
        [Route("/simulatorScenarios/{id}/events/order")]
        public async Task<IActionResult> UpdateEventOrderAsync(int id, SimulatorScenario_UpdateEventsAndScriptsOrder_VM options)
        {
             await _simulatorScenarioService.UpdateEventsOrderAsync(id, options);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/ilas")]
        public async Task<IActionResult> UpdateILAsAsync(int id, SimulatorScenario_UpdateILAs_VM options)
        {
            var result = await _simulatorScenarioService.UpdateILAsAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/prerequisites")]
        public async Task<IActionResult> UpdatePrerequisitesAsync(int id, SimulatorScenario_UpdatePrerequisites_VM options)
        {
            var result = await _simulatorScenarioService.UpdatePrerequisitesAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/collaborators")]
        public async Task<IActionResult> UpdateCollaboratorsAsync(int id, SimulatorScenario_UpdateCollaborators_VM options)
        {
            var result = await _simulatorScenarioService.UpdateCollaboratorsAsync(id, options);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/simulatorScenarios/{id}/publish")]
        public async Task<IActionResult> PublishAsync(int id, SimulatorScenario_VM options)
        {
            await _simulatorScenarioService.PublishAsync(id,options);
            return Ok();
        }

        [HttpGet]
        [Route("/simulatorScenarios/{id}/positions")]
        public async Task<IActionResult> GetSimulatorScenario_PositionsAsync(int id)
        {
            var result = await _simulatorScenarioService.GetSimulatorScenario_PositionsAsync(id);
            return Ok(new { result });
        }
    }
}
