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
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.IssuingAuthorityStatusHistory;

namespace QTD2.Application.Services.Shared
{
    public class Proc_IssuingAuthority_HistoryService : Interfaces.Services.Shared.IProc_IssuingAuthority_HistoryService
    {
        private readonly IProc_IssuingAuthority_HistoryService _issuAuthStatusHistoryService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Proc_IssuingAuthority_History> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public Proc_IssuingAuthority_HistoryService(IProc_IssuingAuthority_HistoryService issuAuthStatusHistoryService, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IStringLocalizer<Proc_IssuingAuthority_History> localizer, UserManager<AppUser> userManager)
        {
            _issuAuthStatusHistoryService = issuAuthStatusHistoryService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<Proc_IssuingAuthority_History> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _issuAuthStatusHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<Proc_IssuingAuthority_History> CreateAsync(Proc_IssuingAuthority_HistoryCreateOptions options)
        {
            var obj = new Proc_IssuingAuthority_History(options.IssuingAuthorityId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate,options.ChangeNotes);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _issuAuthStatusHistoryService.AddAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<Proc_IssuingAuthority_History> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _issuAuthStatusHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<List<Proc_IssuingAuthority_History>> GetAsync()
        {
            var obj = (await _issuAuthStatusHistoryService.AllAsync()).Where(r => !r.Deleted); ;
            obj = obj.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, Proc_IssuingAuthority_HistoryOperations.Read).Result.Succeeded);
            return obj.ToList();
        }

        public async Task<Proc_IssuingAuthority_History> GetAsync(int id)
        {
            var obj = await _issuAuthStatusHistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["IssuingAuthorityStatusHistoryNotFound"]);
            }
        }

        public async Task<Proc_IssuingAuthority_History> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _issuAuthStatusHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<Proc_IssuingAuthority_History> UpdateAsync(int id, Proc_IssuingAuthority_HistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.ChangeNotes = options.ChangeNotes;
                obj.ChangeEffectiveDate = options.ChangeEffectiveDate;
                obj.NewStatus = options.NewStatus;
                obj.OldStatus = options.OldStatus;
                var validationResult = await _issuAuthStatusHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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
