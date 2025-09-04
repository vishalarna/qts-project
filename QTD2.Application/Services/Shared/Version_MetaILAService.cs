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
using QTD2.Infrastructure.Model.Version_MetaILA;
using IVersion_MetaILADomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_MetaILAService;
using IMetaILAService = QTD2.Domain.Interfaces.Service.Core.IMetaILAService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class Version_MetaILAService : IVersion_MetaILAService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_MetaILAService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMetaILAService _metaILAService;
        private readonly IVersion_MetaILADomainService _versionMetaILAService;

        public Version_MetaILAService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<Version_MetaILAService> localizer, UserManager<AppUser> userManager, IVersion_MetaILADomainService versionMetaILAService, IMetaILAService metaILAService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _versionMetaILAService = versionMetaILAService;
            _metaILAService = metaILAService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _versionMetaILAService.UpdateAsync(obj);
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

        public async Task<Version_MetaILA> CreateAsync(Version_MetaILACreateOptions options)
        {
            var obj = (await _versionMetaILAService.FindAsync(x => x.MetaILAId == options.MetaILAId && x.MetaILAName == options.MetaILAName)).FirstOrDefault();
            var metaILA = await _metaILAService.GetAsync(options.MetaILAId);
            string versionNumber = "";
            if (obj == null)
            {
                if (metaILA != null)
                {
                    versionNumber = "1.0";
                    obj = new Version_MetaILA(options.MetaILAId,options.MetaILAName,options.MetaILADesc, options.MetaILAStatusId??1, versionNumber,options.MetaILAAssessmentID, options.Reason);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["MetaILANotFound"].Value);
                }
            } 
            else
            {
                if (metaILA != null)
                {
                    Double verNumber = Convert.ToDouble(obj.VersionNumber);
                    verNumber += 1;
                    versionNumber = verNumber.ToString() + ".0";
                    obj = new Version_MetaILA(options.MetaILAId, options.MetaILAName, options.MetaILADesc, options.MetaILAStatusId??1, versionNumber, options.MetaILAAssessmentID, options.Reason);
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["MetaILANotFound"].Value);
                }
            }
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _versionMetaILAService.AddAsync(obj);
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
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _versionMetaILAService.UpdateAsync(obj);
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

        public async Task<List<Version_MetaILA>> GetAsync()
        {
            var obj_list = await _versionMetaILAService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Version_MetaILA> GetAsync(int id)
        {
            var obj = await _versionMetaILAService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);
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

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _versionMetaILAService.UpdateAsync(obj);
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

        public async Task<Version_MetaILA> UpdateAsync(int id, Version_MetaILAUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.MetaILAId = options.MetaILAId;
                options.MetaILAName = options.MetaILAName;
                obj.MetaILADesc = options.MetaILADesc;
                obj.MetaILAStatusId = options.MetaILAStatusId;
                obj.VersionNumber = options.VersionNumber;
                obj.Reason = options.Reason;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _versionMetaILAService.UpdateAsync(obj);
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
    }
}
