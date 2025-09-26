using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.PositionTask;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class PositionTaskController : ControllerBase
    {
        private readonly IPositionTaskService _positionTaskService;
        public PositionTaskController(
          IPositionTaskService positionTaskService)
        {
            _positionTaskService = positionTaskService;
        }

        [HttpPut]
        [Route("/positionTasks/{positionTaskId}/unmarkAsR6")]
        public async Task<IActionResult> UpdateUnmarkAsR6Async(int positionTaskId, UpdateUnmarkAsR6Model updateUnmarkAsR6Model)
        {
            await _positionTaskService.UnmarkPositionTaskAsR6(positionTaskId, updateUnmarkAsR6Model);
            return Ok();
        }

        [HttpPut]
        [Route("/positionTasks/markAsR6")]
        public async Task<IActionResult> UpdateMarkAsR6Async(UpdateMarkAsR6Model updateMarkAsR6Model)
        {
            await _positionTaskService.MarkPositionTasksAsR6(updateMarkAsR6Model);
            return Ok();
        }

        [HttpPut]
        [Route("/positionTasks/{positionTaskId}/updateR5Tasks")]
        public async Task<IActionResult> UpdateR5TasksAsync(int positionTaskId, LinkR5UpdateTasksModel r5UpdateTasksModel)
        {
            await _positionTaskService.UpdateR5Tasks(positionTaskId, r5UpdateTasksModel);
            return Ok();
        }

        [HttpDelete]
        [Route("/positionTasks/{positionTaskId}/deleteR5Task/{r5ImpactedTaskId}")]
        public async Task<IActionResult> DeleteR5TaskAsync(int positionTaskId, int r5ImpactedTaskId, DeleteR5TaskModel deleteR5TaskModel)
        {
            await _positionTaskService.DeleteR5Task(positionTaskId, r5ImpactedTaskId, deleteR5TaskModel);
            return Ok();
        }

        [HttpGet]
        [Route("/positionTasks/{positionId}")]
        public async Task<IActionResult> GetPositionTasksAsync(int positionId)
        {
            var result = await _positionTaskService.GetPositionTaskByPositionIdAsync(positionId);
            return Ok(new { result });
        }

    }
}