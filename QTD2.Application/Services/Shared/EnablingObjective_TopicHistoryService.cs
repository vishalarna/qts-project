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
using QTD2.Infrastructure.Model.EnablingObjective_TopicHistory;
using IEnablingObjective_TopicHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class EnablingObjective_TopicHistoryService : IEnablingObjective_TopicHistoryService
    {
        private readonly IEnablingObjective_TopicHistoryDomainService _eoTopicHistoryService;
        private readonly IStringLocalizer<EnablingObjective_TopicHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;

        public EnablingObjective_TopicHistoryService(IEnablingObjective_TopicHistoryDomainService eoTopicHistoryService, IStringLocalizer<EnablingObjective_TopicHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _eoTopicHistoryService = eoTopicHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveEOTopicHistory(int id)
        {
            var hist = await _eoTopicHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveTopicHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _eoTopicHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_TopicHistory> CreateEOTopicHistory(EnablingObjective_TopicHistoryCreateOptions options)
        {
            var hist = new EnablingObjective_TopicHistory(options.EnablingObjectiveTopicId, options.OldStatus, options.NewStatus, options.ChangeEffectiveDate, options.ChangeNotes);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_TopicHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _eoTopicHistoryService.AddAsync(hist);
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

        public async System.Threading.Tasks.Task DeleteEOTopicHistory(int id)
        {
            var hist = await _eoTopicHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _eoTopicHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<EnablingObjective_TopicHistory>> GetAllEOTopicHistories()
        {
            var hist = (await _eoTopicHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<EnablingObjective_TopicHistory> GetEOTopicHistory(int id)
        {
            var hist = await _eoTopicHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveEOTopicHistory(int id)
        {
            var hist = await _eoTopicHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveTopicHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _eoTopicHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<EnablingObjective_TopicHistory> UpdateEOTopicHistory(int id, EnablingObjective_TopicHistoryCreateOptions options)
        {
            var hist = await _eoTopicHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["EnablingObjectiveHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EnablingObjective_TopicHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    // TODO change update logic as required
                    hist.ChangeNotes = options.ChangeNotes;
                    var validationResult = await _eoTopicHistoryService.UpdateAsync(hist);
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
