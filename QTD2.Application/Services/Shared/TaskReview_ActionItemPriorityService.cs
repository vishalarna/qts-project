using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class TaskReview_ActionItemPriorityService : ITaskReview_ActionItemPriorityService
    {
        private readonly IActionItem_PriorityService _actionItem_PriorityService;

        public TaskReview_ActionItemPriorityService(IActionItem_PriorityService actionItem_PriorityService)
        {
            _actionItem_PriorityService = actionItem_PriorityService;
        }
        public async Task<List<TaskReviewActionItemPriority_VM>> GetAllAsync()
        {
            var taskReview_ActionitemsPriorities = await _actionItem_PriorityService.AllAsync();
            return taskReview_ActionitemsPriorities.Select(x => new TaskReviewActionItemPriority_VM(x.Id, x.Type)).ToList();
        }
    }
}
