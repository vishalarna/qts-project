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
using QTD2.Infrastructure.Model.ILAHistory;
using IILAHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IILAHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class ILAHistoryService : IILAHistoryService
    {
        private readonly IILAHistoryDomainService _iLAHistoryService;
        private readonly IStringLocalizer<ILAHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public ILAHistoryService(IILAHistoryDomainService iLAHistoryService, IStringLocalizer<ILAHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _iLAHistoryService = iLAHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveILAHistory(int id)
        {
            var hist = await _iLAHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["ILAHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _iLAHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<ILAHistory> CreateILAHistory(ILAHistoryCreateOptions options)
        {
            var hist = new ILAHistory(options.ILAId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, ILAHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _iLAHistoryService.AddAsync(hist);
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

        public async System.Threading.Tasks.Task DeleteILAHistory(int id)
        {
            var hist = await _iLAHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _iLAHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<ILAHistory>> GetAllILAHistories()
        {
            var hist = (await _iLAHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<ILAHistory> GetILAHistory(int id)
        {
            var hist = await _iLAHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveILAHistory(int id)
        {
            var hist = await _iLAHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["ILAHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _iLAHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<ILAHistory> UpdateILAHistory(int id, ILAHistoryCreateOptions options)
        {
            var hist = await _iLAHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["ILAHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, ILAHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _iLAHistoryService.UpdateAsync(hist);
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
