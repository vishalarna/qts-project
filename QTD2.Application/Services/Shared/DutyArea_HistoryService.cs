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
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.DutyArea_History;

namespace QTD2.Application.Services.Shared
{
    public class DutyArea_HistoryService : Interfaces.Services.Shared.IDutyArea_HistoryService
    {
        private readonly IDutyArea_HistoryService _dutyArea_HistoryService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<DutyArea_History> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public DutyArea_HistoryService(IDutyArea_HistoryService dutyAreaHistoryService, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IStringLocalizer<DutyArea_History> localizer, UserManager<AppUser> userManager)
        {
            _dutyArea_HistoryService = dutyAreaHistoryService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<DutyArea_History> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _dutyArea_HistoryService.UpdateAsync(obj);
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

        public async Task<DutyArea_History> CreateAsync(DutyArea_HistoryCreateOptions options)
        {

            var obj = new DutyArea_History(options.DutyAreaId, options.ChangeEffectiveDate, options.ChangeNotes);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _dutyArea_HistoryService.AddAsync(obj);
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

        public async Task<DutyArea_History> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _dutyArea_HistoryService.UpdateAsync(obj);
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

        public async Task<List<DutyArea_History>> GetAsync()
        {
            var obj = (await _dutyArea_HistoryService.AllAsync()).Where(r => !r.Deleted); ;
            obj = obj.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, DutyArea_HistoryOperations.Read).Result.Succeeded);
            return obj.ToList();
        }

        public async Task<DutyArea_History> GetAsync(int id)
        {
            var obj = await _dutyArea_HistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Read);
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
                throw new QTDServerException( _localizer["DutyAreaHistoryNotFound"]);
            }
        }

        public async Task<DutyArea_History> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _dutyArea_HistoryService.UpdateAsync(obj);
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

        public async Task<DutyArea_History> UpdateAsync(int id, DutyArea_HistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, DutyArea_HistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.ChangeNotes = options.ChangeNotes;
                obj.ChangeEffectiveDate = options.ChangeEffectiveDate;
                var validationResult = await _dutyArea_HistoryService.UpdateAsync(obj);
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
