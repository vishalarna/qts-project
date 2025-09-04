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
using QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory;
using IEnablingObjective_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_CategoryHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class EnablingObjective_CategoryHistoryService : IEnablingObjective_CategoryHistoryService
    {
        private readonly IEnablingObjective_CategoryHistoryDomainService _eoCatHistoryService;
        private readonly IStringLocalizer<EnablingObjective_CategoryHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public EnablingObjective_CategoryHistoryService(IEnablingObjective_CategoryHistoryDomainService eoCatHistoryService, IStringLocalizer<EnablingObjective_CategoryHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _eoCatHistoryService = eoCatHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveEOCatHistory(int id)
        {
            var hist = await _eoCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _eoCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_CategoryHistory> CreateEOCatHistory(EnablingObjective_CategoryHistoryCreateOptions options)
        {
            var hist = new EnablingObjective_CategoryHistory(options.EnablingObjectiveCategoryId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_CategoryHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _eoCatHistoryService.AddAsync(hist);
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

        public async System.Threading.Tasks.Task DeleteEOCatHistory(int id)
        {
            var hist = await _eoCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _eoCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<EnablingObjective_CategoryHistory>> GetAllEOCatHistories()
        {
            var hist = (await _eoCatHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<EnablingObjective_CategoryHistory> GetEOCatHistory(int id)
        {
            var hist = await _eoCatHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveEOCatHistory(int id)
        {
            var hist = await _eoCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _eoCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_CategoryHistory> UpdateEOCatHistory(int id, EnablingObjective_CategoryHistoryCreateOptions options)
        {
            var hist = await _eoCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _eoCatHistoryService.UpdateAsync(hist);
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
