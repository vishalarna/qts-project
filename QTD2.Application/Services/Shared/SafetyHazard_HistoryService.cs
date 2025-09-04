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
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.SafetyHazard_History;
using QTD2.Infrastructure.Model.SaftyHazard;
using ISafetyHazard_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_HistoryService;
using ISafetyHazardDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazardService;

namespace QTD2.Application.Services.Shared
{
    public class SafetyHazard_HistoryService : Interfaces.Services.Shared.ISafetyHazard_HistoryService
    {
        private readonly ISafetyHazard_HistoryDomainService _shHistoryService;
        private readonly IStringLocalizer<SafetyHazard_HistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISafetyHazardDomainService _shService;

        public SafetyHazard_HistoryService(ISafetyHazard_HistoryDomainService shHistoryService, IStringLocalizer<SafetyHazard_HistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ISafetyHazardDomainService shService)
        {
            _shHistoryService = shHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _shService = shService;
        }

        public async System.Threading.Tasks.Task ActiveSHHistory(int id)
        {
            var hist = await _shHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _shHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<SafetyHazard_History> CreateSHHistory(SaftyHazardOptions options)
        {
            var hist = new SafetyHazard_History();
            foreach (var id in options.SaftyHazardIds)
            {
                hist = new SafetyHazard_History();
                hist.SafetyHazardId = id;
                hist.ChangeNotes = options.ChangeNotes;
                hist.ChangeEffectiveDate = options.EffectiveDate;
                hist.OldStatus = true;
                hist.NewStatus = false;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var histValidation = await _shHistoryService.AddAsync(hist);
                if (!histValidation.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
                }
            }

            return hist;
        }

        public async System.Threading.Tasks.Task DeleteSHHistory(int id)
        {
            var hist = await _shHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _shHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<SaftyHazardLatestActivityVM>> GetAllSHHistories(bool getLatest)
        {
            var saftyHazards = await _shService.AllQuery().IgnoreQueryFilters().Select(s => new SaftyHazard
            {
                Id=s.Id,
                Title = s.Title,
                Number = s.Number
            }).ToListAsync();
            //var history = await _shHistoryService.AllQuery().ToListAsync();
            //var users = await _userManager.Users.ToListAsync();
            List<SaftyHazardLatestActivityVM> latestActivities = new List<SaftyHazardLatestActivityVM>();
            foreach(var saftyHazard in saftyHazards)
            {
                var latestActivity = new SaftyHazardLatestActivityVM();
                var hists = await _shHistoryService.FindQuery(x => x.SafetyHazardId == saftyHazard.Id).ToListAsync();
                foreach(var hist in hists)
                {
                    var user = await _userManager.Users.Where(w => w.UserName == hist.CreatedBy).FirstOrDefaultAsync();
                    if(user != null)
                    {
                        latestActivity.ActivityDesc = hist.ChangeNotes;
                        latestActivity.Title = saftyHazard.Number + " - " + saftyHazard.Title;
                        latestActivity.CreatedBy = user.Email;
                        latestActivity.CreatedDate = hist.CreatedDate;
                        latestActivities.Add(latestActivity);
                    }
                }
            }

            //var latestactivity = from sh in saftyHazards
            //                     join h in history on sh.Id equals h.SafetyHazardId
            //                     join u in _userManager.Users on h.CreatedBy equals u.Email
            //                     select new SaftyHazardLatestActivityVM
            //                     {
            //                         Id = sh.Id,
            //                         ActivityDesc = h.ChangeNotes,
            //                         Title = sh.Number + " - " + sh.Title,
            //                         CreatedBy = u.Email,
            //                         CreatedDate = h.CreatedDate,
            //                     };


            if (getLatest)
            {
                latestActivities = latestActivities.Take(5).ToList();
            }

            return latestActivities.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public async Task<SafetyHazard_History> GetSHHistory(int id)
        {
            var hist = await _shHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveSHHistory(int id)
        {
            var hist = await _shHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardStatusHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _shHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<SafetyHazard_History> UpdateSHHistory(int id, SafetyHazard_HistoryCreateOptions options)
        {
            var hist = await _shHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, SafetyHazard_HistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _shHistoryService.UpdateAsync(hist);
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
