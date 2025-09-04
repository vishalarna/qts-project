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
using QTD2.Infrastructure.Model.StudentEvaluationHistory;
using IStudentEvaluationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IStudentEvaluationHistoryService;
namespace QTD2.Application.Services.Shared
{
    public class StudentEvaluationHistoryService : IStudentEvaluationHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<StudentEvaluationHistory> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly StudentEvaluationHistory _studentEvaluationHistory;
        private readonly IStudentEvaluationHistoryDomainService _studentEvaluationHistoryService;

        public StudentEvaluationHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<StudentEvaluationHistory> localizer, UserManager<AppUser> userManager, IStudentEvaluationHistoryDomainService studentEvaluationHistoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _studentEvaluationHistory = new StudentEvaluationHistory();
            _studentEvaluationHistoryService = studentEvaluationHistoryService;
        }

        public async Task<StudentEvaluationHistory> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _studentEvaluationHistoryService.UpdateAsync(obj);
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

        public async Task<StudentEvaluationHistory> CreateAsync(StudentEvaluationHistoryCreateOptions options)
        {
            var obj = new StudentEvaluationHistory(options.StudentEvaluationId,options.StudentEvaluationNotes,options.EffectiveDate);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _studentEvaluationHistoryService.AddAsync(obj);
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

        public async Task<StudentEvaluationHistory> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _studentEvaluationHistoryService.UpdateAsync(obj);
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

        public async Task<StudentEvaluationHistory> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _studentEvaluationHistoryService.UpdateAsync(obj);
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

        public List<StudentEvaluationHistory> GetAsync()
        {
            var obj = _studentEvaluationHistoryService.AllQuery().Where(r => !r.Deleted);
            var data = obj.ToList().Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, StudentEvaluationHistoryOperations.Read).Result.Succeeded);
            return data.ToList();
        }

        public async Task<StudentEvaluationHistory> GetAsync(int id)
        {
            var obj = await _studentEvaluationHistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Read);
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

        public async Task<StudentEvaluationHistory> UpdateAsync(int id, StudentEvaluationHistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, StudentEvaluationHistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.StudentEvaluationNotes = options.StudentEvaluationNotes;
                obj.EffectiveDate = options.EffectiveDate;
                
                var validationResult = await _studentEvaluationHistoryService.UpdateAsync(obj);
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
