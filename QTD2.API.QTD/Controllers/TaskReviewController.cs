using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Application.Services.Shared;
using QTD2.Infrastructure.Model.Position_Task_Link;
using QTD2.Infrastructure.Model.PositionHistory;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskReviewController : ControllerBase
    {
        private readonly ITaskReviewService _taskReviewService;
        private readonly IStringLocalizer<TaskReviewService> _localize;

        public TaskReviewController(ITaskReviewService taskReviewService, IStringLocalizer<TaskReviewService> localize)
        {
            _taskReviewService = taskReviewService;
            _localize = localize;
        }

        [HttpPost]
        [Route("/taskReviews/{id}/reviewers")]
        public async Task<IActionResult> CreateTaskReviewReviewerAsync(int id,TaskReview_ReviewerOption option)
        {
            var result = await _taskReviewService.CreateTaskReviewReviewerAsync(id,option);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/taskReviews/{id}/reviewers/{reviewerId}")]

        public async Task<IActionResult> DeleteTaskReviewReviewerAsync(int id,int reviewerId)
        {
             await _taskReviewService.DeleteTaskReviewReviewerAsync(id, reviewerId);
            return Ok( new { message = _localize["TaskReviewerRemoved"].Value });
        }

        [HttpGet]
        [Route("/taskReviews/{id}")]

        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _taskReviewService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpPost]
        [Route("/taskReviews/{id}/taskReviewActionItems")]
        public async Task<IActionResult> CreateTaskReviewActionItemAsync(int id, TaskReviewActionItem_VM option)
        {
            var result = await _taskReviewService.CreateTaskReviewActionItemAsync(id, option);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/taskReviews/{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _taskReviewService.DeleteAsync(id);
            return Ok( new { message = "TaskReview Deleted successfully." });
        }

        [HttpPut]
        [Route("/taskReviews/{id}")]

        public async Task<IActionResult> UpdateAsync(int id,TaskReview_VM options)
        {
             await _taskReviewService.UpdateAsync(id,options);
            return Ok( new { message = "TaskReview Updated successfully." });
        }

        [HttpGet]
        [Route("/taskReviews/status")]
        public async Task<IActionResult> GetAllStatusAsync()
        {
            var result = await _taskReviewService.GetAllStatusAsync();
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/taskReviews/unlink")]
        public async Task<IActionResult> UnlinkTaskAsync(TaskReviewOptions options)
        {
            await _taskReviewService.UnlinkTaskAsync(options);
            return Ok(new { message = "Tasks UnLinked successfully." });
        }
    }
}
