using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClassScheduleTQEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TQEMPSettingsService;
using IClassScheduleDomainService= QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class ClassSchedule_TQEMPSettingsService : IClassSchedule_TQEMPSettingsService
    {
        private readonly IClassScheduleTQEmpSettingDomainService _classScheduleTQEmpSettingDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer _localizer;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly UserManager<AppUser> _userManager;

        public ClassSchedule_TQEMPSettingsService(IClassScheduleTQEmpSettingDomainService classScheduleTQEmpSettingDomainService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleService> localizer, IClassScheduleDomainService classScheduleService, UserManager<AppUser> userManager)
        {
            _classScheduleTQEmpSettingDomainService = classScheduleTQEmpSettingDomainService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _classScheduleService = classScheduleService;
            _userManager = userManager;
        }

        public async Task<ClassSchedule_TQEMPSettingsVM> GetAsync(int id)
        {
            var settings = await _classScheduleTQEmpSettingDomainService.GetTQEmpSettingsByClassId(id);
            if (settings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, ClassSchedule_TQEMPSettingsOperations.Read);
                if (result.Succeeded)
                {
                    return MapToClassScheduleTQEMPSettingsVM(settings);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                return null; 
            }
        }

        public async Task<ClassSchedule_TQEMPSettingsVM> CreateAsync(int classScheduleId)
        {
            var existingSetting = await _classScheduleTQEmpSettingDomainService.GetTQEmpSettingsByClassId(classScheduleId);
            if (existingSetting != null)
            {
                return await GetAsync(existingSetting.ClassScheduleId);
            }
            else
            {
                var classSchedule = await _classScheduleService.GetClassScheduleWithIlaTQEMPSettings(classScheduleId);
                var isRecurring = classSchedule.IsRecurring;
                var ilaTQEMPSetting = classSchedule?.ILA?.TQILAEmpSettings.FirstOrDefault();
                if (ilaTQEMPSetting != null)
                {
                    if (isRecurring)
                    {
                        var recurringSchedules = (await _classScheduleService.GetRecurringClassSchedules(classSchedule?.RecurrenceId)).Where(cs => cs.Id != classScheduleId);
                        foreach (var schdules in recurringSchedules)
                        {
                            var classScheduleTQEMPSetting = new ClassSchedule_TQEMPSetting(schdules.Id, ilaTQEMPSetting.TQRequired, ilaTQEMPSetting.ReleaseOnClassStart, ilaTQEMPSetting.ReleaseOnClassEnd, ilaTQEMPSetting.SpecificTime, ilaTQEMPSetting.PriorToSpecificTime,ilaTQEMPSetting.ShowTaskSuggestions,ilaTQEMPSetting.ShowTaskQuestions);
                            var results = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTQEMPSetting, ClassSchedule_TQEMPSettingsOperations.Create);
                            if (results.Succeeded)
                            {
                                var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                                classScheduleTQEMPSetting.Create(userName.Id);
                                var validationResult = await _classScheduleTQEmpSettingDomainService.AddAsync(classScheduleTQEMPSetting);
                                if (!validationResult.IsValid)
                                {
                                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                                }
                            }
                        }
                    }

                    var classScheduleTQEMPSettings = new ClassSchedule_TQEMPSetting(classScheduleId, ilaTQEMPSetting.TQRequired, ilaTQEMPSetting.ReleaseOnClassStart, ilaTQEMPSetting.ReleaseOnClassEnd, ilaTQEMPSetting.SpecificTime, ilaTQEMPSetting.PriorToSpecificTime,ilaTQEMPSetting.ShowTaskSuggestions,ilaTQEMPSetting.ShowTaskQuestions);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTQEMPSettings, ClassSchedule_TQEMPSettingsOperations.Create);
                    if (result.Succeeded)
                    {
                        var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                        classScheduleTQEMPSettings.Create(userName.Id);
                        var validationResult = await _classScheduleTQEmpSettingDomainService.AddAsync(classScheduleTQEMPSettings);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        else
                        {
                            return await GetAsync(classScheduleTQEMPSettings.ClassScheduleId);
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowedException"]);
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<ClassSchedule_TQEMPSettingsVM> UpdateAsync(int classId, ClassSchedule_TQEMPSettingsCreateOptions options)
        {
            var testSettings = await _classScheduleTQEmpSettingDomainService.GetTQEmpSettingsByClassId(classId);
            if (testSettings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSettings, ClassSchedule_TQEMPSettingsOperations.Update);
                if (result.Succeeded)
                {
                    testSettings.ClassScheduleId = classId;
                    testSettings.TQRequired = options.TQRequired;
                    testSettings.ReleaseOnClassStart = options.ReleaseOnClassStart;
                    testSettings.ReleaseOnClassEnd = options.ReleaseOnClassEnd;
                    testSettings.SpecificTime = options.SpecificTime;
                    testSettings.PriorToSpecificTime = options.PriorToSpecificTime;
                    testSettings.ShowTaskSuggestions = options.ShowTaskSuggestions;
                    testSettings.ShowTaskQuestions = options.ShowTaskQuestions;
                    testSettings.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    testSettings.ModifiedDate = DateTime.UtcNow;

                    var validationResult = await _classScheduleTQEmpSettingDomainService.UpdateAsync(testSettings);
                    if (validationResult.IsValid)
                    {
                        return MapToClassScheduleTQEMPSettingsVM(testSettings);
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
            else
            {
                var classScheduleTQEMPSetting = new ClassSchedule_TQEMPSetting(classId, options.TQRequired,options.ReleaseOnClassStart, options.ReleaseOnClassEnd, options.SpecificTime, options.PriorToSpecificTime,options.ShowTaskSuggestions,options.ShowTaskQuestions);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTQEMPSetting, ClassSchedule_TQEMPSettingsOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    classScheduleTQEMPSetting.Create(userName.Id);
                    var validationResult = await _classScheduleTQEmpSettingDomainService.AddAsync(classScheduleTQEMPSetting);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return await GetAsync(classScheduleTQEMPSetting.ClassScheduleId);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
        }

        public static ClassSchedule_TQEMPSettingsVM MapToClassScheduleTQEMPSettingsVM(ClassSchedule_TQEMPSetting entity)
        {
            return new ClassSchedule_TQEMPSettingsVM
            {
                Id = entity.Id,
                ClassScheduleId = entity.ClassScheduleId,
                TQRequired = entity.TQRequired,
                ReleaseOnClassStart = entity.ReleaseOnClassStart,
                ReleaseOnClassEnd = entity.ReleaseOnClassEnd,
                SpecificTime = entity.SpecificTime,
                PriorToSpecificTime = entity.PriorToSpecificTime,
                Active = entity.Active,
                ShowTaskSuggestions = entity.ShowTaskSuggestions,
                ShowTaskQuestions = entity.ShowTaskQuestions
            };
        }
    }
}
