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
using QTD2.Infrastructure.Model.QuestionBankHistory;
using IQuestionBankHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IQuestionBankHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class QuestionBankHistoryService : IQuestionBankHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<QuestionBankHistory> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IQuestionBankHistoryDomainService _questionBankHistoryService;

        public QuestionBankHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<QuestionBankHistory> localizer, UserManager<AppUser> userManager, IQuestionBankHistoryDomainService questionBankHistoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _questionBankHistoryService = questionBankHistoryService;
        }

        public async Task<QuestionBankHistory> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _questionBankHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<QuestionBankHistory> CreateAsync(QuestionBankHistoryCreateOptions options)
        {
            var obj = new QuestionBankHistory(options.QuestionBankId,options.QuestionBankNotes,options.EffectiveDate);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _questionBankHistoryService.AddAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<QuestionBankHistory> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _questionBankHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public async Task<QuestionBankHistory> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _questionBankHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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

        public List<QuestionBankHistory> GetAsync()
        {
            var obj = _questionBankHistoryService.AllQuery().Where(r => !r.Deleted);
            var data = obj.ToList().Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, QuestionBankHistoryOperations.Read).Result.Succeeded);
            return data.ToList();
        }

        public async Task<QuestionBankHistory> GetAsync(int id)
        {
            var obj = await _questionBankHistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["StudentEvaluationHistoryNotFound"]);
            }
        }

        public async Task<QuestionBankHistory> UpdateAsync(int id, QuestionBankHistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, QuestionBankHistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.QuestionBankNotes = options.QuestionBankNotes;
                obj.EffectiveDate = options.EffectiveDate;

                var validationResult = await _questionBankHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
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