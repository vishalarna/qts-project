using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Version_Task_SaftyHazard_Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVersion_Task_SafetyHazard_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_Task_SaftyHazard_LinkService;


namespace QTD2.Application.Services.Shared
{
    public class Version_Task_SaftyHazard_LinkService : IVersion_Task_SaftyHazard_LinkService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_Task_EnablingObjective_LinkService> _localizer;
        private readonly IVersion_Task_SafetyHazard_LinkDomainService _versionTaskSHLinkService;
        private readonly UserManager<AppUser> _userManager;

        public Version_Task_SaftyHazard_LinkService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Version_Task_EnablingObjective_LinkService> localizer, IVersion_Task_SafetyHazard_LinkDomainService versionTaskSHLinkService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _versionTaskSHLinkService = versionTaskSHLinkService;
            _userManager = userManager;
        }

        public async Task<Version_Task_SaftyHazard_Link> CreateAsync(Version_Task_SaftyHazard_LinkCreateOptions options)
        {
            var obj = (await _versionTaskSHLinkService.FindAsync(x => x.Version_SaftyHazardId == options.Version_SaftyHazardId && x.Version_TaskId == options.Version_TaskId)).FirstOrDefault();
            string versionNumber = "";
            if (obj == null)
            {
                versionNumber = "1.0";
                obj = new Version_Task_SaftyHazard_Link(options.Version_TaskId, options.Version_SaftyHazardId, versionNumber);
            }
            else
            {
                Double verNumber = Convert.ToDouble(obj.VersionNumber);
                verNumber += 1;
                versionNumber = verNumber.ToString() + ".0";
                obj = new Version_Task_SaftyHazard_Link(options.Version_TaskId, options.Version_SaftyHazardId, versionNumber);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _versionTaskSHLinkService.AddAsync(obj);
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

                var validationResult = await _versionTaskSHLinkService.UpdateAsync(obj);
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

        public async Task<List<Version_Task_SaftyHazard_Link>> GetAsync()
        {
            var obj_list = await _versionTaskSHLinkService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Version_Task_SaftyHazard_Link> GetAsync(int id)
        {
            var obj = await _versionTaskSHLinkService.GetAsync(id);
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

        public async Task<Version_Task_SaftyHazard_Link> UpdateAsync(int id, Version_Task_SaftyHazard_LinkUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Version_TaskOperations.Update);

            if (result.Succeeded)
            {
                obj.Version_SaftyHazardId = options.Version_SaftyHazardId;
                obj.VersionNumber = options.VersionNumber;
                obj.Version_TaskId = options.Version_TaskId;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationresult = await _versionTaskSHLinkService.UpdateAsync(obj);
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
