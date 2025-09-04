using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ActivityNotification;
using QTD2.Infrastructure.Model.TaskListReview;
using ITaskListReviewDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskListReviewService;
using ITaskReviewReviewerDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReview_ReviewerService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using ITaskReviewDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskReviewService;
using IPositionTaskDomainService = QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService;
using IClientUserSettings_GeneralSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClientUserSettings_GeneralSettingService;
using ITaskDomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IActionItemDomainService = QTD2.Domain.Interfaces.Service.Core.IActionItemService;
using QTD2.Infrastructure.Hashing.Interfaces;
using QTD2.Infrastructure.Model.Task_Review;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.ExtensionMethods;
using Microsoft.Extensions.Options;

namespace QTD2.Application.Services.Shared
{
    public class TaskListReviewService : ITaskListReviewService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TaskListReviewService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskListReviewDomainService _taskListReviewDomainService;
        private readonly ITaskReviewReviewerDomainService _taskReviewReviewerDomainService;
        private readonly IPersonDomainService _personDomainService;
        private readonly IPositionDomainService _positionDomainService;
        private readonly ITaskReviewDomainService _taskReviewDomainService;
        private readonly IPositionTaskDomainService _positionTaskService;
        private readonly IHasher _hasher;
        private readonly IClientUserSettings_GeneralSettingDomainService _generalSettingService;
        private readonly ITaskDomainService _taskDomainService;
        private readonly IActionItemDomainService _actionItemDomainService;


        public TaskListReviewService(IAuthorizationService authorizationService,
        IStringLocalizer<TaskListReviewService> localizer,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        ITaskListReviewDomainService taskListReviewDomainService,
        ITaskReviewReviewerDomainService taskReviewReviewerDomainService,
        IPersonDomainService personDomainService,
        IPositionDomainService positionDomainService,
        ITaskReviewDomainService taskReviewDomainService,
        IPositionTaskDomainService positionTaskService,
        IHasher hasher,
        IClientUserSettings_GeneralSettingDomainService generalSettingService, ITaskDomainService taskDomainService, IActionItemDomainService actionItemDomainService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _taskListReviewDomainService = taskListReviewDomainService;
            _taskReviewReviewerDomainService = taskReviewReviewerDomainService;
            _personDomainService = personDomainService;
            _positionDomainService = positionDomainService;
            _taskReviewDomainService = taskReviewDomainService;
            _positionTaskService = positionTaskService;
            _hasher = hasher;
            _generalSettingService = generalSettingService;
            _taskDomainService = taskDomainService;
            _actionItemDomainService = actionItemDomainService;
        }
        public async Task<TaskListReviewOverview_VM> GetOverviewAsync()
        {
            var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
            var myPendingTaskReviews = person != null ? (await _taskReviewReviewerDomainService.FindAsync(x => x.TaskReview.StatusId == 1 && x.Reviewer.PersonId == person.Id)).Count() : 0;
            var positionsWithoutTaskListReview = (await _positionDomainService.FindAsync(x=>x.Position_Tasks.All(a=> !a.Task.TaskReviews.Any()))).Count();
            var taskListReviewOverviewVM = new TaskListReviewOverview_VM(positionsWithoutTaskListReview, myPendingTaskReviews);
            var taskListReviews = await _taskListReviewDomainService.AllWithIncludeAsync(new string[] { "Status", "Type" , "TaskReviews.Finding" , "TaskReviews.Status","TaskReviews.Reviewers.Reviewer.Person", "TaskListReview_PositionLinks" });
            var distinctTaskIds = taskListReviews.SelectMany(tl => tl.TaskReviews).Select(tr => tr.TaskId).Distinct().ToList();
            var distinctTaskReviewIds = taskListReviews.SelectMany(tl => tl.TaskReviews).Select(x => x.Id).Distinct().ToList();
            var distinctPositionIds = taskListReviews.SelectMany(tl => tl.TaskListReview_PositionLinks).Select(p => p.PositionId).Distinct().ToList();
            var positions = (await _positionDomainService.GetByIdListAsync(distinctPositionIds)).ToList();
            var tasks = await _taskDomainService.GetTasksWithDutySubDutyAreaByTaskIdsAsync(distinctTaskIds);
            var actionItems = await _actionItemDomainService.GetActionItemsByTaskReviewIds(distinctTaskReviewIds);
            var positionTasks = await _positionTaskService.GetPositionTasksWithPositionByTaskIdsAsync(distinctTaskIds);
            var taskDict = tasks.ToDictionary(t => t.Id);
            var positionTaskLookup = positionTasks.ToLookup(pt => pt.TaskId);
            var actionItemLookup = actionItems.ToLookup(ai => ai.TaskReviewId);
            var positionDict = positions.ToDictionary(p => p.Id);
            foreach (var taskListReview in taskListReviews)
            {
                foreach (var pl in taskListReview.TaskListReview_PositionLinks)
                {
                    if (positionDict.TryGetValue(pl.PositionId, out var pos))
                        pl.Position = pos;
                    else
                        pl.Position = null; 
                }
                foreach (var tr in taskListReview.TaskReviews)
                {
                    if (taskDict.TryGetValue(tr.TaskId, out var task))
                    {
                        tr.Task = task;
                        tr.Task.Position_Tasks = positionTaskLookup[tr.TaskId].ToList();
                    }
                    tr.ActionItems = actionItemLookup[tr.Id].ToList();
                }
                var taskListReviewVM = new TaskListReviewOverview_TaskListReview_VM(taskListReview.Id, taskListReview.Title, taskListReview.Type.Type, taskListReview.StartDate, taskListReview.EndDate, taskListReview.Status.Type, taskListReview.ApprovalDate, taskListReview.Active,taskListReview.TaskListReview_PositionLinks.Select(pl=>pl.Position?.PositionTitle).Distinct().ToList());
                taskListReviewVM.TaskReviews = taskListReview.TaskReviews.Select(x => MapRecordTaskListReview_TaskReviewToVM(x, person)).ToList();
                taskListReviewOverviewVM.TaskListReviewOverview_TaskListReview_VMs.Add(taskListReviewVM);
            }
            return taskListReviewOverviewVM;
         
        }

