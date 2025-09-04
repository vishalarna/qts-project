using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Task_Reference;
using ITask_ReferenceDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_ReferenceService;

namespace QTD2.Application.Services.Shared
{
    public class Task_ReferenceService : Interfaces.Services.Shared.ITask_ReferenceService
    {
        private readonly IStringLocalizer<Task_ReferenceService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITask_ReferenceDomainService _taskReferenceService;

        public Task_ReferenceService(IStringLocalizer<Task_ReferenceService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ITask_ReferenceDomainService taskReferenceService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _taskReferenceService = taskReferenceService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var taskRef = await _taskReferenceService.GetAsync(id);
            if (taskRef == null)
            {
                throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                taskRef.Activate();
                await _taskReferenceService.UpdateAsync(taskRef);
            }
        }

        public async Task<Task_Reference> CreateAsync(Task_ReferenceCreateOptions options)
        {
            var taskRef = new Task_Reference(options.DisplayName, options.Description);
            var taskRefResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskRef, Task_ReferenceOperations.Create);
            if (taskRefResult.Succeeded)
            {
                taskRef.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                taskRef.CreatedDate = DateTime.Now;
                var validationResult = await _taskReferenceService.AddAsync(taskRef);
                if (validationResult.IsValid)
                {
                    return taskRef;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var taskRef = await _taskReferenceService.GetAsync(id);
            if (taskRef == null)
            {
                throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                taskRef.Delete();
                await _taskReferenceService.UpdateAsync(taskRef);
            }
        }

        public async Task<List<Task_Reference>> GetAllAsync()
        {
            var taskRef = (await _taskReferenceService.AllAsync()).ToList();
            return taskRef;
        }

        public async Task<Task_Reference> GetAsync(int id)
        {
            var taskRef = await _taskReferenceService.GetAsync(id);
            return taskRef;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var taskRef = await _taskReferenceService.GetAsync(id);
            if (taskRef == null)
            {
                throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                taskRef.Deactivate();
                await _taskReferenceService.UpdateAsync(taskRef);
            }
        }

        public async Task<Task_Reference> UpdateAsync(int id, Task_ReferenceCreateOptions options)
        {
            var taskRef = await _taskReferenceService.GetAsync(id);
            if (taskRef == null)
            {
                throw new QTDServerException(_localizer["TaskReferenceNotFound"]);
            }
            else
            {
                var taskResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, taskRef, Task_ReferenceOperations.Update);
                if (taskResult.Succeeded)
                {
                    // TODO change update logic as required
                    taskRef.DisplayName = options.DisplayName;
                    var validationResult = await _taskReferenceService.UpdateAsync(taskRef);
                    if (validationResult.IsValid)
                    {
                        return taskRef;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }
    }
}
