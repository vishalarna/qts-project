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
using Microsoft.Extensions.Options;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.CertificationIssuingAuthority;
using ICert_IssuingAuthorityDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationIssuingAuthorityService;

namespace QTD2.Application.Services.Shared
{
    public class CertificationIssuingAuthorityService : ICertificationIssuingAuthorityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<CertificationIssuingAuthorityService> _localizer;
        private readonly ICert_IssuingAuthorityDomainService _cert_IssuingAuthorityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly CertificationIssuingAuthority _certIssuingAuthority;

        public CertificationIssuingAuthorityService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<CertificationIssuingAuthorityService> localizer, ICert_IssuingAuthorityDomainService cert_IssuingAuthorityService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _cert_IssuingAuthorityService = cert_IssuingAuthorityService;
            _userManager = userManager;
            _certIssuingAuthority = new CertificationIssuingAuthority();
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _cert_IssuingAuthorityService.UpdateAsync(obj);
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

        public async Task<CertificationIssuingAuthority> CreateAsync(CertificationIssuingAuthorityCreateOptions options)
        {
            var obj = (await _cert_IssuingAuthorityService.FindAsync(x => x.Title == options.Title)).FirstOrDefault();
            if (obj == null)
            {
                obj = new CertificationIssuingAuthority(options.Title, options.Description, options.Website, options.EffectiveDate, options.Notes);
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
                var validationResult = await _cert_IssuingAuthorityService.AddAsync(obj);
 
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

                var validationResult = await _cert_IssuingAuthorityService.UpdateAsync(obj);
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

        public async Task<List<CertificationIssuingAuthority>> GetAsync()
        {
            var obj_list = await _cert_IssuingAuthorityService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<CertificationIssuingAuthority> GetAsync(int id)
        {
            var obj = await _cert_IssuingAuthorityService.GetAsync(id);
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
                throw new QTDServerException( _localizer["RecordNotFound"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _cert_IssuingAuthorityService.UpdateAsync(obj);
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

        public async Task<CertificationIssuingAuthority> UpdateAsync(int id, CertificationIssuingAuthorityCreateOptions options)
        {
            var obj = await _cert_IssuingAuthorityService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var existing = await _cert_IssuingAuthorityService.FindQuery(x => x.Title.Trim().ToLower() == options.Title.Trim().ToLower() && x.Id != id).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new QTDServerException( _localizer["CertificationIssuingAuthorityAlreadyExists"]);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                // Todo Update logic
                obj.Title = options.Title;
                obj.Description = options.Description;
                obj.EffectiveDate = options.EffectiveDate;
                obj.Notes = options.Notes;
                obj.Website = options.Website;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                var validationResult = await _cert_IssuingAuthorityService.UpdateAsync(obj);
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
