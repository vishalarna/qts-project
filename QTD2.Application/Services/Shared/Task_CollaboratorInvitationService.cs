using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Task_CollaboratorInvitation;
using ITask_CollaboratorInvitationDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_CollaboratorInvitationService;
using ITask_DomainService = QTD2.Domain.Interfaces.Service.Core.ITaskService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITask_CollaboratorLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITask_Collaborator_LinkService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class Task_CollaboratorInvitationService : Interfaces.Services.Shared.ITask_CollaboratorInvitationService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Task_CollaboratorInvitationService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITask_CollaboratorInvitationDomainService _taskCIService;
        private readonly ITask_DomainService _taskService;
        private readonly IPersonDomainService _personService;
        private readonly ITask_CollaboratorLinkDomainService _task_col_linkService;
        private readonly Task_Collaborator_Link _task_col_link;
        private readonly Domain.Entities.Core.Task _task;

        public Task_CollaboratorInvitationService(
            IAuthorizationService authorizationService,
            IStringLocalizer<Task_CollaboratorInvitationService> localizer,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            ITask_CollaboratorInvitationDomainService taskCIService,
            ITask_DomainService taskService,
            IPersonDomainService personService,
            ITask_CollaboratorLinkDomainService task_col_linkService)
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _taskCIService = taskCIService;
            _taskService = taskService;
            _personService = personService;
            _task_col_linkService = task_col_linkService;
            _task_col_link = new Task_Collaborator_Link();
            _task = new Domain.Entities.Core.Task();
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var tCI = await _taskCIService.GetAsync(id);
            if (tCI != null)
            {
                var tCIResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tCI, Task_CollaboratorInvitationOperations.Delete);
                if (tCIResult.Succeeded)
                {
                    tCI.Activate();
                    await _taskCIService.UpdateAsync(tCI);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["TaskCollaboratorInvitationNotFound"]);
            }
        }

        public async Task<Task_CollaboratorInvitation> CreateAsync(Task_CollaboratorInvitationCreateOptions options)
        {
            var task_colab = new Task_CollaboratorInvitation();
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == options.InvitedForTaskId, new string[] { nameof(_task.Task_Collaborator_Links) }).FirstOrDefaultAsync();
            var isCreator = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id == task.CreatedBy;
            var invitedById = await _personService.FindQuery(x => x.Username == _httpContextAccessor.HttpContext.User.Identity.Name).Select(x => x.Id).FirstOrDefaultAsync();

            await UnlinkAndDeleteTaskColabs(options.InvitedForTaskId);
            
            for(var i = 0; i < options.InviteeEIds.Length; i++)
            {
                var tci = await _taskCIService.FindQuery(x => x.InviteeEmail == options.InviteeEmails[i] && x.InvitedForTaskId == options.InvitedForTaskId).FirstOrDefaultAsync();
                if(tci == null)
                {
                    task_colab = new Task_CollaboratorInvitation();
                    task_colab.InviteDate = options.InviteDate;
                    task_colab.InvitedByEId = invitedById;
                    task_colab.InvitedForTaskId = options.InvitedForTaskId;
                    task_colab.Message = options.Message;
                    task_colab.InviteeEId = options.InviteeEIds[i];
                    task_colab.InviteeEmail = options.InviteeEmails[i];
                    await _taskCIService.AddAsync(task_colab);
                    task.LinkTaskCollab(task_colab,isCreator);
                    await _taskService.UpdateAsync(task);
                }
            }
            return task_colab;
        }

        public async System.Threading.Tasks.Task UnlinkAndDeleteTaskColabs(int taskId)
        {
            var task = await _taskService.FindQueryWithIncludeAsync(x => x.Id == taskId, new string[] { nameof(_task.Task_Collaborator_Links) }).FirstOrDefaultAsync();
            if(task.Task_Collaborator_Links.Count > 0)
            {
                task.UnlinkTaskCollab();
                await _taskService.UpdateAsync(task);
            }
            var colabs = await _taskCIService.FindQuery(x => x.InvitedForTaskId == taskId).ToListAsync();
            foreach(var colab in colabs)
            {
                await _taskCIService.DeleteAsync(colab);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var tCI = await _taskCIService.GetAsync(id);
            if (tCI != null)
            {
                var tCIResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tCI, Task_CollaboratorInvitationOperations.Delete);
                if (tCIResult.Succeeded)
                {
                    tCI.Delete();
                    await _taskCIService.UpdateAsync(tCI);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["TaskCollaboratorInvitationNotFound"]);
            }
        }

        public async Task<List<Task_CollaboratorInvitation>> GetAllAsync()
        {
            var tCI = await _taskCIService.AllAsync();
            return tCI.ToList();
        }

        public async Task<List<Task_CollaboratorInvitation>> GetCollaboratorsForTask(int taskId)
        {
            var colabs = await _taskCIService.FindQuery(x => x.InvitedForTaskId == taskId).ToListAsync();
            return colabs;
        }

        public async Task<Task_CollaboratorInvitation> GetAsync(int id)
        {
            var tCI = await _taskCIService.GetAsync(id);
            return tCI;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var tCI = await _taskCIService.GetAsync(id);
            if (tCI != null)
            {
                var tCIResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tCI, Task_CollaboratorInvitationOperations.Delete);
                if (tCIResult.Succeeded)
                {
                    tCI.Deactivate();
                    await _taskCIService.UpdateAsync(tCI);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["TaskCollaboratorInvitationNotFound"]);
            }
        }

        public async Task<Task_CollaboratorInvitation> UpdateAsync(int id, Task_CollaboratorInvitationCreateOptions options)
        {
            var tCI = (await _taskCIService.FindAsync(x => x.InvitedForTaskId == options.InvitedForTaskId && x.InvitedByEId == options.InvitedByEId)).FirstOrDefault();
            if (tCI == null)
            {
                throw new QTDServerException(_localizer["RecordAlreadyExists"]);
            }
            else
            {
                var tCIResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, tCI, Task_CollaboratorInvitationOperations.Update);
                if (tCIResult.Succeeded)
                {
                    // TODO change according to need;
                    await _taskCIService.UpdateAsync(tCI);
                    return tCI;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }
    }
}
