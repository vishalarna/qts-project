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
using QTD2.Infrastructure.Model.Procedure_StatusHistory;
using IProcedure_StatusHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IProcedure_StatusHistoryService;

namespace QTD2.Application.Services.Shared
{
    public class ProcedureStatusHistoryService : Application.Interfaces.Services.Shared.IProcedureStatusHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<RR_StatusHistory> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProcedure_StatusHistoryDomainService _proc_statusHistoryService;

        public ProcedureStatusHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<RR_StatusHistory> localizer, UserManager<AppUser> userManager, IProcedure_StatusHistoryDomainService proc_statusHistoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _proc_statusHistoryService = proc_statusHistoryService;
        }

        public async Task<Procedure_StatusHistory> CreateAsync(Procedure_StatusHistoryCreateOptions options)
        {
            foreach (var id in options.ProcedureIds)
            {
                var histOptions = new Procedure_StatusHistory();
                histOptions.ChangeEffectiveDate = options.ChangeEffectiveDate;
                histOptions.CreatedDate = DateTime.Now;
                histOptions.OldStatus = options.OldStatus;
                histOptions.NewStatus = options.NewStatus;
                histOptions.ChangeNotes = options.ChangeNotes;
                histOptions.ProcedureId = id;
                histOptions.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                var histValidation = await _proc_statusHistoryService.AddAsync(histOptions);
                if (!histValidation.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', histValidation.Errors));
                }
            }

            return new Procedure_StatusHistory();
        }
    }
}
