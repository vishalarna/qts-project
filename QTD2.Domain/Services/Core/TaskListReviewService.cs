using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace QTD2.Domain.Services.Core
{
    public class TaskListReviewService : Common.Service<TaskListReview>,
         ITaskListReviewService
    {
        public TaskListReviewService(ITaskListReviewRepository repository, ITaskListReviewValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<TaskListReview> GetForCopyAndDeleteAsync(int id)
        {
            var taskListReviewCopy = await GetWithIncludeAsync(id, new string[]
            {
                "GeneralReviewers",
                "TaskReviews.Reviewers",
                "TaskReviews.ActionItems.ActionItem_EnablingObjective_Operations",
                "TaskReviews.ActionItems.ActionItem_Procedure_Operations",
                "TaskReviews.ActionItems.ActionItem_QuestionAndAnswer_Operations",
                "TaskReviews.ActionItems.ActionItem_RegulatoryRequirement_Operations",
                "TaskReviews.ActionItems.ActionItem_SafetyHazard_Operations",
                "TaskReviews.ActionItems.ActionItem_Step_Operations",
                "TaskReviews.ActionItems.ActionItem_SubDuty_Operations",
                "TaskReviews.ActionItems.ActionItem_Suggestion_Operations",
                "TaskReviews.ActionItems.ActionItem_Tool_Operations",
                "TaskListReview_PositionLinks"
             });

            return taskListReviewCopy;
        }
        public async Task<string> GetTitleByIdAsync(int id)
        {
            var taskListReview = await GetAsync(id);
            return taskListReview?.Title;
        }
        public async Task<List<TaskListReview>> GetAllAsync()
        {
            var taskListReviews = await AllAsync();
            return taskListReviews.ToList();
        }
        public async Task<TaskListReview> GeForVMAsync(int id)
        {
            List<Expression<Func<Domain.Entities.Core.TaskListReview, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TaskListReview, bool>>>();
            predicates.Add(x => x.Id == id);
            var taskListReview = (await FindWithIncludeAsync(predicates, new[] { "GeneralReviewers.GeneralReviewer.Person", "TaskListReview_PositionLinks" } , true)).FirstOrDefault();
            var taskListReviewWithTaskReview = (await FindWithIncludeAsync(predicates, new[] { "TaskReviews.Task.Position_Tasks.Position", "TaskReviews.Task.SubdutyArea.DutyArea", "TaskReviews.Finding", "TaskReviews.Status", "TaskReviews.Reviewers.Reviewer.Person", "TaskReviews.ActionItems.Assignee.Person", "TaskReviews.ActionItems.Priority" } , true)).FirstOrDefault();
            if(taskListReview != null)
            {
                taskListReview.TaskReviews = taskListReviewWithTaskReview.TaskReviews;
            }
            return taskListReview;
        }
        
        public async Task<List<TaskListReview>> GetTaskListReviewsByIdsAndStatusAsync(List<int> taskListReviewIds,string status)
        {
            List<Expression<Func<TaskListReview, bool>>> predicates = new List<Expression<Func<TaskListReview, bool>>>();
            predicates.Add(x => !taskListReviewIds.Any() || taskListReviewIds.Contains(x.Id));
            if (status.ToLower() == "inactive")
            {
                predicates.Add(r => !r.Active);
            }
            else if (status.ToLower() == "active")
            {
                predicates.Add(r => r.Active);
            }

            var taskListReviews = await FindWithIncludeAsync(predicates, new string[] {  "GeneralReviewers.GeneralReviewer.Person" , "TaskReviews" });
            return taskListReviews.ToList();
        }

    }
}
