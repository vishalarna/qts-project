using QTD2.Application.Interfaces.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.Task_Review;
using QTD2.Domain.Interfaces.Service.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Entities.Core;
using Microsoft.Extensions.Localization;

namespace QTD2.Application.Services.Shared
{
    public class TaskReviewActionItemService : ITaskReviewActionItemService
    {
        private readonly IActionItem_OperationType_LinksService _actionItem_OperationType_LinksService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IActionItemService _actionItemService;
        private readonly IStringLocalizer<TaskReviewActionItemService> _localizer;
        public TaskReviewActionItemService(IActionItem_OperationType_LinksService actionItem_OperationType_LinksService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IActionItemService actionItemService, IStringLocalizer<TaskReviewActionItemService> localizer)
        {
            _actionItem_OperationType_LinksService = actionItem_OperationType_LinksService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _actionItemService = actionItemService;
            _localizer = localizer;
        }
        public List<string> GetActionItemTypesAsync()
        {
            var actionItemTypeList = new List<string>();
            var actionItemOperations = ExtensionMethods.GetOperationTypeByActionType();
            foreach(var item in actionItemOperations)
            {
                actionItemTypeList.Add(item.ActionItemType);
            }
            return actionItemTypeList.ToList();
        }

        public async Task<List<TaskReviewActionItem_OperationType_VM>> GetOperationTypesAsync(string actionItemType)
        {
            var operationTypeList = new List<TaskReviewActionItem_OperationType_VM>();
            var actionOperationList = ExtensionMethods.GetOperationTypeByActionType();
            var actionItemOperationName = actionOperationList.FirstOrDefault(x => x.ActionItemType == actionItemType)?.ActionItemOperationName;
            var operationTypes = await _actionItem_OperationType_LinksService.FindWithIncludeAsync(r => r.ActionItemOperationName == actionItemOperationName,new string[] { "OperationType" });
                foreach(var opt in operationTypes)
                {
                    var operationType = new TaskReviewActionItem_OperationType_VM(opt.OperationTypeId, opt.OperationType.Type);
                    operationTypeList.Add(operationType);
                }
            return operationTypeList.ToList();
        }

