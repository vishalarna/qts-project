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
using QTD2.Infrastructure.Model.ActivityNotification;
using IActivityNotificationDomainService = QTD2.Domain.Interfaces.Service.Core.IActivityNotificationService;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.Services.Shared
{
    public class CbtScormRegistrationService : ICbtScormRegistrationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ActivityNotificationService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        private readonly ICBT_ScormRegistrationService _cbtScormRegistrationService;


        public CbtScormRegistrationService(
                IHttpContextAccessor httpContextAccessor,
                IAuthorizationService authorizationService,
                IStringLocalizer<ActivityNotificationService> localizer,
                UserManager<AppUser> userManager,
                ICBT_ScormRegistrationService cbtScormRegistrationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;

            _cbtScormRegistrationService = cbtScormRegistrationService;
        }

        public async System.Threading.Tasks.Task BulkUpdateCbtRegistrationsAsync(int classScheduleId, ClassRoasterUpdateOptions options)
        {
            var cbtScormRegistrations = await _cbtScormRegistrationService.GetByClassScheduleIdAsync(classScheduleId);
            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).UserName;

            foreach (var cbtScormRegistration in cbtScormRegistrations)
            {
                await modifyScormRegistrationAsync(cbtScormRegistration, options, modifiedBy);
            }
        }

        public async System.Threading.Tasks.Task UpdateCbtRegistrationAsync(int employeeId, ClassRoasterUpdateOptions options)
        {
            var modifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).UserName;
            var cbtScormRegistration = await _cbtScormRegistrationService.GetCbtScormRegistrationAsync(employeeId, options.ClassId,options.TestId);

            await modifyScormRegistrationAsync(cbtScormRegistration, options, modifiedBy);
        }

        private async System.Threading.Tasks.Task modifyScormRegistrationAsync(CBT_ScormRegistration cbtScormRegistration, ClassRoasterUpdateOptions options, string modifiedBy)
        {
            if (options.CompDate.HasValue) 
            {
                cbtScormRegistration.UpdateCompletedDate(options.CompDate.Value, "Manual", modifiedBy);
            }
            if (!String.IsNullOrEmpty(options.Grade))
            {
                cbtScormRegistration.UpdateGrade(options.Grade, "Manual", modifiedBy);
            }
            if (options.Score.HasValue)
            {
                cbtScormRegistration.UpdateScore(options.Score.Value, "Manual", modifiedBy);
            }

            await _cbtScormRegistrationService.UpdateAsync(cbtScormRegistration);
        }
    }
}
