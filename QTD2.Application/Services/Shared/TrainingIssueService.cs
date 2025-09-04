using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITrainingIssueDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssueService;
using ITrainingIssue_DataElementDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingIssue_DataElementService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueService : ITrainingIssueService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TrainingIssueService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITrainingIssueDomainService _trainingIssueDomainService;
        private readonly ITrainingIssue_DataElementDomainService _trainingIssueDataElementDomainService;

        public TrainingIssueService(IAuthorizationService authorizationService,
        IStringLocalizer<TrainingIssueService> localizer,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        ITrainingIssueDomainService trainingIssueDomainService,
        ITrainingIssue_DataElementDomainService trainingIssueDataElementDomainService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _trainingIssueDomainService = trainingIssueDomainService;
            _trainingIssueDataElementDomainService = trainingIssueDataElementDomainService;
        }

        public async Task<TrainingIssueOverview_VM> GetOverviewAsync()
        {
            var trainingIssues = await _trainingIssueDomainService.AllWithIncludeAsync(new string[] { "Status", "Severity", "DriverType", "DriverSubType", "ActionItems.Status" });
            if (trainingIssues != null)
            {
                var trainingIssuesOpen = trainingIssues.Where(r => r.StatusId == 1).Count();
                var trainingIssuesClosed = trainingIssues.Where(r => r.StatusId == 2).Count();
                var trainingIssuesWithPendingActionItems = await GetWithPendingActionItemsAsync();
                var trainingIssuesWithNoActionItems = await GetWithNoActionItemsAsync();
                var trainingIssueOverview_VM = new TrainingIssueOverview_VM(trainingIssuesOpen, trainingIssuesClosed, trainingIssuesWithPendingActionItems.Count, trainingIssuesWithNoActionItems.Count);
                foreach (var training in trainingIssues)
                {
                    var trainingIssueVM = new TrainingIssueOverview_TrainingIssue_VM(training.Id, training.IssueID, training.DueDate, training.IssueTitle, training.Severity.Severity, training.DriverType?.Type, training.DriverSubType?.SubType, String.Join(", ", training.ActionItems.Where(ai => ai.StatusId == 1 || ai.StatusId == 2).Select(x => x.ActionItemName)), training.Status.Status, training.Active, training.TaskReviewId);
                    trainingIssueOverview_VM.TrainingIssues.Add(trainingIssueVM);
                }
                return trainingIssueOverview_VM;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<List<TrainingIssue_VM>> GetWithPendingActionItemsAsync()
        {
            var trainingIssues = await _trainingIssueDomainService.FindWithIncludeAsync(r => r.ActionItems.Any(ai => ai.StatusId == 1 || ai.StatusId == 2), new string[] { "Status", "Severity", "DriverType", "ActionItems.Status", "ActionItems.Priority", "ActionItems" });
            var trainingIssueVMs = new List<TrainingIssue_VM>();

            foreach (var training in trainingIssues)
            {
                var trainingIssue_VM = MapTrainingIssue_VM(training);
                foreach (var actionItem in training.ActionItems.Where(r => r.StatusId == 1 || r.StatusId == 2))
                {
                    var trainingIssue_ActionItemVM = MapTrainingIssue_ActionItem_VM(actionItem);
                    trainingIssue_VM.ActionItems.Add(trainingIssue_ActionItemVM);
                }
                trainingIssueVMs.Add(trainingIssue_VM);
            }
            return trainingIssueVMs;
        }

        public async Task<List<TrainingIssue_VM>> GetWithNoActionItemsAsync()
        {
            var trainingIssues = await _trainingIssueDomainService.FindWithIncludeAsync(r => !r.ActionItems.Any(), new string[] { "Status", "Severity", "DriverType", "ActionItems.Status" });
            var trainingIssueVMs = new List<TrainingIssue_VM>();

            foreach (var training in trainingIssues)
            {
                var trainingIssue_VM = MapTrainingIssue_VM(training);
                trainingIssueVMs.Add(trainingIssue_VM);
            }
            return trainingIssueVMs;
        }

        public async Task<TrainingIssue_VM> CreateAsync(TrainingIssue_VM options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var trainingIssue = new QTD2.Domain.Entities.Core.TrainingIssue(options.IssueCode, options.IssueTitle, options.Description, options.CreatedDate, options.DueDate, options.StatusId, options.SeverityId, options.TaskReviewId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingIssue, AuthorizationOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    trainingIssue.Create(userName.Id);

                    var validationResult = await _trainingIssueDomainService.AddAsync(trainingIssue);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return await GetAsync(trainingIssue.Id);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
        }

        public async Task<TrainingIssue_VM> GetAsync(int id)
        {
            var trainingIssue = await _trainingIssueDomainService.GetWithAllIncludesAsync(id);
            if (trainingIssue != null)
            {
                var trainingIssueVM = MapTrainingIssue_VM(trainingIssue);
                var orderedActionItems = trainingIssue.ActionItems.OrderBy(x => x.Order);
                foreach (var actionItem in orderedActionItems)
                {
                    var trainingIssue_ActionItemVM = MapTrainingIssue_ActionItem_VM(actionItem);
                    trainingIssueVM.ActionItems.Add(trainingIssue_ActionItemVM);
                }

                var dataElement = trainingIssue.DataElementId != null ? await MapTrainingIssueDataElementVMAsync(trainingIssue.DataElementId.GetValueOrDefault()) : null;
                trainingIssueVM.DataElement = dataElement;
                return trainingIssueVM;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task<object> CopyAsync(int id)
        {
            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var obj = await _trainingIssueDomainService.GetForCopy(id);
            var copy = obj.Copy<TrainingIssue>(createdBy);

            var result = await _trainingIssueDomainService.AddAsync(copy);
            if (!result.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', result.Errors));
            }

            if (copy.DataElement != null)
            {
                copy.DataElement.TrainingIssueId = copy.Id;

                copy.DataElementId = copy.DataElement.Id;

                var updateResult = await _trainingIssueDomainService.UpdateAsync(copy);
                if (!updateResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', updateResult.Errors));
                }
            }

            return copy;
        }


        public async System.Threading.Tasks.Task ActivateAsync(int id)
        {
            var trainingIssue = await _trainingIssueDomainService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingIssue, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                trainingIssue.Activate();
                var validationResult = await _trainingIssueDomainService.UpdateAsync(trainingIssue);
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

        public async System.Threading.Tasks.Task InactivateAsync(int id)
        {
            var trainingIssue = await _trainingIssueDomainService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingIssue, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                trainingIssue.Deactivate();
                var validationResult = await _trainingIssueDomainService.UpdateAsync(trainingIssue);
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

        public async System.Threading.Tasks.Task DeleteAsync(int trainingIssueId)
        {
            var trainingIssue = await _trainingIssueDomainService.GetWithIncludeAsync(trainingIssueId, new[] { "ActionItems", "DataElement" });
            if (trainingIssue == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingIssue, AuthorizationOperations.Update);
            if (result.Succeeded)
            {
                trainingIssue.Delete();
                var validationResult = await _trainingIssueDomainService.UpdateAsync(trainingIssue);
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

        public async Task<TrainingIssue_VM> UpdateAsync(int id, TrainingIssue_VM options)
        {
            var trainingIssue = await _trainingIssueDomainService.GetAsync(id);
            if (trainingIssue == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, trainingIssue, AuthorizationOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
            trainingIssue.IssueID = options.IssueCode;
            trainingIssue.IssueTitle = options.IssueTitle;
            trainingIssue.Description = options.Description;
            trainingIssue.TrainingIssueCreatedDate = options.CreatedDate;
            trainingIssue.DueDate = options.DueDate;
            trainingIssue.StatusId = options.StatusId;
            trainingIssue.SeverityId = options.SeverityId;
            trainingIssue.DriverTypeId = options.DriverTypeId;
            trainingIssue.DriverSubTypeId = options.DriverSubTypeId;
            trainingIssue.OtherComments = options.OtherComments;
            trainingIssue.DataElementId = options.DataElement?.Id;
            trainingIssue.PlannedResponse = options.PlannedResponse;
            trainingIssue.TaskReviewId = options.TaskReviewId;

            var validationResult = await _trainingIssueDomainService.UpdateAsync(trainingIssue);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return await GetAsync(trainingIssue.Id);
            }
        }

        public async Task<List<TrainingIssue_ActionItem_VM>> UpdateActionItemsAsync(int id, TrainingIssue_ActionItems_VM options, bool isStatusCheck)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var trainingIssue = await _trainingIssueDomainService.GetWithIncludeAsync(id, new[] { "ActionItems", "ActionItems.Priority", "ActionItems.Status" });
            if (trainingIssue == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var currentActionItem in trainingIssue.ActionItems)
            {
                var actionItem = options.ActionItem_VMs.FirstOrDefault(x => x.Id == currentActionItem.Id);
                if (actionItem == null)
                {
                    currentActionItem.Delete();
                    currentActionItem.Modify(userName);
                    if (isStatusCheck)
                    {
                        trainingIssue.UpdateActionItems();
                    }
                }
            }

            foreach (var trainingActionItem in options.ActionItem_VMs)
            {
                if (trainingActionItem.Id != null)
                {
                    var existingActionItem = trainingIssue.ActionItems.FirstOrDefault(ai => ai.Id == trainingActionItem.Id);
                    if (existingActionItem != null)
                    {
                        existingActionItem.UpdateActionItem(trainingActionItem.Order, trainingActionItem.ActionItemName, trainingActionItem.PriorityId, trainingActionItem.DateAssigned, trainingActionItem.DueDate, trainingActionItem.DateCompleted, trainingActionItem.StatusId, trainingActionItem.Notes,trainingActionItem.AssigneeName);
                        existingActionItem.Modify(userName);
                        if (isStatusCheck)
                        {
                            trainingIssue.UpdateActionItems();
                        }
                    }
                }
                else
                {
                    var actionItem = new TrainingIssue_ActionItem(id, trainingActionItem.Order, trainingActionItem.ActionItemName, trainingActionItem.PriorityId, trainingActionItem.DateAssigned, trainingActionItem.DueDate, trainingActionItem.DateCompleted, trainingActionItem.StatusId, trainingActionItem.Notes,trainingActionItem.AssigneeName);
                    actionItem.Create(userName);
                    trainingIssue.ActionItems.Add(actionItem);
                    if (isStatusCheck)
                    {
                        trainingIssue.UpdateActionItems();
                    }
                }
            }

            var validationResult = await _trainingIssueDomainService.UpdateAsync(trainingIssue);
            var actionItemList = new List<TrainingIssue_ActionItem_VM>();
            if (validationResult.IsValid)
            {
                var updatedTrainingIssues = await _trainingIssueDomainService.GetWithAllIncludesAsync(trainingIssue.Id);
                foreach (var ai in updatedTrainingIssues.ActionItems.Where(x=>!x.Deleted))
                {
                    var actionItem = MapTrainingIssue_ActionItem_VM(ai);
                    actionItemList.Add(actionItem);
                }
                return actionItemList.OrderBy(x => x.Order).ToList();
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<TrainingIssue_DataElement_VM> UpdateDataElementAsync(int id, TrainingIssue_DataElement_VM option)
        {
            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var trainingIssue = (await _trainingIssueDomainService.FindWithIncludeAsync(x => x.Id == id, new[] { "DataElement" })).FirstOrDefault();
            if (trainingIssue == null)
            {
                throw new ArgumentNullException();
            }
            if (trainingIssue.DataElement != null)
            {
                if (trainingIssue.DataElement.DataElementDisplay == option.DataElementType)
                {
                    trainingIssue.DataElement.Update(option.DataElementId);
                }
                else if (trainingIssue.DataElement.DataElementDisplay != option.DataElementType)
                {
                    trainingIssue.DataElement.Delete();
                    await _trainingIssueDataElementDomainService.UpdateAsync(trainingIssue.DataElement);
                    var dataElement = await CreateTrainingIssueDataElementAsync(id, option);
                    trainingIssue.UpdateDataElement(dataElement);
                }
            }
            else
            {
                var dataElement = await CreateTrainingIssueDataElementAsync(id, option);
                trainingIssue.UpdateDataElement(dataElement);
            }
            var validationResult = await  _trainingIssueDomainService.UpdateAsync(trainingIssue); 
            return  await MapTrainingIssueDataElementVMAsync(trainingIssue.DataElementId.GetValueOrDefault());
        }

        public async Task<TrainingIssue_DataElement> CreateTrainingIssueDataElementAsync(int trainingIssueId, TrainingIssue_DataElement_VM option)
        {
            TrainingIssue_DataElement trainingIssueDataElement = null;
            switch (option.DataElementType)
            {
                case "Procedure":
                    trainingIssueDataElement = new DataElement_Procedure(trainingIssueId, option.DataElementId);
                    break;
                case "RegulatoryRequirement":
                    trainingIssueDataElement = new DataElement_RegulatoryRequirement(trainingIssueId, option.DataElementId);
                    break;
                case "SafetyHazard":
                    trainingIssueDataElement = new DataElement_SafetyHazard(trainingIssueId, option.DataElementId);
                    break;
                case "Tool":
                    trainingIssueDataElement = new DataElement_Tool(trainingIssueId, option.DataElementId);
                    break;
                case "ILAsCourses":
                    trainingIssueDataElement = new DataElement_ILAsCourses(trainingIssueId, option.DataElementId);
                    break;
                case "Test":
                    trainingIssueDataElement = new DataElement_Test(trainingIssueId, option.DataElementId);
                    break;
                case "Pretest":
                    trainingIssueDataElement = new DataElement_Pretest(trainingIssueId, option.DataElementId);
                    break;
                case "MetaILAsCourses":
                    trainingIssueDataElement = new DataElement_MetaILAsCourses(trainingIssueId, option.DataElementId);
                    break;
                case "ComputerBasedTraining":
                    trainingIssueDataElement = new DataElement_ComputerBasedTraining(trainingIssueId, option.DataElementId);
                    break;
                case "TrainingProgram":
                    trainingIssueDataElement = new DataElement_TrainingProgram(trainingIssueId, option.DataElementId);
                    break;
                case "Task":
                    trainingIssueDataElement = new DataElement_Task(trainingIssueId, option.DataElementId);
                    break;
                case "TestItem":
                    trainingIssueDataElement = new DataElement_TestItem(trainingIssueId, option.DataElementId);
                    break;
                case "EnablingObjective":
                    trainingIssueDataElement = new DataElement_EnablingObjective(trainingIssueId, option.DataElementId);
                    break;
                case "MetaTask":
                    trainingIssueDataElement = new DataElement_MetaTask(trainingIssueId, option.DataElementId);
                    break;
                case "MetaEnablingObjective":
                    trainingIssueDataElement = new DataElement_MetaEnablingObjective(trainingIssueId, option.DataElementId);
                    break;
            }
            if (trainingIssueDataElement != null)
            {
                await _trainingIssueDataElementDomainService.AddAsync(trainingIssueDataElement);
            }
            return trainingIssueDataElement;
        }

        public TrainingIssue_VM MapTrainingIssue_VM(TrainingIssue trainingIssue)
        {
            var trainingIssue_VM = new TrainingIssue_VM(trainingIssue.Id, trainingIssue.IssueID, trainingIssue.IssueTitle, trainingIssue.Description, trainingIssue.TrainingIssueCreatedDate, trainingIssue.DueDate, trainingIssue.StatusId, trainingIssue.SeverityId, trainingIssue.DriverTypeId, trainingIssue.DriverSubTypeId, trainingIssue.OtherComments, trainingIssue.PlannedResponse, trainingIssue.Status.Status, trainingIssue.Severity.Severity,trainingIssue.DriverType?.Type,trainingIssue.DriverSubType?.SubType, trainingIssue.TaskReviewId);
            return trainingIssue_VM;
        }

        public TrainingIssue_ActionItem_VM MapTrainingIssue_ActionItem_VM(TrainingIssue_ActionItem trainingIssue_ActionItem)
        {
            var trainingIssue_ActionItem_VM = new TrainingIssue_ActionItem_VM(trainingIssue_ActionItem.Id, trainingIssue_ActionItem.Order, trainingIssue_ActionItem.ActionItemName, trainingIssue_ActionItem.PriorityId, trainingIssue_ActionItem.DateAssigned, trainingIssue_ActionItem.DueDate, trainingIssue_ActionItem.DateCompleted, trainingIssue_ActionItem.StatusId, trainingIssue_ActionItem.Notes, trainingIssue_ActionItem.Priority?.Priority, trainingIssue_ActionItem.Status?.Status,trainingIssue_ActionItem.AssigneeName);
            return trainingIssue_ActionItem_VM;
        }
        public async Task<TrainingIssue_DataElement_VM> MapTrainingIssueDataElementVMAsync(int dataElementId)
        {
            var trainingIssue_DataElement = await _trainingIssueDataElementDomainService.GetDataElementWithAllIncludesAsync(dataElementId);
            if(trainingIssue_DataElement != null)
            {
                var dataElementVM = ExtensionMethods.GetAllDataElementsWithCategories().SelectMany(x => x.DataElementVMs).FirstOrDefault(x => x.DataElementType == trainingIssue_DataElement.DataElementDisplay);
                switch (trainingIssue_DataElement)
                {
                    case DataElement_Procedure dataElementProcedure:
                        dataElementVM.DataElementId = dataElementProcedure.ProcedureId;
                        dataElementVM.DataElementDescription = dataElementProcedure.Procedure != null ? $"{dataElementProcedure.Procedure.Number} - {dataElementProcedure.Procedure.Title}" : null;
                        break;
                    case DataElement_RegulatoryRequirement dataElementRegRequirement:
                        dataElementVM.DataElementId = dataElementRegRequirement.RegulatoryRequirementId;
                        dataElementVM.DataElementDescription = dataElementRegRequirement.RegulatoryRequirement != null ? $"{dataElementRegRequirement.RegulatoryRequirement.Number} - {dataElementRegRequirement.RegulatoryRequirement.Title}" : null;
                        break;
                    case DataElement_SafetyHazard dataElementSafetyHazard:
                        dataElementVM.DataElementId = dataElementSafetyHazard.SafetyHazardId;
                        dataElementVM.DataElementDescription = dataElementSafetyHazard.SafetyHazard != null ? $"{dataElementSafetyHazard.SafetyHazard.Number} - {dataElementSafetyHazard.SafetyHazard.Title}" : null;
                        break;
                    case DataElement_Tool dataElementTool:
                        dataElementVM.DataElementId = dataElementTool.ToolId;
                        dataElementVM.DataElementDescription = dataElementTool.Tool != null ? $"{dataElementTool.Tool.Number} - {dataElementTool.Tool.Name}" : null;
                        break;
                    case DataElement_ILAsCourses dataElementILAsCourses:
                        dataElementVM.DataElementId = dataElementILAsCourses.ILAId;
                        dataElementVM.DataElementDescription = dataElementILAsCourses.ILA != null ? $"{dataElementILAsCourses.ILA.Number} - {dataElementILAsCourses.ILA.Name}" : null;
                        break;
                    case DataElement_MetaILAsCourses dataElementMetaILAsCourses:
                        dataElementVM.DataElementId = dataElementMetaILAsCourses.MetaILAId;
                        dataElementVM.DataElementDescription = dataElementMetaILAsCourses.MetaILA != null ? $"{dataElementMetaILAsCourses.MetaILA.Name}" : null;
                        break;
                    case DataElement_Test dataElementTest:
                        dataElementVM.DataElementId = dataElementTest.TestId;
                        dataElementVM.DataElementDescription = dataElementTest.Test != null ? $"{dataElementTest.Test.TestTitle}" : null;
                        break;
                    case DataElement_Pretest dataElementPretest:
                        dataElementVM.DataElementId = dataElementPretest.PreTestId;
                        dataElementVM.DataElementDescription = dataElementPretest.PreTest != null ? $"{dataElementPretest.PreTest.TestTitle}" : null;
                        break;
                    case DataElement_ComputerBasedTraining dataElementComputerBasedTraining:
                        dataElementVM.DataElementId = dataElementComputerBasedTraining.CBT_ScormUploadId;
                        dataElementVM.DataElementDescription = dataElementComputerBasedTraining.CBT_ScormUpload != null ? $"{dataElementComputerBasedTraining.CBT_ScormUpload.Name}" : null;
                        break;
                    case DataElement_TrainingProgram dataElement_TrainingProgram:
                        dataElementVM.DataElementId = dataElement_TrainingProgram.TrainingProgramId;
                        dataElementVM.DataElementDescription = dataElement_TrainingProgram.TrainingProgram != null ? $"{dataElement_TrainingProgram.TrainingProgram.Version}" : null;
                        break;
                    case DataElement_Task dataElementTask:
                        dataElementVM.DataElementId = dataElementTask.TaskId;
                        dataElementVM.DataElementDescription = dataElementTask.Task != null ? $"{dataElementTask.Task.FullNumber} - {dataElementTask.Task.Description}" : null;
                        break;
                    case DataElement_TestItem dataElementTestItem:
                        dataElementVM.DataElementId = dataElementTestItem.TestItemId;
                        dataElementVM.DataElementDescription = dataElementTestItem.TestItem != null ? $"{dataElementTestItem.TestItem.Description}" : null;
                        break;
                    case DataElement_EnablingObjective dataElement_EnablingObjective:
                        dataElementVM.DataElementId = dataElement_EnablingObjective.EnablingObjectiveId;
                        dataElementVM.DataElementDescription = dataElement_EnablingObjective.EnablingObjective != null ? $"{dataElement_EnablingObjective.EnablingObjective.FullNumber} - {dataElement_EnablingObjective.EnablingObjective.Description}" : null;
                        break;
                    case DataElement_MetaTask dataElement_MetaTask:
                        dataElementVM.DataElementId = dataElement_MetaTask.MetaTaskId;
                        dataElementVM.DataElementDescription = dataElement_MetaTask.MetaTask != null ? $"{dataElement_MetaTask.MetaTask.FullNumber} - {dataElement_MetaTask.MetaTask.Description}" : null;
                        break;
                    case DataElement_MetaEnablingObjective dataElement_MetaEnablingObjective:
                        dataElementVM.DataElementId = dataElement_MetaEnablingObjective.MetaEnablingObjectiveId;
                        dataElementVM.DataElementDescription = dataElement_MetaEnablingObjective.MetaEnablingObjective != null ? $"{dataElement_MetaEnablingObjective.MetaEnablingObjective.FullNumber} - {dataElement_MetaEnablingObjective.MetaEnablingObjective.Description}" : null;
                        break;
                }
                return new TrainingIssue_DataElement_VM(trainingIssue_DataElement.Id, dataElementVM.DataElementType, dataElementVM.DataElementId, trainingIssue_DataElement.DataElementDisplay,dataElementVM.DataElementDescription,dataElementVM.DataElementCategory);
            }
            else
            {
                return null;
            }

        }

        public List<TrainingIssue_DataElementCategory_VM> GetAllDataElementsWithCategories()
        {
            return ExtensionMethods.GetAllDataElementsWithCategories().ToList();
        }

        public async Task<List<TrainingIssue_VM>> GetTrainingIssueByDataElementTypeAndTypeIdAsync(int id, string type)
        {
            var trainingIssues = await _trainingIssueDomainService.GetAllTrainingIssuesByDataElementTypeAsync(type);

            trainingIssues = type switch
            {
                "EnablingObjective" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_EnablingObjective)?.EnablingObjectiveId == id)
                    .ToList(),

                "MetaEnablingObjective" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_MetaEnablingObjective)?.MetaEnablingObjectiveId == id)
                    .ToList(),

                "MetaTask" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_MetaTask)?.MetaTaskId == id)
                    .ToList(),

                "Procedure" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_Procedure)?.ProcedureId == id)
                    .ToList(),

                "RegulatoryRequirement" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_RegulatoryRequirement)?.RegulatoryRequirementId == id)
                    .ToList(),

                "SafetyHazard" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_SafetyHazard)?.SafetyHazardId == id)
                    .ToList(),

                "Task" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_Task)?.TaskId == id)
                    .ToList(),

                "Tool" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_Tool)?.ToolId == id)
                    .ToList(),

                "TrainingProgram" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_TrainingProgram)?.TrainingProgramId == id)
                    .ToList(),

                "ILAsCourses" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_ILAsCourses)?.ILAId == id)
                    .ToList(),

                "MetaILAsCourses" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_MetaILAsCourses)?.MetaILAId == id)
                    .ToList(),

                "ComputerBasedTraining" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_ComputerBasedTraining)?.CBT_ScormUploadId == id)
                    .ToList(),

                "TestItem" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_TestItem)?.TestItemId == id)
                    .ToList(),

                "Pretest" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_Pretest)?.PreTestId == id)
                    .ToList(),

                "Test" => trainingIssues
                    .Where(tr => (tr.DataElement as DataElement_Test)?.TestId == id)
                    .ToList(),

                _ => throw new ArgumentException("Invalid data element type", nameof(type))
            };

            return trainingIssues.Select(tr => MapTrainingIssue_VM(tr)).ToList();
        }

        public async Task<TrainingIssue_VM> GetTrainingIssueByTaskReviewIdAsync(int taskReviewId)
        {
            var trainingIssue = await _trainingIssueDomainService.GetByTaskReviewIdAsync(taskReviewId);
            if (trainingIssue != null)
            {
                var trainingIssueVM = MapTrainingIssue_VM(trainingIssue);
                var orderedActionItems = trainingIssue.ActionItems.OrderBy(x => x.Order);
                foreach (var actionItem in orderedActionItems)
                {
                    var trainingIssue_ActionItemVM = MapTrainingIssue_ActionItem_VM(actionItem);
                    trainingIssueVM.ActionItems.Add(trainingIssue_ActionItemVM);
                }

                var dataElement = trainingIssue.DataElementId != null ? await MapTrainingIssueDataElementVMAsync(trainingIssue.DataElementId.GetValueOrDefault()) : null;
                trainingIssueVM.DataElement = dataElement;
                return trainingIssueVM;
            }
            else
            {
                return null;
            }
        }
    }
}
