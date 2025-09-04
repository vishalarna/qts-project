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
using QTD2.Infrastructure.Model.Location_CategoryHistory;
using ILocation_CategoryHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_CategoryHistoryService;


namespace QTD2.Application.Services.Shared
{
    public class Location_CategoryHistoryService : Interfaces.Services.Shared.ILocation_CategoryHistoryService
    {
        private readonly ILocation_CategoryHistoryDomainService _lCatHistoryService;
        private readonly IStringLocalizer<Location_CategoryHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;



        public Location_CategoryHistoryService(ILocation_CategoryHistoryDomainService lCatHistoryService, IStringLocalizer<Location_CategoryHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _lCatHistoryService = lCatHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _lCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _lCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Location_CategoryHistory> CreateAsync(Location_CategoryHistoryCreateOptions options)
        {
            var hist = new Location_CategoryHistory(options.LocCategoryId, options.EffectiveDate, options.CategoryNotes);

            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            hist.Notes = options.CategoryNotes;
            var validationResult = await _lCatHistoryService.AddAsync(hist);
            if (validationResult.IsValid)
            {
                return hist;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

        }


        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var hist = await _lCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _lCatHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<Location_CategoryHistory>> GetAllLocCatHistories()
        {
            var hist = (await _lCatHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<Location_CategoryHistory> GetLocCatHistory(int id)
        {
            var hist = await _lCatHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _lCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationCategoryHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _lCatHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<Location_CategoryHistory> UpdateAsync(int id, Location_CategoryHistoryCreateOptions options)
        {
            var hist = await _lCatHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationCategoryHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Location_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    hist.LocCategoryID = options.LocCategoryId;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _lCatHistoryService.UpdateAsync(hist);
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
