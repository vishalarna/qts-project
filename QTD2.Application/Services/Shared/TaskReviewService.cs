using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Task_Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITaskReviewDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReviewService;
using ITaskReviewService = QTD2.Application.Interfaces.Services.Shared.ITaskReviewService;
using ITaskListReviewDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskListReviewService;
using ITaskReviewStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReview_StatusService;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Services.Core;

namespace QTD2.Application.Services.Shared
{
    public class TaskReviewService : ITaskReviewService
    {
        private readonly ITaskReviewDomainService _taskReviewService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TaskReviewService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITaskReview_ReviewerService _taskReview_ReviewerService;
        private readonly ITaskListReviewDomainService _taskListReviewService;
        private readonly IActionItemService _actionItemService;
        private readonly ITaskReviewActionItemService _taskReviewActionItemService;
        private readonly ITaskReviewStatusDomainService _taskReviewStatusDomainService;
        private readonly Interfaces.Services.Shared.ITaskService _taskService;

        public TaskReviewService(ITaskReviewDomainService taskReviewService, IAuthorizationService authorizationService, IActionItemService actionItemService,
        IStringLocalizer<TaskReviewService> localizer, IHttpContextAccessor httpContextAccessor, ITaskReview_ReviewerService taskReview_ReviewerService, ITaskListReviewDomainService taskListReviewService, ITaskReviewActionItemService taskReviewActionItemService, ITaskReviewStatusDomainService taskReviewStatusDomainService,
        Interfaces.Services.Shared.ITaskService taskService)
        {
            _taskReviewService = taskReviewService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _taskReview_ReviewerService = taskReview_ReviewerService;
            _taskListReviewService = taskListReviewService;
            _actionItemService = actionItemService;
            _taskReviewActionItemService = taskReviewActionItemService;
            _taskReviewStatusDomainService = taskReviewStatusDomainService;
            _taskService = taskService;
        }

