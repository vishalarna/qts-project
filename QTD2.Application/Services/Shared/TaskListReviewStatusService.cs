using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.TaskListReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class TaskListReviewStatusService : ITaskListReviewStatusService
    {
        private readonly ITaskListReview_StatusService _taskListReview_StatusService;
        public TaskListReviewStatusService(ITaskListReview_StatusService taskListReview_StatusService)
        {
            _taskListReview_StatusService = taskListReview_StatusService;
        }
        public async Task<List<TaskListReviewStatus_VM>> GetAllAsync()
        {
            var taskListReviewStatus = await _taskListReview_StatusService.AllAsync();
            return taskListReviewStatus.Select(x => new TaskListReviewStatus_VM(x.Id, x.Type)).ToList();
        }
    }
}
