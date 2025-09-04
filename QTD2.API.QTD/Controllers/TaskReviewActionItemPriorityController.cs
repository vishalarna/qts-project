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
    public class TaskReviewActionItemPriorityController : ControllerBase
    {
        private readonly ITaskReview_ActionItemPriorityService _taskReview_ActionItemPriorityService;
        
        public TaskReviewActionItemPriorityController(ITaskReview_ActionItemPriorityService taskReview_ActionItemPriorityService)
        {
            _taskReview_ActionItemPriorityService = taskReview_ActionItemPriorityService;
        }

        [HttpGet]
        [Route("/taskReviewActionItemsPriorities")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _taskReview_ActionItemPriorityService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
