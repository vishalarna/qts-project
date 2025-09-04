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
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.EnablingObjectiveHistory;
using QTD2.Infrastructure.Model.Task;
using IEnablingObjectiveHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveHistoryService;
using IEODomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;

namespace QTD2.Application.Services.Shared
{
    public class EnablingObjectiveHistoryService : IEnablingObjectiveHistoryService
    {
        private readonly IEnablingObjectiveHistoryDomainService _eoHistoryService;
        private readonly IStringLocalizer<EnablingObjectiveHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEnablingObjectiveService _eo_appService;
        private readonly IEODomainService _eoService;
        private readonly IPersonDomainService _person_Serivce;

        public EnablingObjectiveHistoryService(IEnablingObjectiveHistoryDomainService eoHistoryService, IStringLocalizer<EnablingObjectiveHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, IEnablingObjectiveService eo_appService, IEODomainService eoService, IPersonDomainService person_Serivce)
        {
            _eoHistoryService = eoHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _eo_appService = eo_appService;
            _eoService = eoService;
            _person_Serivce = person_Serivce;
        }

        public async System.Threading.Tasks.Task ActiveEOHistory(int id)
        {
            var hist = await _eoHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _eoHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjectiveHistory> CreateEOHistory(EnablingObjectiveHistoryCreateOptions options)
        {
            var hist = new EnablingObjectiveHistory(options.EnablingObjectiveId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjectiveHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.ChangeEffectiveDate = options.ChangeEffectiveDate;
                hist.ChangeNotes = options.ChangeNotes;
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                hist.OldStatus = false;
                hist.NewStatus = true;
                hist.EnablingObjectiveId = options.EnablingObjectiveId;
                hist.Version_EnablingObjectiveId = options.Version_EnablingObjectiveId;
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Task_HistoryOperations.Create);
                if (result.Succeeded)
                {
                    var validationResult = await _eoHistoryService.AddAsync(hist);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    return hist;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return hist;
        }

        public async System.Threading.Tasks.Task DeleteEOHistory(int id)
        {
            var hist = await _eoHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _eoHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<EnablingObjectiveHistory>> GetAllEOHistories()
        {
            var hist = (await _eoHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<List<EOLatestActivityVM>> GetLatestActivity(bool getTrimmed)
        {
            var eos = await _eoService.AllQuery().ToListAsync();
            List<EnablingObjectiveHistory> history = new List<EnablingObjectiveHistory>();
            history = await _eoHistoryService.AllQuery().ToListAsync();

            var users = await _userManager.Users.ToListAsync();
            //var persons = await _person_Serivce.AllQuery().ToListAsync();
            //var eoNumbers = new Dictionary<int, string>();

            //foreach (var item in eos)
            //{
            //    var number = (await _eo_appService.GetAsync(item.Id)).Number;
            //    eoNumbers.Add(item.Id, number);
            //}

            List<EOLatestActivityVM> latestactivity = new List<EOLatestActivityVM>();

            //var latestactivity = from t in eos
            //                     join h in history on t.Id equals h.EnablingObjectiveId
            //                     join u in users on h.CreatedBy equals u.Email
            //                   //  join p in persons on u.UserName equals p.Username
            //                     select new EOLatestActivityVM
            //                     {
            //                         Id = t.Id,
            //                         ActivityDesc = h.ChangeNotes,
            //                         Title = eoNumbers[t.Id] + " " + t.Description,
            //                         CreatedBy = u.UserName,
            //                         CreatedDate = h.CreatedDate,
            //                     };

            foreach (var hist in history)
            {
                var eo = eos.Where(w => w.Id == hist.EnablingObjectiveId).FirstOrDefault();
                if (eo != null)
                {
                    var user = users.Where(w => w.Email == hist.CreatedBy).FirstOrDefault();
                    if (user != null)
                    {
                        var activity = new EOLatestActivityVM
                        {
                            Id = eo.Id,
                            ActivityDesc = hist.ChangeNotes,
                            Title = eo.Number + " " + eo.Description,
                            CreatedBy = user.Email,
                            CreatedDate = hist.CreatedDate,
                        };
                        latestactivity.Add(activity);
                    }
                }
            }
            if (getTrimmed)
            {
                latestactivity = latestactivity.Take(5).ToList();
            }
            return latestactivity.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public async Task<EnablingObjectiveHistory> GetEOHistory(int id)
        {
            var hist = await _eoHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveEOHistory(int id)
        {
            var hist = await _eoHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _eoHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjectiveHistory> UpdateEOHistory(int id, EnablingObjectiveHistoryCreateOptions options)
        {
            var hist = await _eoHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjectiveHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _eoHistoryService.UpdateAsync(hist);
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

        public async Task<List<EOLatestActivityVM>> GetLatestActivity(int id)
        {
            var eos = await _eoService.FindQuery(x => x.Id == id).ToListAsync();
            var history = await _eoHistoryService.FindQuery(x => x.EnablingObjectiveId == id).ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            //var persons = await _person_Serivce.AllQuery().ToListAsync();
            //var eoNumbers = new Dictionary<int, string>();

            //foreach (var item in eos)
            //{
            //    var number = (await _eo_appService.GetAsync(item.Id)).Number;
            //    eoNumbers.Add(item.Id, number);
            //}

            List<EOLatestActivityVM> latestactivity = new List<EOLatestActivityVM>();

            //var latestactivity = from t in eos
            //                     join h in history on t.Id equals h.EnablingObjectiveId
            //                     join u in users on h.CreatedBy equals u.Email
            //                     //join p in persons on u.UserName equals p.Username
            //                     select new EOLatestActivityVM
            //                     {
            //                         Id = t.Id,
            //                         ActivityDesc = h.ChangeNotes,
            //                         Title = eoNumbers[t.Id] + " " + t.Description,
            //                         CreatedBy = u.UserName,
            //                         CreatedDate = h.CreatedDate,
            //                     };

            foreach (var hist in history)
            {
                var eo = eos.Where(w => w.Id == hist.EnablingObjectiveId).FirstOrDefault();
                if (eo != null)
                {
                    var user = users.Where(w => w.Email == hist.CreatedBy).FirstOrDefault();
                    if (user != null)
                    {
                        var activity = new EOLatestActivityVM
                        {
                            Id = eo.Id,
                            ActivityDesc = hist.ChangeNotes,
                            Title = eo.Number + " " + eo.Description,
                            CreatedBy = user.Email,
                            CreatedDate = hist.CreatedDate,
                        };
                        latestactivity.Add(activity);
                    }
                }
            }

            return latestactivity.OrderByDescending(x => x.CreatedDate).ToList();

        }
    }
}
