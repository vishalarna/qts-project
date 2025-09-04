using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskReviewService
    {
        public Task<TaskReview_Reviewer_VM> CreateTaskReviewReviewerAsync(int id, TaskReview_ReviewerOption option);
        public System.Threading.Tasks.Task DeleteTaskReviewReviewerAsync(int id, int reviewerId);
        public Task<TaskReview_VM> GetAsync(int id);
        public Task<TaskReviewActionItem_VM> CreateTaskReviewActionItemAsync(int id, TaskReviewActionItem_VM option);
        public System.Threading.Tasks.Task DeleteAsync(int id);
        public System.Threading.Tasks.Task UpdateAsync(int id, TaskReview_VM options);
        public Task<List<TaskReviewStatusVM>> GetAllStatusAsync();
        public System.Threading.Tasks.Task UnlinkTaskAsync(TaskReviewOptions options);

    }
}
