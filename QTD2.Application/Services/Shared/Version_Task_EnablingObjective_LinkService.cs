using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Version_Task_EnablingObjective_Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVersion_Task_EnablingObjective_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_EnablingObjective_LinkService;

namespace QTD2.Application.Services.Shared
{
    public class Version_Task_EnablingObjective_LinkService : IVersion_Task_EnablingObjective_LinkService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_Task_EnablingObjective_LinkService> _localizer;
        private readonly IVersion_Task_EnablingObjective_LinkDomainService _versionTaskEOLinkService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITaskService _taskService;

        public Version_Task_EnablingObjective_LinkService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Version_Task_EnablingObjective_LinkService> localizer, IVersion_Task_EnablingObjective_LinkDomainService versionTaskEOLinkService, UserManager<AppUser> userManager, ITaskService taskService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _versionTaskEOLinkService = versionTaskEOLinkService;
            _userManager = userManager;
            _taskService = taskService;
        }

        public async Task<Version_Task_EnablingObjective_Link> CreateAsync(Version_Task_EnablingObjective_LinkCreateOptions options)
        {
            var obj = (await _versionTaskEOLinkService.FindAsync(x => x.Version_EnablingObjectiveId == options.Version_EnablingObjectiveId && x.Version_TaskId == options.Version_TaskId)).FirstOrDefault();
            string versionNumber = "";
            if (obj == null)
            {
                    versionNumber = "1.0";
                    obj = new Version_Task_EnablingObjective_Link(options.Version_EnablingObjectiveId, options.Version_TaskId, versionNumber);            
            }
            else
            {
                Double verNumber = Convert.ToDouble(obj.VersionNumber);
                    verNumber += 1;
                    versionNumber = verNumber.ToString() + ".0";
                obj = new Version_Task_EnablingObjective_Link(options.Version_EnablingObjectiveId, options.Version_TaskId, versionNumber);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _versionTaskEOLinkService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _versionTaskEOLinkService.UpdateAsync(obj);
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

        public async Task<List<Version_Task_EnablingObjective_Link>> GetAsync()
        {
            var obj_list = await _versionTaskEOLinkService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Version_Task_EnablingObjective_Link> GetAsync(int id)
        {
            var obj = await _versionTaskEOLinkService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<Version_Task_EnablingObjective_Link> UpdateAsync(int id, Version_Task_EnablingObjective_LinkUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Update);

            if (result.Succeeded)
            {
                obj.Version_EnablingObjectiveId = options.Version_EnablingObjectiveId;
                obj.VersionNumber = options.VersionNumber;
                obj.Version_TaskId = options.Version_TaskId;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationresult = await _versionTaskEOLinkService.UpdateAsync(obj);
                if (!validationresult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationresult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
    }
}
