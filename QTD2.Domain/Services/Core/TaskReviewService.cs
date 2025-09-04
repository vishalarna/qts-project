using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace QTD2.Domain.Services.Core
{
    public class TaskReviewService : Common.Service<TaskReview>,
              ITaskReviewService
    {
        public TaskReviewService(ITaskReviewRepository repository, ITaskReviewValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<TaskReview> GetTaskReviewWithAllIncludeAsync(int id)
        {
            var taskReview = await GetWithIncludeAsync(id, new string[] { "Reviewers",
                  "ActionItems.ActionItem_SubDuty_Operations",
                  "ActionItems.ActionItem_Step_Operations",
                  "ActionItems.ActionItem_QuestionAndAnswer_Operations",
                  "ActionItems.ActionItem_Suggestion_Operations",
                  "ActionItems.ActionItem_EnablingObjective_Operations",
                  "ActionItems.ActionItem_Tool_Operations",
                  "ActionItems.ActionItem_RegulatoryRequirement_Operations",
                  "ActionItems.ActionItem_Procedure_Operations",
                  "ActionItems.ActionItem_SafetyHazard_Operations" });

            return taskReview;
        }

        public async Task<List<TaskReview>> GetTaskReviewsByIdsAsync(List<int> taskReviewIds)
        {
            List<Expression<Func<TaskReview, bool>>> predicates = new List<Expression<Func<TaskReview, bool>>>();
            predicates.Add(x => taskReviewIds.Contains(x.Id));

            var taskReviews = await FindWithIncludeAsync(predicates, new string[] { "Finding" });
            var taskReviewsWithTask = await FindWithIncludeAsync(predicates, new string[] { "Task.SubdutyArea.DutyArea", "Task.Version_Tasks", "Task.Task_Histories" });
            var taskReviewsWithReviewers = await FindWithIncludeAsync(predicates, new string[] { "Reviewers.Reviewer.Person" });
            var taskReviewsWithActionItems = await FindWithIncludeAsync(predicates, new string[] { "ActionItems.Assignee.Person", "ActionItems.Priority" });

            foreach (var taskReview in taskReviews)
            {
                taskReview.Task = taskReviewsWithTask.First(x => x.Id == taskReview.Id).Task;
                taskReview.Reviewers = taskReviewsWithReviewers.First(x => x.Id == taskReview.Id).Reviewers;
                taskReview.ActionItems = taskReviewsWithActionItems.First(x => x.Id == taskReview.Id).ActionItems;
            }
            return taskReviews.ToList();
        }

        public async Task<List<TaskReview>> GetTaskReviewsAsync(List<int> taskReviewIds)
        {
            List<Expression<Func<TaskReview, bool>>> predicates = new List<Expression<Func<TaskReview, bool>>>();
            predicates.Add(x => taskReviewIds.Contains(x.Id));
            var taskReviews = await FindAsync(predicates);
            return taskReviews.ToList();
        }

    }
}