        public async Task<TaskReviewActionItem_VM> CreateTaskReviewActionItemAsync(int id, TaskReviewActionItem_VM option)
        {
            ActionItem actionItem;
            TaskReviewActionItem_VM action_Item = null;
            switch (option.Type)
                {
                   case "SubDutyArea":
                    actionItem = new SubDutyArea_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.SubDuty_Operations.ForEach(x => (actionItem as SubDutyArea_ActionItem)?.AddSubDutyOperation(x.OperationTypeId, x.SubDutyAreaId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Steps":
                    actionItem = new Steps_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.Step_Operations.ForEach(x => (actionItem as Steps_ActionItem)?.AddStepOperation(x.OperationTypeId, x.Task_StepId,x.Description));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "QuestionAndAnswer":
                    actionItem = new QuestionAndAnswer_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.QuestionAndAnswer_Operations.ForEach(x => (actionItem as QuestionAndAnswer_ActionItem)?.AddQuestionAnswerOperation(x.OperationTypeId, x.Task_QuestionId, x.Question,x.Answer));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "TaskSpecificSuggestions":
                    actionItem = new Task_Specific_Suggestions_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.Suggestion_Operations.ForEach(x => (actionItem as Task_Specific_Suggestions_ActionItem)?.AddSuggestionOperation(x.OperationTypeId, x.Task_SuggestionId, x.Description));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Tools":
                    actionItem = new Tools_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.Tool_Operations.ForEach(x => (actionItem as Tools_ActionItem)?.AddToolOperation(x.OperationTypeId, x.ToolId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "EnablingObjective":
                    actionItem = new EnablingObjective_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.EnablingObjective_Operations.ForEach(x => (actionItem as EnablingObjective_ActionItem)?.AddEnablingObjectiveOperation(x.OperationTypeId, x.EnablingObjectiveId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Procedure":
                    actionItem = new Procedure_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.Procedure_Operations.ForEach(x => (actionItem as Procedure_ActionItem)?.AddProcedureOperation(x.OperationTypeId, x.ProcedureId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "RegulatoryRequirements":
                    actionItem = new RegulatoryRequirements_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.RegulatoryRequirement_Operations.ForEach(x => (actionItem as RegulatoryRequirements_ActionItem)?.AddRegulatoryRequirementOperation(x.OperationTypeId, x.RegulatoryRequirementId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "SafetyHazards":
                    actionItem = new SafetyHazards_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    option.SafetyHazard_Operations.ForEach(x => (actionItem as SafetyHazards_ActionItem)?.AddSafetyhazardOperation(x.OperationTypeId, x.SafetyHazardId));
                    actionItem = await _taskReviewActionItemService.UpdateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "DutyArea":
                    actionItem = new DutyArea_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Task":
                    actionItem = new Task_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes,option.Task_number,option.Task_statement);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Conditions":
                    actionItem = new Conditions_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes,option.Conditions);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "Criteria":
                    actionItem = new Criteria_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes, option.Criteria);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "References":
                    actionItem = new References_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes, option.References);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                   case "MetaTask":
                    actionItem = new MetaTask_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes, option.IsMeta);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                case "Other":
                    actionItem = new Other_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                case "PrepareForTaskRequalification":
                    actionItem = new PrepareForTaskRequalification_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

                case "MakeTaskInactive":
                    actionItem = new MakeTaskInactive_ActionItem(id, option.AssigneeId, option.PriorityId, option.AssignedDate, option.DueDate, option.Notes);
                    actionItem = await _taskReviewActionItemService.CreateActionItemAsync(actionItem);
                    action_Item = await _taskReviewActionItemService.GetAsync(actionItem.Id);
                    break;

            }
            return action_Item;
        }

        public async Task<TaskReview_Reviewer_VM> CreateTaskReviewReviewerAsync(int id,TaskReview_ReviewerOption option)
        {
            var taskReview = await _taskReviewService.GetWithIncludeAsync(id, new string[] { "Reviewers" });
            if (taskReview == null)
            {
                throw new ArgumentNullException(nameof(taskReview), "TaskReview not found.");
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskReview, TaskReviewOperations.Update);
            if (result.Succeeded)
            {
                var taskReview_reviewer = taskReview.AddReviewer(option.QtdUserId);

                var taskReview_Reviewer_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskReview_reviewer, TaskReview_ReviewerOperations.Create);

                if (taskReview_Reviewer_Result.Succeeded)
                {
                    taskReview_reviewer.Create(_httpContextAccessor.HttpContext.User.Identity.Name);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
                var validationResult = await _taskReviewService.UpdateAsync(taskReview);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                var reviewer = await _taskReview_ReviewerService.GetWithIncludeAsync(taskReview_reviewer.Id, new string[] { "Reviewer.Person" });
                var taskReview_reviewerVM = new TaskReview_Reviewer_VM(reviewer.Id, $"{reviewer.Reviewer.Person.FirstName} {reviewer.Reviewer.Person.LastName}");
                return taskReview_reviewerVM;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async System.Threading.Tasks.Task DeleteTaskReviewReviewerAsync(int id, int reviewerId)
        {
            var taskReview = await _taskReviewService.GetWithIncludeAsync(id, new string[] { "Reviewers" });
            if (taskReview == null)
            {
                throw new ArgumentNullException(nameof(taskReview), "TaskReview not found.");
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskReview, TaskReviewOperations.Update);
            if (result.Succeeded)
            {
                var taskReview_reviewer = taskReview.RemoveReviewer(reviewerId);
                var taskReview_Reviewer_Result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskReview_reviewer, TaskReview_ReviewerOperations.Delete);
                if (taskReview_Reviewer_Result.Succeeded)
                {
                    taskReview_reviewer.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
                var validationResult = await _taskReviewService.UpdateAsync(taskReview);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<TaskReview_VM> GetAsync(int id)
        {
            var taskReview = await _taskReviewService.GetWithIncludeAsync(id, new string[] { "ActionItems", "Reviewers.Reviewer.Person", "Finding", "Task.SubdutyArea.DutyArea", "TaskListReview" });
            var taskReviews =await _taskReviewService.FindAsync(x => x.TaskListReviewId == taskReview.TaskListReviewId);
            var nextTaskReview = taskReviews.OrderBy(x => x.Id).SkipWhile(tr => tr.Id != taskReview.Id).Skip(1).FirstOrDefault();
            var fullNumber = "";
            if (nextTaskReview != null)
            {
                fullNumber = (await _taskService.GetAsync(nextTaskReview.TaskId))?.FullNumber;
            }
            var taskReviewVM = new TaskReview_VM()
            {
                Id = taskReview.Id,
                Number = taskReview.Task?.FullNumber,
                Statement = taskReview.Task?.Description,
                RecentHistoryDate = taskReview.ModifiedDate,
                ReviewDate = taskReview.ReviewDate,
                Reviewers = taskReview?.Reviewers.Select(reviewer => new TaskReview_Reviewer_VM(reviewer.Reviewer.Id, $"{reviewer.Reviewer.Person.FirstName} {reviewer.Reviewer.Person.LastName}")).ToList(),
                FindingId = taskReview.Finding?.Id,
                RequalificationDueDate = taskReview.RequalificationDueDate,
                Notes = taskReview.Notes,
                TaskReviewActionItems = taskReview.ActionItems.Select(actionItem => new TaskReview_TaskReviewActionItem_VM(actionItem.Id, actionItem.ActionItemTypeToDisplay, actionItem.PriorityId, actionItem.AssignedDate, actionItem.DueDate, actionItem.Notes)).ToList(),
                TaskId = taskReview.Task.Id,
                NextTaskReviewId = nextTaskReview?.Id,
                FullNumber = fullNumber,
                TrainingIssueId = taskReview.TrainingIssueId
            };
            return taskReviewVM;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await _taskReviewService.GetTaskReviewWithAllIncludeAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TaskReviewOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _taskReviewService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, TaskReview_VM options)
        {
            var taskReviewObj = await _taskReviewService.GetTaskReviewWithAllIncludeAsync(id);
            if (taskReviewObj == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskReviewObj, TaskReviewOperations.Update);
            if (result.Succeeded)
            {
                taskReviewObj.UpdateTaskReview(options.ReviewDate, options.RequalificationDueDate, options.Notes);
                taskReviewObj.SetFinding(options.FindingId);
                taskReviewObj.SetTaskReview_StatusId(options.FindingId);
                taskReviewObj.TrainingIssueId = options.TrainingIssueId;
                var validationResult = await _taskReviewService.UpdateAsync(taskReviewObj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
        public async Task<List<TaskReviewStatusVM>> GetAllStatusAsync()
        {
            var taskReviewStatus = await _taskReviewStatusDomainService.AllAsync();
            return taskReviewStatus.Select(x => new TaskReviewStatusVM(x.Id, x.Status)).ToList();
        }

        public async System.Threading.Tasks.Task UnlinkTaskAsync(TaskReviewOptions options)
        {
            var taskReviews = await _taskReviewService.GetTaskReviewsAsync(options.TaskReviewIds);

            if (taskReviews == null)
            {
                throw new ArgumentNullException(nameof(taskReviews), "TaskReview not found.");
            }

            foreach (var taskReview in taskReviews)
            {
                taskReview.Delete();
            }
            var validationResult = await _taskReviewService.BulkUpdateAsync(taskReviews);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

        }
    }
}
