using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class TaskReviewActionItemController : ControllerBase
    {
        private readonly ITaskReviewActionItemService _taskReviewActionItemService;
        public TaskReviewActionItemController(ITaskReviewActionItemService taskReviewActionItemService)
        {
            _taskReviewActionItemService = taskReviewActionItemService;
    }


        [HttpGet]
        [Route("/taskReviewActionItems/types")]
        public async Task<IActionResult> GetActionItemTypes()
        {
            var result = _taskReviewActionItemService.GetActionItemTypesAsync();
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskReviewActionItems/operationTypes/{actionItemType}")]

        public async Task<IActionResult> GetOperationTypesAsync(string actionItemType)
        {
            var result = await _taskReviewActionItemService.GetOperationTypesAsync(actionItemType);
            return Ok( new { result });
        }

        [HttpGet]
        [Route("/taskReviewActionItems/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result =await _taskReviewActionItemService.GetAsync(id);
            return Ok( new { result });
        }

        [HttpDelete]
        [Route("/taskReviewActionItems/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
             await _taskReviewActionItemService.DeleteAsync(id);
            return Ok( new { message = "ActionItem Deleted successfully." });
        }

        [HttpPut]
        [Route("/taskReviewActionItems/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskReviewActionItem_VM options)
        {
            var result = await _taskReviewActionItemService.UpdateAsync(id,options);
            return Ok( new { result });
        }

    }
}
