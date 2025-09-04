using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_Review;
using QTD2.Infrastructure.Model.TaskListReview;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaskListReviewService
    {
        public Task<TaskListReviewOverview_VM> GetOverviewAsync();
        public Task<TaskListReview_VM> CreateAsync(TaskListReview_VM options);
        public Task<TaskListReview_VM> UpdateAsync(int id , TaskListReview_VM options);
        public Task<TaskListReview_VM> GetAsync(int taskListReviewId);
        public TaskListReview_VM MapRecordTaskListReviewToVM(TaskListReview taskListReview, Person person);
        public TaskListReview_GeneralReviewer_VM MapRecordTaskListReviewGeneralReviewerToVM(TaskListReview_GeneralReviewer generalReviewer);
        public TaskListReview_TaskReview_VM MapRecordTaskListReview_TaskReviewToVM(TaskReview taskReview, Person person);
        public Task<string> CopyAsync(int id);
        public System.Threading.Tasks.Task DeleteAsync(int id);
        public System.Threading.Tasks.Task ActivateInactivateAsync(int id, string type);
        public Task<List<TaskListReview_TaskReview_VM>> CreateTaskReviewsAsync(int id, TaskReviewCreateOption option);
    }
}
