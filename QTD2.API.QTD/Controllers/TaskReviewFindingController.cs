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
    public class TaskReviewFindingController : ControllerBase
    {
        private readonly ITaskReviewFindingService _taskReviewFindingService;
        public TaskReviewFindingController(ITaskReviewFindingService taskReviewFindingService)
        {
            _taskReviewFindingService = taskReviewFindingService;
        }

        [HttpGet]
        [Route("/taskReviewFindings")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _taskReviewFindingService.GetAllAsync();
            return Ok(new { result });
        }

    }
}
