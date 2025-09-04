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
using QTD2.Infrastructure.Model.ProviderLevel;
using IProviderLevelDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderLevelService;

namespace QTD2.Application.Services.Shared
{
    public class ProviderLevelService : IProviderLevelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ProviderLevelService> _locaizer;
        private readonly IProviderLevelDomainService _providerLevelService;
        private readonly UserManager<AppUser> _userManager;

        public ProviderLevelService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ProviderLevelService> locaizer, IProviderLevelDomainService providerLevelService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _locaizer = locaizer;
            _providerLevelService = providerLevelService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var providerLevel = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Delete);

            if (result.Succeeded)
            {
                providerLevel.Activate();

                var validationResult = await _providerLevelService.UpdateAsync(providerLevel);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ProviderLevel> CreateAsync(ProviderLevelCreateOptions options)
        {
            var providerLevel = (await _providerLevelService.FindAsync(x => x.Name == options.Name)).FirstOrDefault();
            if (providerLevel == null)
            {
                providerLevel = new ProviderLevel(options.Name);
            }
            else
            {
                throw new BadHttpRequestException(message: _locaizer["ProviderLevelExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Create);
            if (result.Succeeded)
            {
                providerLevel.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                providerLevel.CreatedDate = DateTime.Now;
                var validationResult = await _providerLevelService.AddAsync(providerLevel);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return providerLevel;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var providerLevel = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Delete);

            if (result.Succeeded)
            {
                providerLevel.Delete();

                var validationResult = await _providerLevelService.UpdateAsync(providerLevel);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<List<ProviderLevel>> GetAsync()
        {
            var providerLevels = await _providerLevelService.AllAsync();
            providerLevels = providerLevels.Where(providerLevel => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Read).Result.Succeeded);
            return providerLevels?.ToList();
        }

        public async Task<ProviderLevel> GetAsync(int id)
        {
            var providerLevel = await _providerLevelService.GetAsync(id);
            if (providerLevel != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Read);
                if (result.Succeeded)
                {
                    return providerLevel;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ProviderLevelNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var providerLevel = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Delete);

            if (result.Succeeded)
            {
                providerLevel.Deactivate();

                var validationResult = await _providerLevelService.UpdateAsync(providerLevel);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<ProviderLevel> UpdateAsync(int id, ProviderLevelUpdateOptions options)
        {
            var providerLevel = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, providerLevel, ProviderLevelOperations.Update);

            if (result.Succeeded)
            {
                providerLevel.Name = options.Name;
                providerLevel.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                providerLevel.ModifiedDate = DateTime.Now;

                var validationResult = await _providerLevelService.UpdateAsync(providerLevel);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return providerLevel;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }
    }
}
