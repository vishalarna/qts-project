using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingIssue_ActionItemPriorityController : ControllerBase
    {
        private readonly ITrainingIssueActionItemPriorityService _trainingIssueActionItemPriorityService;
        public TrainingIssue_ActionItemPriorityController(ITrainingIssueActionItemPriorityService trainingIssueActionItemPriorityService)
        {
            _trainingIssueActionItemPriorityService = trainingIssueActionItemPriorityService;
        }

        [HttpGet]
        [Route("/trainingIssueActionItemPriorities")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _trainingIssueActionItemPriorityService.GetAllAsync();
            return Ok(new { result });
        }

    }
}
