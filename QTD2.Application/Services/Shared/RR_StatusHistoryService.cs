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
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.RR_StatusHistory;
using IRR_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IRR_StatusHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class RR_StatusHistoryService : Interfaces.Services.Shared.IRR_StatusHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<RR_StatusHistory> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly RR_StatusHistory _rrStatusHistory;
        private readonly IRR_StatusHistoryDomainService _rrStatusHistoryService;

        public RR_StatusHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<RR_StatusHistory> localizer, UserManager<AppUser> userManager, IRR_StatusHistoryDomainService rrStatusHistoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _rrStatusHistory = new RR_StatusHistory();
            _rrStatusHistoryService = rrStatusHistoryService;
        }

        public async Task<RR_StatusHistory> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RR_StatusHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _rrStatusHistoryService.UpdateAsync(obj);
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

        public async Task<RR_StatusHistory> CreateAsync(RR_StatusHistoryCreateOptions options)
        {
            var obj = new RR_StatusHistory(options.RegulatoryRequirementId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _rrStatusHistoryService.AddAsync(obj);
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

        public async Task<RR_StatusHistory> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RR_StatusHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _rrStatusHistoryService.UpdateAsync(obj);
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

        public async Task<RR_StatusHistory> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RR_StatusHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _rrStatusHistoryService.UpdateAsync(obj);
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

        public List<RR_StatusHistory> GetAsync()
        {
            var obj = _rrStatusHistoryService.AllQuery().Where(r => !r.Deleted);
            var data = obj.ToList().Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, RR_StatusHistoryOperations.Read).Result.Succeeded);
            return data.ToList();
        }

        public async Task<RR_StatusHistory> GetAsync(int id)
        {
            var obj = await _rrStatusHistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RR_StatusHistoryOperations.Read);
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

        public async Task<RR_StatusHistory> UpdateAsync(int id, RR_StatusHistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, RR_StatusHistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.ChangeNotes = options.ChangeNotes;
                obj.ChangeEffectiveDate = options.ChangeEffectiveDate;
                obj.NewStatus = options.NewStatus;
                obj.OldStatus = options.OldStatus;
                var validationResult = await _rrStatusHistoryService.UpdateAsync(obj);
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
