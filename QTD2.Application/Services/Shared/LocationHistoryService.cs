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
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;
using ILocationDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_Service;
using ILocationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ILocation_HistoryService;
using QTD2.Domain.Exceptions;


namespace QTD2.Application.Services.Shared
{
    public class LocationHistoryService : Interfaces.Services.Shared.ILocationHistoryService
    {

        private readonly ILocationHistoryDomainService _locHistoryService;
        private readonly ILocationDomainService _locService;
        private readonly IStringLocalizer<LocationHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public LocationHistoryService(ILocationHistoryDomainService locHistoryService, IStringLocalizer<LocationHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ILocationDomainService locService)
        {
            _locHistoryService = locHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _locService = locService;
        }


        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _locHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _locHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<Location_History> CreateAsync(Location_HistoryCreateOptions options)
        {
            var hist = new Location_History(options.LocationId, options.EffectiveDate, options.Notes);

            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            var validationResult = await _locHistoryService.AddAsync(hist);
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
            var hist = await _locHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _locHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<Location_History>> GetAllLocCatHistories()
        {
            var hist = (await _locHistoryService.AllAsync()).ToList();
            return hist;
        }


        public async Task<List<LocationLatestActivityVM>> GetHistoryAsync()
        {
            var instructors = await _locService.AllQuery().IgnoreQueryFilters().ToListAsync();
            var history = await _locHistoryService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from i in instructors
                                 join h in history on i.Id equals h.LocationId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email
                                 select new LocationLatestActivityVM
                                 {
                                     LocId = h.Id,
                                     LocName = i.LocName,
                                     LocNumber = i.LocNumber,
                                     ActivityDesc = h.Notes,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate,
                                 };

            return latestactivity.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public async Task<Location_History> GetLocCatHistory(int id)
        {
            var hist = await _locHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _locHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _locHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Location_History> UpdateAsync(int id, Location_HistoryCreateOptions options)
        {
            var hist = await _locHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["LocationHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Location_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.Notes = options.Notes;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _locHistoryService.UpdateAsync(hist);
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
