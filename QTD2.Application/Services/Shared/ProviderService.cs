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
using QTD2.Infrastructure.Model.Provider;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IProviderDomainService = QTD2.Domain.Interfaces.Service.Core.IProviderService;
using IILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using QTD2.Infrastructure.Model.ILA;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using System.Linq.Expressions;
using QTD2.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace QTD2.Application.Services.Shared
{
    public class ProviderService : IProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ProviderService> _locaizer;
        private readonly IProviderDomainService _providerService;
        private readonly IILADomainService _ilaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Provider _provider;
        private readonly IILATraineeEvaluationDomainService _ilaTraineeEvalService;

        public ProviderService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ProviderService> locaizer, IProviderDomainService providerService, UserManager<AppUser> userManager, IILADomainService ilaService, IILATraineeEvaluationDomainService ilaTraineeEvalService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _locaizer = locaizer;
            _providerService = providerService;
            _userManager = userManager;
            _provider = new Provider();
            _ilaService = ilaService;
            _ilaTraineeEvalService = ilaTraineeEvalService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var provider = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Delete);

            if (result.Succeeded)
            {
                provider.Activate();

                var validationResult = await _providerService.UpdateAsync(provider);
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

        public async Task<Provider> CreateAsync(ProviderCreateOptions options)
        {
            var provider = (await _providerService.FindAsync(x => x.Name == options.Name)).FirstOrDefault();
            if (provider == null)
            {
                provider = new Provider(options.IsNERC, options.IsPriority ?? false, options.RepSignature, options.RepEmail, options.RepPhone, options.RepTitle, options.RepName, options.CompanyWebsite, options.ContactEmail, options.ContactMobile, options.ContactExt, options.ContactPhone, options.ContactTitle, options.ContactName, options.ProviderLevelId, options.Number, options.Name);
            }
            else
            {
                throw new BadHttpRequestException(message: _locaizer["ProviderExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Create);
            if (result.Succeeded)
            {
                provider.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                provider.CreatedDate = DateTime.Now;
                var validationResult = await _providerService.AddAsync(provider);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return provider;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var provider = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Delete);

            if (result.Succeeded)
            {
                provider.Delete();

                var validationResult = await _providerService.UpdateAsync(provider);
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

        public async Task<List<Provider>> GetAsync()
        {
            var providers = await _providerService.AllAsync();
            providers = providers.Where(provider => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read).Result.Succeeded).ToList();
            return providers?.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<Provider>> GetProviderWithoutIncludes()
        {
            var providers = await _providerService.GetCompactedProvider();
            providers = providers.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, ProviderOperations.Read).Result.Succeeded).ToList();
            return providers.Where(x => x.Active == true).OrderByDescending(x => x.IsPriority).ThenBy(y => y.Name).ToList();
        }

        public async Task<List<Provider>> GetActiveProvidersAsync()
        {
            var providers = (await _providerService.GetCompactedProvider()).Where(x => x.Active == true);
            providers = providers.Where(provider => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read).Result.Succeeded);
            return providers.OrderBy(c => c.Name).OrderByDescending(o => o.IsPriority).ToList();
        }

        public async Task<List<Provider>> GetWithILAAsync()
        {
            var providers = await _providerService.AllQuery().ToListAsync();
            providers = providers.Where(provider => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read).Result.Succeeded).ToList();
            var linked = new List<Provider>();
            foreach (var provider in providers)
            {
                provider.ILAs = await _ilaService.FindQuery(x => x.ProviderId == provider.Id && x.IsPublished == true).ToListAsync();
                if (provider.ILAs.Count > 0)
                {
                    linked.Add(provider);
                }
            }

            return linked;
        }

        public async Task<List<ILA_ProviderVM>> GetWithILACountAsync()
        {
            var providers = await _providerService.AllAsync();
            providers = providers.Where(provider => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read).Result.Succeeded);
            var ilaProviderVM = new List<ILA_ProviderVM>();
            foreach (var provider in providers)
            {
                ilaProviderVM.Add(new ILA_ProviderVM
                {
                    Id = provider.Id,
                    Active = provider.Active,
                    IsPriority = provider.IsPriority,
                    Name = provider.Name,
                    providerNumber = provider.Number,
                    isNerc = provider.IsNERC,
                    ILACount = await _ilaService.GetCount(x => x.ProviderId == provider.Id),
                });
            }

            return ilaProviderVM?.ToList();
        }

        public async Task<List<ILA_ProviderVM>> GetProviderWithFilterAndILACount(FilterByOptions filterOptions)
        {
            var providers = await _providerService.GetFilteredProvidersAsync(filterOptions.Filter, filterOptions.ActiveStatus, filterOptions.ProviderIds);
            var ilas = await _ilaService.AllAsync();

            var ilaProviderVM = new List<ILA_ProviderVM>();
            foreach (var provider in providers)
            {
                ilaProviderVM.Add(new ILA_ProviderVM
                {
                    Id = provider.Id,
                    Active = provider.Active,
                    IsPriority = provider.IsPriority,
                    Name = provider.Name,
                    providerNumber = provider.Number,
                    isNerc = provider.IsNERC,
                    ILACount = provider.ILAs.Where(r => (r.Name ?? "").ToUpper().Contains(filterOptions.Filter.ToUpper()) || (r.Number ?? "").ToUpper().Contains(filterOptions.Filter.ToUpper()))
                                            .Where(r => r.Active == filterOptions.ActiveILAStatus).Count()
                });
            }

            return ilaProviderVM?.ToList();
        }

        public async Task<Provider> GetAsync(int id)
        {
            var provider = await _providerService.GetWithIncludeAsync(id, new string[] { nameof(_provider.ILAs) });
            if (provider != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read);
                if (result.Succeeded)
                {
                    return provider;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ProviderNotFound"].Value);
            }
        }

        public async Task<Provider> GetOnlyProviderAsync(int id)
        {
            var provider = await _providerService.GetAsync(id);
            if (provider != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Read);
                if (result.Succeeded)
                {
                    return provider;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_locaizer["ProviderNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var provider = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Delete);

            if (result.Succeeded)
            {
                provider.Deactivate();

                var validationResult = await _providerService.UpdateAsync(provider);
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

        public async Task<Provider> UpdateAsync(int id, ProviderUpdateOptions options)
        {
            var provider = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, provider, ProviderOperations.Update);

            var providerExists = (await _providerService.FindAsync(x => x.Id != id && x.Name == options.Name)).FirstOrDefault() != null;

            if (providerExists)
            {
                throw new UnauthorizedAccessException(message: _locaizer["ProviderAlreadyExistsWithSameName"].Value);
            }

            if (result.Succeeded)
            {
                // Todo update logic
                provider.Name = options.Name;
                provider.Number = options.Number;
                provider.ProviderLevelId = options.ProviderLevelId;
                provider.ContactName = options.ContactName;
                provider.ContactTitle = options.ContactTitle;
                provider.ContactPhone = options.ContactPhone;
                provider.ContactMobile = options.ContactMobile;
                provider.ContactExt = options.ContactExt;
                provider.ContactEmail = options.ContactEmail;
                provider.CompanyWebsite = options.CompanyWebsite;
                provider.RepName = options.RepName;
                provider.RepPhone = options.RepPhone;
                provider.RepEmail = options.RepEmail;
                provider.RepSignature = options.RepSignature;
                provider.IsPriority = options.IsPriority;
                provider.IsNERC = options.IsNERC;
                provider.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                provider.ModifiedDate = DateTime.Now;

                var validationResult = await _providerService.UpdateAsync(provider);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return provider;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _locaizer["OperationNotAllowed"].Value);
            }
        }
    }
}
