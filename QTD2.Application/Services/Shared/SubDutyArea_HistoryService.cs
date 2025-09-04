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
using QTD2.Infrastructure.Model.SubDutyArea_History;

namespace QTD2.Application.Services.Shared
{
    public class SubDutyArea_HistoryService : Interfaces.Services.Shared.ISubDutyArea_HistoryService
    {
        private readonly ISubDutyArea_HistoryService _subDutyArea_HistoryService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SubDutyArea_History> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public SubDutyArea_HistoryService(ISubDutyArea_HistoryService subDutyAreaHistoryService, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IStringLocalizer<SubDutyArea_History> localizer, UserManager<AppUser> userManager)
        {
            _subDutyArea_HistoryService = subDutyAreaHistoryService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<SubDutyArea_History> CreateAsync(SubDutyArea_HistoryCreateOptions options)
        {

            var obj = new SubDutyArea_History(options.SubDutyAreaId, options.ChangeEffectiveDate, options.ChangeNotes);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubDutyArea_HistoryOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _subDutyArea_HistoryService.AddAsync(obj);
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

        public async Task<List<SubDutyArea_History>> GetAsync()
        {
            var obj = (await _subDutyArea_HistoryService.AllAsync()).Where(r => !r.Deleted); 
            obj = obj.Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, SubDutyArea_HistoryOperations.Read).Result.Succeeded);
            return obj.ToList();
        }

        public async Task<SubDutyArea_History> GetAsync(int id)
        {
            var obj = await _subDutyArea_HistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SubDutyArea_HistoryOperations.Read);
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
                throw new QTDServerException(_localizer["SubDutyAreaHistoryNotFound"]);
            }
        }

    }
}
