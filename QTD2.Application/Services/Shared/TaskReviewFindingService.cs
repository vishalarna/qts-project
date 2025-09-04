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
    public class TaskReviewFindingService : ITaskReviewFindingService
    {
        private readonly ITaskReview_FindingService _taskReview_FindingService;

        public TaskReviewFindingService(ITaskReview_FindingService taskReview_FindingService)
        {
            _taskReview_FindingService = taskReview_FindingService;
        }
        public async Task<List<TaskReviewFinding_VM>> GetAllAsync()
        {
            var findings = await _taskReview_FindingService.AllAsync();
            return findings.Select(item => new TaskReviewFinding_VM(item.Id, item.Finding)).ToList();
        }
    }
}
