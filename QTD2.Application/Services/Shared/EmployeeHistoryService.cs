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
using QTD2.Infrastructure.Model.EmployeeHistory;
using IEmployeeHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class EmployeeHistoryService : IEmployeeHistoryService
    {
        private readonly IEmployeeHistoryDomainService _employeeHistoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EmployeeHistoryService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public EmployeeHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<EmployeeHistoryService> localizer, IEmployeeHistoryDomainService employeeHistoryService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _employeeHistoryService = employeeHistoryService;
            _userManager = userManager;
        }

        public async Task<EmployeeHistory> CreateEmployeeHistory(EmployeeHistoryCreateOptions options)
        {
            var hist = new EmployeeHistory(options.EmployeeID, options.ChangeNotes, options.ChangeEffectiveDate,options.OperationType, options.CurrentActiveStatus);
            var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, EmployeeHistoryOperations.Create);
            if (histResult.Succeeded)
            {
                hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                hist.CreatedDate = DateTime.Now;
                var validationResult = await _employeeHistoryService.AddAsync(hist);
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