        public async Task<TaskReviewActionItem_VM> GetAsync(int id)
        {
            var actionItem = await _actionItemService.GetActionItemWithIncludeAsync(id);
            var taskReviewActionItem = new TaskReviewActionItem_VM()
            {
                Id = actionItem.Id,
                Type = actionItem.ActionItemTypeToDisplay,
                AssigneeId = actionItem.AssigneeId,
                PriorityId = actionItem.PriorityId,
                Notes = actionItem.Notes,
                AssignedDate = actionItem.AssignedDate,
                DueDate = actionItem.DueDate,
                SubDuty_Operations = (actionItem as SubDutyArea_ActionItem)?.ActionItem_SubDuty_Operations.Select(m => new TaskReviewActionItem_SubDutyArea_Operation_VM(m.Id, m.OperationTypeId, m.SubDutyAreaId,m.OperationType.Type)).ToList(),
                Step_Operations = (actionItem as Steps_ActionItem)?.ActionItem_Step_Operations.Select(m => new TaskReviewActionItem_Step_Operation_VM(m.Id, m.OperationTypeId, m.Task_StepId,m.Description, m.OperationType.Type)).ToList(),
                QuestionAndAnswer_Operations = (actionItem as QuestionAndAnswer_ActionItem)?.ActionItem_QuestionAndAnswer_Operations.Select(m => new TaskReviewActionItem_QuestionAndAnswer_Operation_VM(m.Id, m.OperationTypeId,m.Task_QuestionId, m.Question,m.Answer, m.OperationType.Type)).ToList(),
                Suggestion_Operations = (actionItem as Task_Specific_Suggestions_ActionItem)?.ActionItem_Suggestion_Operations.Select(m => new TaskReviewActionItem_Suggestion_Operation_VM(m.Id, m.OperationTypeId, m.TaskSuggestionId,m.Description, m.OperationType?.Type)).ToList(),
                EnablingObjective_Operations = (actionItem as EnablingObjective_ActionItem)?.ActionItem_EnablingObjective_Operations.Select(m => new TaskReviewActionItem_EnablingObjective_Operation_VM(m.Id, m.OperationTypeId,m.EnablingObjectiveId.GetValueOrDefault(), m.OperationType.Type)).ToList(),
                Tool_Operations = (actionItem as Tools_ActionItem)?.ActionItem_Tool_Operations.Select(m => new TaskReviewActionItem_Tool_Operation_VM(m.Id, m.OperationTypeId,m.ToolId.GetValueOrDefault(), m.OperationType.Type)).ToList(),
                RegulatoryRequirement_Operations = (actionItem as RegulatoryRequirements_ActionItem)?.ActionItem_RegulatoryRequirement_Operations.Select(m => new TaskReviewActionItem_RegulatoryRequirement_Operation_VM(m.Id, m.OperationTypeId,m.RegulatoryRequirementId.GetValueOrDefault(), m.OperationType.Type)).ToList(),
                Procedure_Operations = (actionItem as Procedure_ActionItem)?.ActionItem_Procedure_Operations.Select(m => new TaskReviewActionItem_Procedure_Operation_VM(m.Id, m.OperationTypeId,m.ProcedureId.GetValueOrDefault(), m.OperationType.Type)).ToList(),
                SafetyHazard_Operations = (actionItem as SafetyHazards_ActionItem)?.ActionItem_SafetyHazard_Operations.Select(m => new TaskReviewActionItem_SafetyHazard_Operation_VM(m.Id, m.OperationTypeId,m.SafetyHazardId.GetValueOrDefault(), m.OperationType.Type)).ToList(),
                Task_number = (actionItem as Task_ActionItem)?.Number,
                Task_statement = (actionItem as Task_ActionItem)?.Statement,
                Criteria = (actionItem as Criteria_ActionItem)?.Criteria,
                Conditions = (actionItem as Conditions_ActionItem)?.Conditions,
                References = (actionItem as References_ActionItem)?.References,
                IsMeta = (actionItem as MetaTask_ActionItem)?.IsMeta,
                Priority = actionItem.Priority?.Type,
                Assignees = $"{actionItem.Assignee?.Person?.FirstName} {actionItem.Assignee?.Person?.LastName}"
            };
            return taskReviewActionItem;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await _actionItemService.GetActionItemWithIncludeAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ActionItemOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _actionItemService.UpdateAsync(obj);
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

        public async Task<TaskReviewActionItem_VM> UpdateAsync(int id, TaskReviewActionItem_VM options)
        {
            var actionItem = await _actionItemService.GetActionItemWithIncludeAsync(id);
            if (actionItem == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, actionItem, ActionItemOperations.Update);
            if (result.Succeeded)
            {
                actionItem.ActionItemTypeToDisplay = options.Type;
                actionItem.AssigneeId = options.AssigneeId;
                actionItem.PriorityId = options.PriorityId;
                actionItem.Notes = options.Notes;
                actionItem.AssignedDate = options.AssignedDate;
                actionItem.DueDate = options.DueDate;
                if (actionItem is SubDutyArea_ActionItem subDutyAreaActionItem)
                {
                    var createSubDutyOperations = options.SubDuty_Operations.Where(y => y.Id == 0).ToList();
                    var editSuggestionOperations = options.SubDuty_Operations.Except(createSubDutyOperations).ToList();
                    subDutyAreaActionItem.ActionItem_SubDuty_Operations.Where(y => !editSuggestionOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createSubDutyOperations.ForEach(x => subDutyAreaActionItem.AddSubDutyOperation(x.OperationTypeId, x.SubDutyAreaId));
                    editSuggestionOperations.ForEach(x => subDutyAreaActionItem.UpdateSubDutyOperation(x.Id, x.OperationTypeId, x.SubDutyAreaId));
                    actionItem = await UpdateActionItemAsync(subDutyAreaActionItem);
                }

                if (actionItem is Steps_ActionItem stepActionItem)
                {
                    var createStepOperations = options.Step_Operations.Where(y => y.Id == 0).ToList();
                    var editStepOperations = options.Step_Operations.Except(createStepOperations).ToList();
                    stepActionItem.ActionItem_Step_Operations.Where(y => !editStepOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createStepOperations.ForEach(x => stepActionItem.AddStepOperation(x.OperationTypeId, x.Task_StepId,x.Description));
                    editStepOperations.ForEach(x => stepActionItem.UpdateStepOperation(x.Id, x.OperationTypeId, x.Task_StepId,x.Description));
                    actionItem = await UpdateActionItemAsync(stepActionItem);
                }

                if (actionItem is QuestionAndAnswer_ActionItem qaActionItem)
                {
                    var createQuesAnsOperations = options.QuestionAndAnswer_Operations.Where(y => y.Id == 0).ToList();
                    var editQuesAnsOperations = options.QuestionAndAnswer_Operations.Except(createQuesAnsOperations).ToList();
                    qaActionItem.ActionItem_QuestionAndAnswer_Operations.Where(y => !editQuesAnsOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createQuesAnsOperations.ForEach(x => qaActionItem.AddQuestionAnswerOperation(x.OperationTypeId, x.Task_QuestionId, x.Question,x.Answer));
                    editQuesAnsOperations.ForEach(x => qaActionItem.UpdateQuesAnsOperation(x.Id, x.OperationTypeId, x.Task_QuestionId,x.Question,x.Answer));
                    actionItem = await UpdateActionItemAsync(qaActionItem);
                }

                if (actionItem is Task_Specific_Suggestions_ActionItem suggestionActionItem)
                {
                    var createSuggestionOperations = options.Suggestion_Operations.Where(y => y.Id == 0).ToList();
                    var editSuggestionOperations = options.Suggestion_Operations.Except(createSuggestionOperations).ToList();
                    suggestionActionItem.ActionItem_Suggestion_Operations.Where(y => !editSuggestionOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createSuggestionOperations.ForEach(x => suggestionActionItem.AddSuggestionOperation(x.OperationTypeId, x.Task_SuggestionId, x.Description));
                    editSuggestionOperations.ForEach(x => suggestionActionItem.UpdateSuggestionOperation(x.Id, x.OperationTypeId, x.Task_SuggestionId, x.Description));
                    actionItem = await UpdateActionItemAsync(suggestionActionItem);
                }

                if (actionItem is EnablingObjective_ActionItem eoActionItem)
                {
                    var createEoOperations = options.EnablingObjective_Operations.Where(y => y.Id == 0).ToList();
                    var editEoOperations = options.EnablingObjective_Operations.Except(createEoOperations).ToList();
                    eoActionItem.ActionItem_EnablingObjective_Operations.Where(y => !editEoOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createEoOperations.ForEach(x => eoActionItem.AddEnablingObjectiveOperation(x.OperationTypeId, x.EnablingObjectiveId));
                    editEoOperations.ForEach(x => eoActionItem.UpdateEnablingObjectiveOperation(x.Id, x.OperationTypeId, x.EnablingObjectiveId));
                    actionItem = await UpdateActionItemAsync(eoActionItem);
                }

                if (actionItem is Tools_ActionItem toolActionItem)
                {
                    var createToolOperations = options.Tool_Operations.Where(y => y.Id == 0).ToList();
                    var editToolOperations = options.Tool_Operations.Except(createToolOperations).ToList();
                    toolActionItem.ActionItem_Tool_Operations.Where(y => !editToolOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createToolOperations.ForEach(x => toolActionItem.AddToolOperation(x.OperationTypeId, x.ToolId));
                    editToolOperations.ForEach(x => toolActionItem.UpdateToolOperation(x.Id, x.OperationTypeId, x.ToolId));
                    actionItem = await UpdateActionItemAsync(toolActionItem);
                }

                if (actionItem is RegulatoryRequirements_ActionItem rrActionItem)
                {
                    var createRROperations = options.RegulatoryRequirement_Operations.Where(y => y.Id == 0).ToList();
                    var editRROperations = options.RegulatoryRequirement_Operations.Except(createRROperations).ToList();
                    rrActionItem.ActionItem_RegulatoryRequirement_Operations.Where(y => !editRROperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createRROperations.ForEach(x => rrActionItem.AddRegulatoryRequirementOperation(x.OperationTypeId, x.RegulatoryRequirementId));
                    editRROperations.ForEach(x => rrActionItem.UpdateRegulatoryRequirementOperation(x.Id, x.OperationTypeId, x.RegulatoryRequirementId));
                    actionItem = await UpdateActionItemAsync(rrActionItem);
                }

                if (actionItem is Procedure_ActionItem procedureActionItem)
                {
                    var createProcedureOperations = options.Procedure_Operations.Where(y => y.Id == 0).ToList();
                    var editProcedureOperations = options.Procedure_Operations.Except(createProcedureOperations).ToList();
                    procedureActionItem.ActionItem_Procedure_Operations.Where(y => !editProcedureOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createProcedureOperations.ForEach(x => procedureActionItem.AddProcedureOperation(x.OperationTypeId, x.ProcedureId));
                    editProcedureOperations.ForEach(x => procedureActionItem.UpdateProcedureOperation(x.Id, x.OperationTypeId, x.ProcedureId));
                    actionItem = await UpdateActionItemAsync(procedureActionItem);
                }

                if (actionItem is SafetyHazards_ActionItem safetyHazardActionItem)
                {
                    var createSafetyHazardOperations = options.SafetyHazard_Operations.Where(y => y.Id == 0).ToList();
                    var editSafetyHazardOperations = options.SafetyHazard_Operations.Except(createSafetyHazardOperations).ToList();
                    safetyHazardActionItem.ActionItem_SafetyHazard_Operations.Where(y => !editSafetyHazardOperations.Any(x => x.Id == y.Id)).ToList().ForEach(x => x.Delete());
                    createSafetyHazardOperations.ForEach(x => safetyHazardActionItem.AddSafetyhazardOperation(x.OperationTypeId, x.SafetyHazardId));
                    editSafetyHazardOperations.ForEach(x => safetyHazardActionItem.UpdateSafetyHazardOperation(x.Id, x.OperationTypeId, x.SafetyHazardId));
                    actionItem = await UpdateActionItemAsync(safetyHazardActionItem);
                }

                if (actionItem is Task_ActionItem taskActionItem)
                {
                    taskActionItem.Number = options.Task_number;
                    taskActionItem.Statement = options.Task_statement;
                }

                if (actionItem is Criteria_ActionItem criteriaActionItem)
                {
                    criteriaActionItem.Criteria = options.Criteria;
                }

                if (actionItem is Conditions_ActionItem conditionsActionItem)
                {
                    conditionsActionItem.Conditions = options.Conditions;
                }

                if (actionItem is References_ActionItem referencesActionItem)
                {
                    referencesActionItem.References = options.References;
                }

                if (actionItem is MetaTask_ActionItem metaTaskActionItem)
                {
                    metaTaskActionItem.IsMeta = options.IsMeta;
                }

                var validationResult = await _actionItemService.UpdateAsync(actionItem);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return await GetAsync(actionItem.Id);
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<ActionItem> CreateActionItemAsync(ActionItem actionItem)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, actionItem, ActionItemOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _actionItemService.AddAsync(actionItem);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return actionItem;
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<ActionItem> UpdateActionItemAsync(ActionItem actionItem)
        {
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, actionItem, ActionItemOperations.Update);
            if (result.Succeeded)
            {
                var validationResult = await _actionItemService.UpdateAsync(actionItem);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return actionItem;
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
