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
using QTD2.Infrastructure.Model.Position;
using QTD2.Infrastructure.Model.PositionHistory;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using IPositionHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class PositionHistoryService : Interfaces.Services.Shared.IPositionHistoryService
    {
        private readonly IPositionHistoryDomainService _positionHistoryService;
        private readonly IPositionDomainService _positionService;
        private readonly IStringLocalizer<PositionHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public PositionHistoryService(IPositionHistoryDomainService positionHistoryService, IStringLocalizer<PositionHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, IPositionDomainService positionService)
        {
            _positionHistoryService = positionHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _positionService = positionService;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _positionHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["PositionHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _positionHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Position_History> CreateAsync(Position_HistoryCreateOptions options)
        {
            var hist = new Position_History(options.PositionId, options.ChangeNotes, options.EffectiveDate);

            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, PositionHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _positionHistoryService.AddAsync(hist);
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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var hist = await _positionHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["PositionHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _positionHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<Position_History>> GetAllPositionHistories()
        {
            var hist = (await _positionHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<List<PositionLatestActivityVM>> GetHistoryAsync(bool getLatest)
        {
            var positions = await _positionService.GetAllActiveCompactPositions();
            var history = await _positionHistoryService.AllQuery().Select(s => new Position_History
            {
                Id = s.Id,
                ChangeNotes = s.ChangeNotes,
                CreatedDate = s.CreatedDate,
                PositionId = s.PositionId,
                CreatedBy = s.CreatedBy,
            }).ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from i in positions
                                 join h in history on i.Id equals h.PositionId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email
                                 select new PositionLatestActivityVM
                                 {
                                     Id = h.Id,
                                     PositionName = i.PositionTitle,
                                     PositionNum = i.PositionNumber,
                                     ActivityDesc = h.ChangeNotes,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate,
                                 };

            if (getLatest)
            {
                latestactivity = latestactivity.OrderByDescending(a=>a.CreatedDate).Take(5).ToList();
            }

            return latestactivity.OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public async Task<Position_History> GetPositionHistory(int id)
        {
            var hist = await _positionHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _positionHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["PositionHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _positionHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<Position_History> UpdateAsync(int id, Position_HistoryCreateOptions options)
        {
            var hist = await _positionHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["PoaitionHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, PositionHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    hist.ChangeEffectiveDate = options.EffectiveDate;
                    var validationResult = await _positionHistoryService.UpdateAsync(hist);
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
