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
using QTD2.Infrastructure.Model.EnablingObjective_SubCategoryHistory;
using IEnablingObjective_SubCategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class EnablingObjective_SubCategoryHistoryService : IEnablingObjective_SubCategoryHistoryService
    {
        private readonly IEnablingObjective_SubCategoryHistoryDomainService _eoSubCatHistoryService;
        private readonly IStringLocalizer<EnablingObjective_SubCategoryHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public EnablingObjective_SubCategoryHistoryService(IEnablingObjective_SubCategoryHistoryDomainService eoSubCatHistoryService, IStringLocalizer<EnablingObjective_SubCategoryHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _eoSubCatHistoryService = eoSubCatHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveEOSubCatHistory(int id)
        {
            var hist = await _eoSubCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveSubCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _eoSubCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_SubCategoryHistory> CreateEOSubCatHistory(EnablingObjective_SubCategoryHistoryCreateOptions options)
        {
            var hist = new EnablingObjective_SubCategoryHistory(options.EnablingObjectiveSubCategoryId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_SubCategoryHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _eoSubCatHistoryService.AddAsync(hist);
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

        public async System.Threading.Tasks.Task DeleteEOSubCatHistory(int id)
        {
            var hist = await _eoSubCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _eoSubCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<EnablingObjective_SubCategoryHistory>> GetAllEOSubCatHistories()
        {
            var hist = (await _eoSubCatHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<EnablingObjective_SubCategoryHistory> GetEOSubCatHistory(int id)
        {
            var hist = await _eoSubCatHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveEOSubCatHistory(int id)
        {
            var hist = await _eoSubCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveSubCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _eoSubCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_SubCategoryHistory> UpdateEOSubCatHistory(int id, EnablingObjective_SubCategoryHistoryCreateOptions options)
        {
            var hist = await _eoSubCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_SubCategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _eoSubCatHistoryService.UpdateAsync(hist);
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
