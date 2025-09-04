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
using QTD2.Infrastructure.Model.SafetyHazard_Category;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;
using ISafetyHazard_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_CategoryHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class SafetyHazard_CategoryHistoryService : Interfaces.Services.Shared.ISafetyHazard_CategoryHistoryService
    {
        private readonly ISafetyHazard_CategoryHistoryDomainService _shCatHistoryService;
        private readonly IStringLocalizer<SafetyHazard_CategoryHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public SafetyHazard_CategoryHistoryService(ISafetyHazard_CategoryHistoryDomainService shCatHistoryService, IStringLocalizer<SafetyHazard_CategoryHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _shCatHistoryService = shCatHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveSHCatHistory(int id)
        {
            var hist = await _shCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _shCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<SafetyHazard_CategoryHistory> CreateSHCatHistory(SafetyHazard_CategoryHistoryCreateOptions options)
        {
            var hist = new SafetyHazard_CategoryHistory(options.SafetyHazardCategoryId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, SafetyHazard_CategoryHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _shCatHistoryService.AddAsync(hist);
                if (validationResult.IsValid)
                {
                    return hist;
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

        public async System.Threading.Tasks.Task DeleteSHCatHistory(int id)
        {
            var hist = await _shCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _shCatHistoryService.UpdateAsync(hist);
            }
        }

        public async System.Threading.Tasks.Task CreateDeleteHistAsync(SaftyHazardCategoryOptions options)
        {
            foreach (var id in options.SaftyHazardCategoryIds)
            {
                var hist = new SafetyHazard_CategoryHistory(id, true, false, options.EffectiveDate, options.ChangeNotes);
                hist.CreatedDate = DateTime.Now;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var histValidation = await _shCatHistoryService.AddAsync(hist);
                if (!histValidation.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
                }
            }
        }

        public async Task<List<SafetyHazard_CategoryHistory>> GetAllSHCatHistories()
        {
            var hist = (await _shCatHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<SafetyHazard_CategoryHistory> GetSHCatHistory(int id)
        {
            var hist = await _shCatHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveSHCatHistory(int id)
        {
            var hist = await _shCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _shCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<SafetyHazard_CategoryHistory> UpdateSHCatHistory(int id, SafetyHazard_CategoryHistoryCreateOptions options)
        {
            var hist = await _shCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, SafetyHazard_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _shCatHistoryService.UpdateAsync(hist);
                    if (validationResult.IsValid)
                    {
                        return hist;
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
}
