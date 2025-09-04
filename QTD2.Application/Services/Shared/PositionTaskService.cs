using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.PositionTask;
using QTD2.Infrastructure.Model.Task_History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class PositionTaskService : IPositionTaskService
    {
        private readonly Domain.Interfaces.Service.Core.IPosition_TaskService _positionTaskService;
        private readonly Application.Interfaces.Services.Shared.IPositionHistoryService _positionHistoryService;
        private readonly Application.Interfaces.Services.Shared.ITaskService _taskService;
        private readonly Application.Interfaces.Services.Shared.ITask_HistoryService _taskHistoryService;
        private readonly Application.Interfaces.Services.Shared.IVersion_TaskService _versionTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<PositionTaskService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public PositionTaskService(
            Domain.Interfaces.Service.Core.IPosition_TaskService positionTaskService,
            Application.Interfaces.Services.Shared.IPositionHistoryService positionHistoryService,
            Application.Interfaces.Services.Shared.ITaskService taskService,
            Application.Interfaces.Services.Shared.ITask_HistoryService taskHistoryService,
            Application.Interfaces.Services.Shared.IVersion_TaskService versionTaskService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<PositionTaskService> localizer,
            UserManager<AppUser> userManager
        ) {
            _positionTaskService = positionTaskService;
            _positionHistoryService = positionHistoryService;
            _taskService = taskService;
            _taskHistoryService = taskHistoryService;
            _versionTaskService = versionTaskService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task UpdateR5Tasks(int positionTaskId, LinkR5UpdateTasksModel linkR5UpdateTasksModel)
        {
            var positionTask = (await _positionTaskService.GetWithIncludeAsync(positionTaskId, new string[] { "R5ImpactedTasks" }));

            if (positionTask == null)
            {
                throw new BadHttpRequestException(message: _localizer["PositionTaskNotFound"]);
            }

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            if (linkR5UpdateTasksModel.UnlinkAll)
            {
                var result = positionTask.RemoveAllR5ImpactedTasks();

                foreach (int removedTaskId in result.Where(i => i != positionTask.TaskId))
                {
                    await CreateTaskVersionAndHistory(
                        removedTaskId,
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.ChangeNotes,
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.EffectiveDate);
                }
            }
            else
            {
                var result = positionTask.SetR5ImpactedTasks(linkR5UpdateTasksModel.Link_TaskIds);

                foreach (int removedTaskId in result.removedTaskIds.Where(i => i != positionTask.TaskId))
                {
                    await CreateTaskVersionAndHistory(
                        removedTaskId, 
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.ChangeNotes, 
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.EffectiveDate);
                }

                foreach (int addedTaskId in result.addedTaskIds.Where(i => i != positionTask.TaskId))
                {
                   await CreateTaskVersionAndHistory(
                        addedTaskId, 
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.ChangeNotes, 
                        linkR5UpdateTasksModel.Position_HistoryCreateOptions.EffectiveDate);
                }
            }

            positionTask.Modify(userName);

            var validationResult = await _positionTaskService.UpdateAsync(positionTask);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            //Removed the Position_Task's Task from all R5 Tasks Version and History calls in case it was linked to itself as an R5 Task.
            //That way we only add one Version and History record for the Task here.
            //i.e. the ".Where(i => i != positionTask.TaskId)" calls in each foreach loop for the removed and added R5ImpactedTaskIds
            await CreateTaskVersionAndHistory(
                positionTask.TaskId,
                linkR5UpdateTasksModel.Position_HistoryCreateOptions.ChangeNotes,
                linkR5UpdateTasksModel.Position_HistoryCreateOptions.EffectiveDate);

            await _positionHistoryService.CreateAsync(linkR5UpdateTasksModel.Position_HistoryCreateOptions);
        }

        public async System.Threading.Tasks.Task MarkPositionTasksAsR6(UpdateMarkAsR6Model updateMarkAsR6Model)
        {
            var positionTasks = (await _positionTaskService.FindAsync(r => updateMarkAsR6Model.PositionTaskIds.Contains(r.Id))).ToList();
            
            if (positionTasks == null)
            {
                throw new BadHttpRequestException(message: _localizer["PositionTasksNotFound"]);
            }

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            
            foreach (var positionTask in positionTasks)
            {
                positionTask.SetIsR6Impacted(
                    true, 
                    updateMarkAsR6Model.Position_HistoryCreateOptions.ChangeNotes, 
                    updateMarkAsR6Model.Position_HistoryCreateOptions.EffectiveDate);

                positionTask.Modify(userName);

                var validationResult = await _positionTaskService.UpdateAsync(positionTask);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }

                await CreateTaskVersionAndHistory(
                    positionTask.TaskId,
                    updateMarkAsR6Model.Position_HistoryCreateOptions.ChangeNotes,
                    updateMarkAsR6Model.Position_HistoryCreateOptions.EffectiveDate);

                await _positionHistoryService.CreateAsync(updateMarkAsR6Model.Position_HistoryCreateOptions);
            }
        }

        public async System.Threading.Tasks.Task UnmarkPositionTaskAsR6(int positionTaskId, UpdateUnmarkAsR6Model updateUnmarkAsR6Model)
        {
            var positionTask = (await _positionTaskService.GetAsync(positionTaskId));

            if (positionTask == null)
            {
                throw new BadHttpRequestException(message: _localizer["PositionTaskNotFound"]);
            }

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            positionTask.SetIsR6Impacted(false, null, null);
            positionTask.Modify(userName);

            var validationResult = await _positionTaskService.UpdateAsync(positionTask);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            await CreateTaskVersionAndHistory(
                positionTask.TaskId,
                updateUnmarkAsR6Model.Position_HistoryCreateOptions.ChangeNotes,
                updateUnmarkAsR6Model.Position_HistoryCreateOptions.EffectiveDate);

            await _positionHistoryService.CreateAsync(updateUnmarkAsR6Model.Position_HistoryCreateOptions);
        }

		public async System.Threading.Tasks.Task DeleteR5Task(int positionTaskId, int r5ImpactedTaskId, DeleteR5TaskModel deleteR5TaskModel)
		{
            var positionTask = (await _positionTaskService.GetWithIncludeAsync(positionTaskId, new string[] { "R5ImpactedTasks" }));

            if (positionTask == null)
            {
                throw new BadHttpRequestException(message: _localizer["PositionTaskNotFound"]);
            }

            var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;

            var removedTaskId = positionTask.DeleteR5Task(r5ImpactedTaskId);
            positionTask.Modify(userName);

            var validationResult = await _positionTaskService.UpdateAsync(positionTask);

            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

            //Conditionally create Task Version and History for the removed R5 Task if it isn't the same as the Position_Task's Task
            //That way we only add one Version and History record for a Task which is listed under itself as an R5 Impacted Task.
            if (removedTaskId != positionTask.TaskId)
            {
                await CreateTaskVersionAndHistory(
                    removedTaskId,
                    deleteR5TaskModel.Position_HistoryCreateOptions.ChangeNotes,
                    deleteR5TaskModel.Position_HistoryCreateOptions.EffectiveDate);
            }

            await CreateTaskVersionAndHistory(
                positionTask.TaskId,
                deleteR5TaskModel.Position_HistoryCreateOptions.ChangeNotes,
                deleteR5TaskModel.Position_HistoryCreateOptions.EffectiveDate);

            await _positionHistoryService.CreateAsync(deleteR5TaskModel.Position_HistoryCreateOptions);
        }

        private async System.Threading.Tasks.Task CreateTaskVersionAndHistory(int taskId, string changeNotes, DateTime effectiveDate)
        {
            var task = await _taskService.GetAsync(taskId);
            var histOptions = new Task_HistoryOptions();
            histOptions.ChangeNotes = changeNotes;
            histOptions.EffectiveDate = effectiveDate;
            histOptions.TaskIds = new int[] { task.Id };
            var version = await _versionTaskService.VersionAndCreateCopy(task, 2);
            histOptions.Version_TaskId = version.Id;
            await _taskHistoryService.SaveHistoryAsync(histOptions);
        }
	}
}