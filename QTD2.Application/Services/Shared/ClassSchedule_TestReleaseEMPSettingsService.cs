using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ClassSchedule_TestRelease_EmpSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClassSchedule_TestReleaseEMPSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using ClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IClassSchedule_TestReleaseEMPSetting_Retake_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSetting_Retake_LinksService;
using Microsoft.AspNetCore.Identity;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Application.Services.Shared
{
    public class ClassSchedule_TestReleaseEMPSettingsService : IClassSchedule_TestReleaseEMPSettingsService
    {
        private readonly IClassSchedule_TestReleaseEMPSettingsDomainService _classSchedule_TestReleaseEMPSettingsDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer _localizer;
        private readonly ClassScheduleDomainService _classScheduleService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITestService _testService;
        private readonly IClassSchedule_TestReleaseEMPSetting_Retake_LinkDomainService _classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService;

        public ClassSchedule_TestReleaseEMPSettingsService(IClassSchedule_TestReleaseEMPSettingsDomainService classSchedule_TestReleaseEMPSettingsDomainService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleService> localizer, ClassScheduleDomainService classScheduleService, UserManager<AppUser> userManager, ITestService testService, IClassSchedule_TestReleaseEMPSetting_Retake_LinkDomainService classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService)
        {
            _classSchedule_TestReleaseEMPSettingsDomainService = classSchedule_TestReleaseEMPSettingsDomainService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _classScheduleService = classScheduleService;
            _userManager = userManager;
            _testService = testService;
            _classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService = classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService;
        }

        public async Task<ClassSchedule_TestReleaseEMPSettingVM> CreateAsync(int classScheduleId)
        {
            var existingSetting = await _classSchedule_TestReleaseEMPSettingsDomainService.GetTestSettingsByClassId(classScheduleId);
            if (existingSetting != null)
            {
                return await GetAsync(existingSetting.ClassScheduleId);
            }
            else
            {
                var classSchedule = await _classScheduleService.GetClassScheduleWithIlaTestSettings(classScheduleId);
                var isRecurring = classSchedule.IsRecurring;
                var ilaTestRelease = classSchedule?.ILA?.TestReleaseEMPSettings;


                if (ilaTestRelease != null)
                {
                    if (isRecurring)
                    {
                        var recurringSchedules = (await _classScheduleService.GetRecurringClassSchedules(classSchedule?.RecurrenceId)).Where(cs => cs.Id != classScheduleId);
                        foreach (var schdules in recurringSchedules)
                        {
                            var classScheduleTestReleaseSettings = new ClassSchedule_TestReleaseEMPSetting(schdules.Id, ilaTestRelease.FinalTestId, ilaTestRelease.PreTestId, ilaTestRelease.UsePreTestAndTest, ilaTestRelease.PreTestRequired, ilaTestRelease.PreTestAvailableOnEnrollment, ilaTestRelease.PreTestAvailableOneStartDate, ilaTestRelease.ShowStudentSubmittedPreTestAnswers, ilaTestRelease.ShowCorrectIncorrectPreTestAnswers, ilaTestRelease.MakeAvailableBeforeDays, ilaTestRelease.FinalTestPassingScore, ilaTestRelease.MakeFinalTestAvailableImmediatelyAfterStartDate, ilaTestRelease.MakeFinalTestAvailableOnClassEndDate, ilaTestRelease.MakeFinalTestAvailableAfterCBTCompleted, ilaTestRelease.MakeFinalTestAvailableOnSpecificTime, ilaTestRelease.FinalTestSpecificTimePrior, ilaTestRelease.FinalTestDueDate, ilaTestRelease.ShowStudentSubmittedFinalTestAnswers, ilaTestRelease.ShowStudentSubmittedRetakeTestAnswers, ilaTestRelease.ShowCorrectIncorrectFinalTestAnswers, ilaTestRelease.ShowCorrectIncorrectRetakeTestAnswers, ilaTestRelease.AutoReleaseRetake, ilaTestRelease.RetakeEnabled, ilaTestRelease.NumberOfRetakes, ilaTestRelease.PreTestScore, ilaTestRelease.MakeAvailableBeforeWeeks, ilaTestRelease.DaysOrWeeks, ilaTestRelease.EmpSettingsReleaseTypeId);
                            var results = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTestReleaseSettings, ClassSchedule_TestReleaseEMPSettingsOperations.Create);
                            if (results.Succeeded)
                            {
                                var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                                classScheduleTestReleaseSettings.Create(userName.Id);

                                var validationResult = await _classSchedule_TestReleaseEMPSettingsDomainService.AddAsync(classScheduleTestReleaseSettings);

                                if (!validationResult.IsValid)
                                {
                                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                                }
                                else
                                {
                                    if (ilaTestRelease.NumberOfRetakes > 0)
                                    {
                                        var retakeIds = ilaTestRelease.TestReleaseEMPSetting_Retake_Links.Select(x => x.RetakeTestId).ToList();
                                        await LinkRetakes(schdules.Id, retakeIds);
                                    }
                                }
                            }
                        }
                    }
                    var classScheduleTestReleaseSetting = new ClassSchedule_TestReleaseEMPSetting(classScheduleId, ilaTestRelease.FinalTestId, ilaTestRelease.PreTestId, ilaTestRelease.UsePreTestAndTest, ilaTestRelease.PreTestRequired, ilaTestRelease.PreTestAvailableOnEnrollment, ilaTestRelease.PreTestAvailableOneStartDate, ilaTestRelease.ShowStudentSubmittedPreTestAnswers, ilaTestRelease.ShowCorrectIncorrectPreTestAnswers, ilaTestRelease.MakeAvailableBeforeDays, ilaTestRelease.FinalTestPassingScore, ilaTestRelease.MakeFinalTestAvailableImmediatelyAfterStartDate, ilaTestRelease.MakeFinalTestAvailableOnClassEndDate, ilaTestRelease.MakeFinalTestAvailableAfterCBTCompleted, ilaTestRelease.MakeFinalTestAvailableOnSpecificTime, ilaTestRelease.FinalTestSpecificTimePrior, ilaTestRelease.FinalTestDueDate, ilaTestRelease.ShowStudentSubmittedFinalTestAnswers, ilaTestRelease.ShowStudentSubmittedRetakeTestAnswers, ilaTestRelease.ShowCorrectIncorrectFinalTestAnswers, ilaTestRelease.ShowCorrectIncorrectRetakeTestAnswers, ilaTestRelease.AutoReleaseRetake, ilaTestRelease.RetakeEnabled, ilaTestRelease.NumberOfRetakes, ilaTestRelease.PreTestScore, ilaTestRelease.MakeAvailableBeforeWeeks, ilaTestRelease.DaysOrWeeks, ilaTestRelease.EmpSettingsReleaseTypeId);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTestReleaseSetting, ClassSchedule_TestReleaseEMPSettingsOperations.Create);
                    if (result.Succeeded)
                    {
                        var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                        classScheduleTestReleaseSetting.Create(userName.Id);
                        var validationResult = await _classSchedule_TestReleaseEMPSettingsDomainService.AddAsync(classScheduleTestReleaseSetting);

                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                        else
                        {
                            if (ilaTestRelease.NumberOfRetakes > 0)
                            {
                                var retakeIds = ilaTestRelease.TestReleaseEMPSetting_Retake_Links.Select(x => x.RetakeTestId).ToList();
                                await LinkRetakes(classScheduleId, retakeIds);

                            }
                            return await GetAsync(classScheduleTestReleaseSetting.ClassScheduleId);
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<ClassSchedule_TestReleaseEMPSettingVM> GetAsync(int id)
        {
            var settings = await _classSchedule_TestReleaseEMPSettingsDomainService.GetTestSettingsByClassId(id);
            if (settings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, ClassSchedule_TestReleaseEMPSettingsOperations.Read);
                if (result.Succeeded)
                {
                    return MapToClassScheduleTestReleaseVM(settings);
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

        public static ClassSchedule_TestReleaseEMPSettingVM MapToClassScheduleTestReleaseVM(ClassSchedule_TestReleaseEMPSetting entity)
        {
            return new ClassSchedule_TestReleaseEMPSettingVM
            {
                Id = entity.Id,
                ClassScheduleId = entity.ClassScheduleId,
                FinalTestId = entity.FinalTestId,
                PreTestId = entity.PreTestId,
                UsePreTestAndTest = entity.UsePreTestAndTest,
                PreTestRequired = entity.PreTestRequired,
                PreTestAvailableOnEnrollment = entity.PreTestAvailableOnEnrollment,
                PreTestAvailableOneStartDate = entity.PreTestAvailableOneStartDate,
                ShowStudentSubmittedPreTestAnswers = entity.ShowStudentSubmittedPreTestAnswers,
                ShowCorrectIncorrectPreTestAnswers = entity.ShowCorrectIncorrectPreTestAnswers,
                MakeAvailableBeforeDays = entity.MakeAvailableBeforeDays,
                MakeAvailableBeforeWeeks = entity.MakeAvailableBeforeWeeks,
                DaysOrWeeks = entity.DaysOrWeeks,
                FinalTestPassingScore = entity.FinalTestPassingScore,
                MakeFinalTestAvailableImmediatelyAfterStartDate = entity.MakeFinalTestAvailableImmediatelyAfterStartDate,
                MakeFinalTestAvailableOnClassEndDate = entity.MakeFinalTestAvailableOnClassEndDate,
                MakeFinalTestAvailableAfterCBTCompleted = entity.MakeFinalTestAvailableAfterCBTCompleted,
                MakeFinalTestAvailableOnSpecificTime = entity.MakeFinalTestAvailableOnSpecificTime,
                FinalTestSpecificTimePrior = entity.FinalTestSpecificTimePrior,
                FinalTestDueDate = entity.FinalTestDueDate,
                ShowStudentSubmittedFinalTestAnswers = entity.ShowStudentSubmittedFinalTestAnswers,
                ShowStudentSubmittedRetakeTestAnswers = entity.ShowStudentSubmittedRetakeTestAnswers,
                ShowCorrectIncorrectFinalTestAnswers = entity.ShowCorrectIncorrectFinalTestAnswers,
                ShowCorrectIncorrectRetakeTestAnswers = entity.ShowCorrectIncorrectRetakeTestAnswers,
                AutoReleaseRetake = entity.AutoReleaseRetake,
                RetakeEnabled = entity.RetakeEnabled,
                NumberOfRetakes = entity.NumberOfRetakes,
                PreTestScore = entity.PreTestScore,
                EmpSettingsReleaseTypeId = entity.EmpSettingsReleaseTypeId,
                retakesTestIds = entity.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.Select(r => r.RetakeTestId).ToList(),
                TestReleaseEMPSetting_Retake_Links = entity.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList()
            };
        }

        public async Task<ClassSchedule_TestReleaseEMPSetting> LinkRetakes(int classId, List<int> retakeTestIds)
        {
            var testSettings = await _classSchedule_TestReleaseEMPSettingsDomainService.GetTestSettingsByClassId(classId);
            var existingLinks = (await _classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService.FindAsync(x=>x.ClassSchedule_TestReleaseSettingId == testSettings.Id && retakeTestIds.Contains(x.RetakeTestId))).ToList();
            var existingRetakeIds = existingLinks.Any() ? existingLinks.Select(m => m.RetakeTestId).ToList() : new List<int>();
            foreach (var id in retakeTestIds)
            {
                if (existingRetakeIds.Contains(id)) continue;
                var test = await _testService.GetAsync(id);
                var testSettingsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSettings, TestSettingOperations.Update);
                var testResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, test, TestOperations.Read);
                if (testSettingsResult.Succeeded && testResult.Succeeded)
                {
                    testSettings.LinkRetake(test);
                    var validationResult = await _classSchedule_TestReleaseEMPSettingsDomainService.UpdateAsync(testSettings);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return testSettings;
        }

        public async Task<ClassSchedule_TestReleaseEMPSettingVM> UpdateAsync(int classId, ClassScheduleTestReleaseEmpSettingsCreateOptions options)
        {
            var testSettings = await _classSchedule_TestReleaseEMPSettingsDomainService.GetTestSettingsByClassId(classId);
            if (testSettings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSettings, TestReleaseEMPSettingsOperations.Update);
                if (result.Succeeded)
                {
                    testSettings.ClassScheduleId = classId;
                    testSettings.FinalTestId = options.FinalTestId;
                    testSettings.PreTestId = options.PreTestId;
                    testSettings.UsePreTestAndTest = options.UsePreTestAndTest;
                    testSettings.PreTestRequired = options.PreTestRequired;
                    testSettings.PreTestAvailableOnEnrollment = options.PreTestAvailableOnEnrollment;
                    testSettings.PreTestAvailableOneStartDate = options.PreTestAvailableOneStartDate;
                    testSettings.ShowStudentSubmittedPreTestAnswers = options.ShowStudentSubmittedPreTestAnswers;
                    testSettings.ShowCorrectIncorrectPreTestAnswers = options.ShowCorrectIncorrectPreTestAnswers;
                    testSettings.MakeAvailableBeforeDays = options.MakeAvailableBeforeDays;
                    testSettings.FinalTestPassingScore = options.FinalTestPassingScore;
                    testSettings.MakeFinalTestAvailableImmediatelyAfterStartDate = options.MakeFinalTestAvailableImmediatelyAfterStartDate;
                    testSettings.MakeFinalTestAvailableOnClassEndDate = options.MakeFinalTestAvailableOnClassEndDate;
                    testSettings.MakeFinalTestAvailableAfterCBTCompleted = options.MakeFinalTestAvailableAfterCBTCompleted;
                    testSettings.MakeFinalTestAvailableOnSpecificTime = options.MakeFinalTestAvailableOnSpecificTime;
                    testSettings.FinalTestSpecificTimePrior = options.FinalTestSpecificTimePrior;
                    testSettings.FinalTestDueDate = options.FinalTestDueDate;
                    testSettings.ShowStudentSubmittedFinalTestAnswers = options.ShowStudentSubmittedFinalTestAnswers;
                    testSettings.ShowStudentSubmittedRetakeTestAnswers = options.ShowStudentSubmittedRetakeTestAnswers;
                    testSettings.ShowCorrectIncorrectFinalTestAnswers = options.ShowCorrectIncorrectFinalTestAnswers;
                    testSettings.ShowCorrectIncorrectRetakeTestAnswers = options.ShowCorrectIncorrectRetakeTestAnswers;
                    testSettings.AutoReleaseRetake = options.AutoReleaseRetake;
                    testSettings.RetakeEnabled = options.RetakeEnabled;
                    testSettings.NumberOfRetakes = options.NumberOfRetakes;
                    testSettings.PreTestScore = options.PreTestScore;
                    testSettings.DaysOrWeeks = options.DaysOrWeeks;
                    testSettings.MakeAvailableBeforeWeeks = options.MakeAvailableBeforeWeeks;
                    testSettings.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    testSettings.ModifiedDate = DateTime.UtcNow;
                    testSettings.EmpSettingsReleaseTypeId = options.EmpSettingsReleaseTypeId;

                    var validationResult = await _classSchedule_TestReleaseEMPSettingsDomainService.UpdateAsync(testSettings);
                    if (validationResult.IsValid)
                    {
                        await UnlinkAllRetakes(testSettings.ClassScheduleId);
                        if (options.NumberOfRetakes > 0)
                        {
                            await LinkRetakes(testSettings.ClassScheduleId, options.retakesTestIds);

                        }
                        return MapToClassScheduleTestReleaseVM(testSettings);
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
                var classScheduleTestReleaseSetting = new ClassSchedule_TestReleaseEMPSetting(classId, options.FinalTestId, options.PreTestId, options.UsePreTestAndTest, options.PreTestRequired, options.PreTestAvailableOnEnrollment, options.PreTestAvailableOneStartDate, options.ShowStudentSubmittedPreTestAnswers, options.ShowCorrectIncorrectPreTestAnswers, options.MakeAvailableBeforeDays, options.FinalTestPassingScore, options.MakeFinalTestAvailableImmediatelyAfterStartDate, options.MakeFinalTestAvailableOnClassEndDate, options.MakeFinalTestAvailableAfterCBTCompleted, options.MakeFinalTestAvailableOnSpecificTime, options.FinalTestSpecificTimePrior, options.FinalTestDueDate, options.ShowStudentSubmittedFinalTestAnswers, options.ShowStudentSubmittedRetakeTestAnswers, options.ShowCorrectIncorrectFinalTestAnswers, options.ShowCorrectIncorrectRetakeTestAnswers, options.AutoReleaseRetake, options.RetakeEnabled, options.NumberOfRetakes, options.PreTestScore, options.MakeAvailableBeforeWeeks, options.DaysOrWeeks, options.EmpSettingsReleaseTypeId);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, classScheduleTestReleaseSetting, ClassSchedule_TestReleaseEMPSettingsOperations.Create);
                if (result.Succeeded)
                {
                    var userName = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                    classScheduleTestReleaseSetting.Create(userName.Id);
                    var validationResult = await _classSchedule_TestReleaseEMPSettingsDomainService.AddAsync(classScheduleTestReleaseSetting);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        if (options.NumberOfRetakes > 0)
                        {
                            await LinkRetakes(classId, options.retakesTestIds);

                        }
                        return await GetAsync(classScheduleTestReleaseSetting.ClassScheduleId);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkAllRetakes(int classId)
        {
            var testSettings = await _classSchedule_TestReleaseEMPSettingsDomainService.GetTestSettingsByClassId(classId);
            if (testSettings == null)
            {
                throw new BadHttpRequestException(message: _localizer["ClassScheduleTestReleaseEMPSettingsNotFound"]);
            }
            else
            {
                var retakeList = testSettings.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList();
                foreach (var retakeLink in retakeList)
                {
                    var link = (await _classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService.FindAsync(x => x.Id == retakeLink.Id && x.ClassSchedule_TestReleaseSettingId == testSettings.Id)).FirstOrDefault();
                    if (link != null)
                    {
                        await _classSchedule_TestReleaseEMPSetting_Retake_LinkDomainService.DeleteAsync(link);
                    }
                }
            }
        }
    }
}