        public async Task<TaskListReview_VM> CreateAsync(TaskListReview_VM options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            else
            {
                var taskListReview = new QTD2.Domain.Entities.Core.TaskListReview(options.Title, options.TypeId, options.StartDate, options.EndDate, 1, options.ReviewedBy);
                options.GeneralReviewers.ForEach(x => taskListReview.AddGeneralReviewer(x.QTDUserId));
                options.PositionIds.ForEach(p=>taskListReview.AddPosition(p));
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskListReview, TaskListReviewOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    taskListReview.Create(userName.Id);
                    var validationResult = await _taskListReviewDomainService.AddAsync(taskListReview);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {

                        return await GetAsync(taskListReview.Id);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }

            }
        }

        public async Task<TaskListReview_VM> UpdateAsync(int id, TaskListReview_VM options)
        {
            var taskListReview = await _taskListReviewDomainService.GetWithIncludeAsync(id, new string[] {"GeneralReviewers", "TaskReviews.Reviewers", "TaskListReview_PositionLinks" });
            if (taskListReview == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskListReview, TaskListReviewOperations.Update);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
            var currentPositions = new HashSet<int>(taskListReview.TaskListReview_PositionLinks.Select(m=>m.PositionId).Distinct().ToList() ?? new List<int>());
            var newPositions = new HashSet<int>(options.PositionIds ?? new List<int>());
            var isPositionChange = !currentPositions.SetEquals(newPositions);
            taskListReview.UpdateTaskListReview(options.Title,options.TypeId, options.StartDate, options.EndDate,options.Conclusion,options.ApprovalDate,options.Signature, options.ReviewedBy);
            var newPositionIds = options.PositionIds.Except(currentPositions).ToList();
            foreach (var posId in newPositionIds)
            {
                taskListReview.AddPosition(posId);
            }
            var removedPositionIds = currentPositions.Where(cp => !options.PositionIds.Contains(cp)).ToList();
            foreach(var posId in removedPositionIds)
            {
                taskListReview.RemovePosition(posId);
            }
            if (options.StatusId == 2) { taskListReview.Publish() ;}
            foreach (var currentReviewer in taskListReview.GeneralReviewers)
            {
                if (!options.GeneralReviewers.Any(x=>x.QTDUserId == currentReviewer.GeneralReviewerId))
                {
                    taskListReview.RemoveGeneralReviewer(currentReviewer);
                }
            }
            foreach (var newReviewer in options.GeneralReviewers)
            {
                if (!taskListReview.GeneralReviewers.Any(x => x.GeneralReviewerId == newReviewer.QTDUserId))
                {
                    taskListReview.AddGeneralReviewer(newReviewer.QTDUserId);
                }
            }
            if (isPositionChange)
            {
                var removedPositionTasks = await _positionTaskService.GetPositionTasksByPositionIds(removedPositionIds);
                var removedTaskIds = removedPositionTasks.Select(m => m.TaskId).Distinct().ToList();

                var remainingPositionTasks = await _positionTaskService.GetPositionTasksByPositionIds(newPositions.ToList());
                var remainingTaskIds = remainingPositionTasks.Select(m => m.TaskId).Distinct().ToList();

                var taskReviewToDelete = taskListReview.TaskReviews
                    .Where(tr => removedTaskIds.Contains(tr.TaskId) && !remainingTaskIds.Contains(tr.TaskId)).ToList();

                foreach (var t in taskReviewToDelete)
                {
                    t.Delete();
                }
            }
            var validationResult = await _taskListReviewDomainService.UpdateAsync(taskListReview);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return await GetAsync(id);
            }
        }

        public async Task<TaskListReview_VM> GetAsync(int taskListReviewId)
        {
            var taskListReview = await _taskListReviewDomainService.GeForVMAsync(taskListReviewId);
            if (taskListReview != null)
            {
                var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
                return MapRecordTaskListReviewToVM(taskListReview,person);
            }
            else
            {
                throw new QTDServerException("TaskListReview Id not found");
            }
        }

        public TaskListReview_VM MapRecordTaskListReviewToVM(TaskListReview taskListReview, Person person)
        {
            var generalReviewers = taskListReview.GeneralReviewers.Where(x=>!x.Deleted).Select(x => MapRecordTaskListReviewGeneralReviewerToVM(x)).ToList();
            var taskReviews = taskListReview.TaskReviews.Where(x=>!x.Deleted).Select( x =>  MapRecordTaskListReview_TaskReviewToVM(x,person)).ToList();
            TaskListReview_VM taskListReviewVM = new TaskListReview_VM(taskListReview.Id, taskListReview.Title, taskListReview.TypeId, taskListReview.StartDate, taskListReview.EndDate,taskListReview.StatusId, generalReviewers, taskReviews, taskListReview.Conclusion, taskListReview.ApprovalDate, taskListReview.Signature,taskListReview.Active,taskListReview.TaskListReview_PositionLinks.Where(p=>!p.Deleted).Select(m=>m.PositionId).Distinct().ToList(),taskListReview.ReviewedBy);
            return taskListReviewVM;
        }

        public TaskListReview_GeneralReviewer_VM MapRecordTaskListReviewGeneralReviewerToVM(TaskListReview_GeneralReviewer generalReviewer)
        {
            return new TaskListReview_GeneralReviewer_VM(generalReviewer.GeneralReviewerId, $"{generalReviewer?.GeneralReviewer?.Person.FirstName} {generalReviewer?.GeneralReviewer?.Person.LastName}");
        } 
        public TaskListReview_TaskReview_VM MapRecordTaskListReview_TaskReviewToVM(TaskReview taskReview , Person person)
        {
            var defaultTimeZone = GetDefaultTimeZone().Result;
            var taskReviewReviewers = taskReview.Reviewers.Where(x => !x.Deleted);
            var taskReviewVM = new TaskListReview_TaskReview_VM(taskReview.Id,taskReview.TaskId, taskReview.Task?.FullNumber, taskReview.Task?.Description, String.Join(',', taskReview.Task?.Position_Tasks.Select(x => x?.Position?.PositionAbbreviation)), taskReview?.ModifiedDate?.ConvertToDefaultTimeZone(defaultTimeZone), String.Join(",", taskReviewReviewers.Select(x => $"{x.Reviewer?.Person?.FirstName} {x.Reviewer?.Person?.LastName}")), taskReview.ReviewDate, taskReview.Finding?.Finding, taskReview.Status?.Status);
            taskReviewVM.Reviewers = taskReviewReviewers.Select(x => new TaskListReview_TaskReviewReviewer_VM(x.ReviewerId, $"{x.Reviewer.Person.LastName} {x.Reviewer.Person.FirstName}", (person != null && x.Reviewer.PersonId == person.Id))).ToList(); ;
            taskReviewVM.TaskReviewActionItems = taskReview.ActionItems.Where(x => !x.Deleted).Select(x => new TaskListReview_TaskReviewActionItem_VM(x.Id, x.ActionItemTypeToDisplay, $"{x.Assignee?.Person?.FirstName} {x.Assignee?.Person?.LastName}", x.Priority.Type, x.AssignedDate, x.DueDate)).ToList();
            return taskReviewVM;
        }
        public async Task<string> CopyAsync(int id)
        {
            var createdBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var obj = await _taskListReviewDomainService.GetForCopyAndDeleteAsync(id);
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            obj = obj.Copy<TaskListReview>(createdBy);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TaskListReviewOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _taskListReviewDomainService.AddAsync(obj);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {

                    return _hasher.Encode(obj.Id.ToString());
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var taskListReview = await _taskListReviewDomainService.GetForCopyAndDeleteAsync(id);
            if (taskListReview == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskListReview, TaskListReviewOperations.Delete);
            if (result.Succeeded)
            {
                taskListReview.Delete();
                var validationResult = await _taskListReviewDomainService.UpdateAsync(taskListReview);

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
        public async System.Threading.Tasks.Task ActivateInactivateAsync(int id,string type)
        {
            var taskListReview = await _taskListReviewDomainService.GetAsync(id);
            if (taskListReview == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskListReview, TaskListReviewOperations.Update);
            if (result.Succeeded)
            {
                switch (type)
                {
                    case "activate":
                        taskListReview.Activate();
                        break;
                    case "inactivate":
                        taskListReview.Deactivate();
                        break;
                }
                var validationResult = await _taskListReviewDomainService.UpdateAsync(taskListReview);

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

        public async Task<List<TaskListReview_TaskReview_VM>> CreateTaskReviewsAsync(int id, TaskReviewCreateOption option)
        {
            var taskListReview = await _taskListReviewDomainService.GetWithIncludeAsync(id, new string[] { "TaskReviews.Reviewers", "GeneralReviewers" });
            var createdTaskReviews = new List<TaskReview>();
            var createdTaskReviewsVM = new List<TaskListReview_TaskReview_VM>();
            if (taskListReview == null)
            {
                throw new ArgumentNullException();
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskListReview, TaskListReviewOperations.Update);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
            foreach(var taskId in option.TaskIds)
            {
                var taskReview = taskListReview.AddTaskReview(taskId);
                taskReview.SetStatusNotStarted();
                createdTaskReviews.Add(taskReview);
            }
            var validationResult = await _taskListReviewDomainService.UpdateAsync(taskListReview);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                var person = await _personDomainService.GetPersonByUserName(_httpContextAccessor.HttpContext?.User.Identity?.Name);
                foreach (var createdTaskReview in createdTaskReviews)
                {
                    var taskReview = await _taskReviewDomainService.GetWithIncludeAsync(createdTaskReview.Id, new string[] { "Task.Position_Tasks.Position", "Task.SubdutyArea.DutyArea", "Status", "Reviewers.Reviewer.Person" });
                    if(taskReview != null)
                    {
                        createdTaskReviewsVM.Add(MapRecordTaskListReview_TaskReviewToVM(taskReview,person));
                    }
                }
                return createdTaskReviewsVM;
            }
        }

        public async Task<string> GetDefaultTimeZone()
        {
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            return generalSettings.DefaultTimeZone;
        }
    }

}
