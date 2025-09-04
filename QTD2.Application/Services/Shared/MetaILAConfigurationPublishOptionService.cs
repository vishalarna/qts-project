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
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.MetaILAConfigurationPublishOption;
using IMetaILAConfigurationPublishOptionDomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILAConfigurationPublishOptionService;

namespace QTD2.Application.Services.Shared
{
    public class MetaILAConfigurationPublishOptionService : IMetaILAConfigurationPublishOptionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<MetaILAConfigurationPublishOption> _localizer;
        private readonly IMetaILAConfigurationPublishOptionDomainService _metaILAConfigurationPublishOptionService;
        private readonly UserManager<AppUser> _userManager;

        public MetaILAConfigurationPublishOptionService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<MetaILAConfigurationPublishOption> localizer, IMetaILAConfigurationPublishOptionDomainService metaILAConfigurationPublishOptionService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _metaILAConfigurationPublishOptionService = metaILAConfigurationPublishOptionService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _metaILAConfigurationPublishOptionService.UpdateAsync(obj);
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

        public async Task<MetaILAConfigurationPublishOption> CreateAsync(MetaILAConfigurationPublishOptionCreateOptions options)
        {
            var obj = (await _metaILAConfigurationPublishOptionService.FindAsync(x => x.Description == options.Description)).FirstOrDefault();
            if (obj == null)
            {
                obj = new MetaILAConfigurationPublishOption(options.Description);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _metaILAConfigurationPublishOptionService.AddAsync(obj);
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
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _metaILAConfigurationPublishOptionService.UpdateAsync(obj);
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

        public async Task<List<MetaILAConfigurationPublishOption>> GetAsync()
        {
            var obj_list = await _metaILAConfigurationPublishOptionService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<MetaILAConfigurationPublishOption> GetAsync(int id)
        {
            var obj = await _metaILAConfigurationPublishOptionService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Read);
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
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _metaILAConfigurationPublishOptionService.UpdateAsync(obj);
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

        public async Task<MetaILAConfigurationPublishOption> UpdateAsync(int id, MetaILAConfigurationPublishOptionUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, MetaILAConfigurationPublishOptionOperations.Update);

            if (result.Succeeded)
            {
                obj.Description = options.Description;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _metaILAConfigurationPublishOptionService.UpdateAsync(obj);
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
