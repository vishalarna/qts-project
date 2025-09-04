using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.DIFSurvey;
using QTD2.Infrastructure.Model.TaskListReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListReviewTypeController : ControllerBase
    {
        private readonly ITaskListReviewTypeService _taskListReviewTypeService;
        private readonly IStringLocalizer<TaskListReviewController> _localizer;

        public TaskListReviewTypeController(ITaskListReviewTypeService taskListReviewTypeService,
            IStringLocalizer<TaskListReviewController> localizer)
        {
            _taskListReviewTypeService = taskListReviewTypeService;
            _localizer = localizer;
        }

        [HttpGet]
        [Route("/taskListReviewTypes")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _taskListReviewTypeService.GetAllAsync();
            return Ok(new { result });
        }
    }
}
