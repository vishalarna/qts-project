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
    public class TaskListReviewStatusController : ControllerBase
    {
        private readonly ITaskListReviewStatusService _taskListReviewStatusService;
        public TaskListReviewStatusController(ITaskListReviewStatusService taskListReviewStatusService)
        {
            _taskListReviewStatusService = taskListReviewStatusService;
        }

        [HttpGet]
        [Route("/taskListReviewStatus")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _taskListReviewStatusService.GetAllAsync();
            return Ok(new { result });
        }

    }
}
