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
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.CustomEnablingObjective;
using ICustomEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.ICustomEnablingObjectiveService;

namespace QTD2.Application.Services.Shared
{
    public class CustomEnablingObjectiveService : ICustomEnablingObjectiveService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<CustomEnablingObjectiveService> _localizer;
        private readonly ICustomEnablingObjectiveDomainService _customEnablingObjectiveService;
        private readonly UserManager<AppUser> _userManager;

        public CustomEnablingObjectiveService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<CustomEnablingObjectiveService> localizer, ICustomEnablingObjectiveDomainService customEnablingObjectiveService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _customEnablingObjectiveService = customEnablingObjectiveService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _customEnablingObjectiveService.UpdateAsync(obj);
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

        public async Task<CustomEnablingObjective> CreateAsync(CustomEnablingObjectiveCreateOptions options)
        {
            var obj = (await _customEnablingObjectiveService.FindAsync(x => x.Description == options.Description && x.EO_TopicId == options.EO_TopicId && x.EO_SubCatId == options.EO_SubCatId && x.EO_CatId == options.EO_SubCatId)).FirstOrDefault();
            if (obj == null)
            {
                var num = (await _customEnablingObjectiveService.AllAsync()).OrderByDescending(x => x.CustomEONumber).FirstOrDefault();
                obj = new CustomEnablingObjective(options.EO_TopicId, options.Description, options.IsAddtoEO,options.EO_CatId,options.EO_SubCatId);
                if(num != null)
                {
                    var nextCustomEONumber = num.CustomEONumber + 1;
                    obj.CustomEONumber = nextCustomEONumber;
                }
                else
                {
                    obj.CustomEONumber = 1;
                }
                
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _customEnablingObjectiveService.AddAsync(obj);
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

                var validationResult = await _customEnablingObjectiveService.UpdateAsync(obj);
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

        public async Task<List<CustomEnablingObjective>> GetAsync()
        {
            var obj_list = await _customEnablingObjectiveService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<CustomEnablingObjective> GetAsync(int id)
        {
            var obj = await _customEnablingObjectiveService.GetAsync(id);
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

                var validationResult = await _customEnablingObjectiveService.UpdateAsync(obj);
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

        public async Task<CustomEnablingObjective> UpdateAsync(int id, CustomEnablingObjectiveUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.Description = options.Description;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                obj.EO_TopicId = options.EO_TopicId;
                obj.EO_CatId = options.Eo_CatId;
                obj.EO_SubCatId = options.Eo_SubCatId;
                var validationResult = await _customEnablingObjectiveService.UpdateAsync(obj);
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
