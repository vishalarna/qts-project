using QTD2.Domain.Entities.Core;
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
using QTD2.Infrastructure.Authorization.Operations.Core;
using ITestReleaseEmpSettingsDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSettingsService;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using QTD2.Infrastructure.Model.ILA_TestRelease_EMPSettings;
using Microsoft.EntityFrameworkCore;
using ITestRelease_Retake_Link = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;

namespace QTD2.Application.Services.Shared
{
    public class TestReleaseEmpSettingsService : Interfaces.Services.Shared.ITestReleaseEmpSettingsService
    {
        private readonly ITestReleaseEmpSettingsDomainService _testReleaseEmpSettingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ClassScheduleService> _localizer;
        private readonly TestReleaseEMPSettings _testReleaseSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITestDomainService _testSerivce;
        private readonly ITestRelease_Retake_Link _testSetting_retake_linkService;
        private readonly IClassScheduleDomainService _classScheduleDomainService;

        public TestReleaseEmpSettingsService(IHttpContextAccessor httpContextAccessor, IClassScheduleDomainService classScheduleDomainService, IAuthorizationService authorizationService, IStringLocalizer<ClassScheduleService> localizer, ITestReleaseEmpSettingsDomainService testReleaseEmpSettingService, UserManager<AppUser> userManager, ITestDomainService testSerivce, ITestRelease_Retake_Link testSetting_retake_linkService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _testReleaseEmpSettingService = testReleaseEmpSettingService;
            _userManager = userManager;
            _testReleaseSettings = new TestReleaseEMPSettings();
            _testSerivce = testSerivce;
            _testSetting_retake_linkService = testSetting_retake_linkService;
            _classScheduleDomainService = classScheduleDomainService;
        }

        public async Task<TestReleaseEMPSettings> CreateAsync(ILATestReleaseEmpSettingsCreateOptions options)
        {
            var exists = await _testReleaseEmpSettingService.FindQuery(x => x.ILAId == options.ILAId).FirstOrDefaultAsync();
            if(exists == null)
            {
                var settings = new TestReleaseEMPSettings(options.ILAId, options.FinalTestId, options.PreTestId, options.UsePreTestAndTest, options.PreTestRequired, options.PreTestAvailableOnEnrollment,
                options.PreTestAvailableOneStartDate, options.ShowStudentSubmittedPreTestAnswers, options.ShowCorrectIncorrectPreTestAnswers,
                options.MakeAvailableBeforeDays, options.FinalTestPassingScore, options.MakeFinalTestAvailableImmediatelyAfterStartDate, options.MakeFinalTestAvailableOnClassEndDate, options.MakeFinalTestAvailableAfterCBTCompleted,
                options.MakeFinalTestAvailableOnSpecificTime, options.FinalTestSpecificTimePrior, options.FinalTestDueDate, options.ShowStudentSubmittedFinalTestAnswers, options.ShowStudentSubmittedRetakeTestAnswers,
                options.ShowCorrectIncorrectFinalTestAnswers, options.ShowCorrectIncorrectRetakeTestAnswers, options.AutoReleaseRetake, options.RetakeEnabled, options.NumberOfRetakes, options.PreTestScore,
                options.MakeAvailableBeforeWeeks,options.DaysOrWeeks,options.EmpSettingsReleaseTypeId);


                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, TestReleaseEMPSettingsOperations.Create);

                if (result.Succeeded)
                {
                    settings.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    settings.CreatedDate = DateTime.UtcNow;
                    var validationResult = await _testReleaseEmpSettingService.AddAsync(settings);

                    //now add Retake and number of retakes
                    if (options.NumberOfRetakes > 0)
                    {
                        await LinkRetakes(settings.Id, options.retakesTestIds);

                    }

                    if (validationResult.IsValid)
                    {
                        return settings;
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
                var setting = await UpdateAsync(exists.ILAId, options);
                return setting;
            }
            
        }


    
        public async Task<TestReleaseEMPSettings> DeleteAsync(int id)
        {
            var settings = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, TestReleaseEMPSettingsOperations.Delete);
            if (result.Succeeded)
            {
                settings.Delete();
                var validationResult = await _testReleaseEmpSettingService.UpdateAsync(settings);

                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    settings = (await _testReleaseEmpSettingService.FindAsync(r => r.Id == id)).FirstOrDefault();
                    return settings;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<TestReleaseEMPSettings>> GetAsync()
        {
            var settings = await _testReleaseEmpSettingService.AllAsync();
            return settings.ToList();
        }

        public async Task<TestReleaseEMPSettings> GetAsync(int id)
        {
            var settings = await _testReleaseEmpSettingService.GetAsync(id);
            if (settings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, TestReleaseEMPSettingsOperations.Read);
                if (result.Succeeded)
                {
                    return settings;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return settings;
        }

        public async System.Threading.Tasks.Task UnlinkAllRetakes(int id)
        {
            var setting = await _testReleaseEmpSettingService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if(setting == null)
            {
                throw new BadHttpRequestException(message: _localizer["TestReleaseEMPSettingsNotFound"]);
            }
            else
            {
                var retakeList = await _testSetting_retake_linkService.FindQuery(x => x.TestReleaseSettingId == id).Select(s => s.RetakeTestId).ToListAsync();
                for (int i =0;i < retakeList.Count;i++)
                {
                    var link = await _testSetting_retake_linkService.FindQuery(x => x.TestReleaseSettingId == id && x.RetakeTestId == retakeList[i]).FirstOrDefaultAsync();
                    if(link != null)
                    {
                        await _testSetting_retake_linkService.DeleteAsync(link);
                    }
                }
            }
        }

        public async Task<TestReleaseEMPSettings> UpdateAsync(int id, ILATestReleaseEmpSettingsCreateOptions options)
        {
            var settings = (await _testReleaseEmpSettingService.FindAsync(x=>x.ILAId==options.ILAId)).FirstOrDefault();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, settings, TestReleaseEMPSettingsOperations.Update);
            if (result.Succeeded)
            {
                settings.ILAId = id;
                settings.FinalTestId = options.FinalTestId;
                settings.PreTestId = options.PreTestId;
                settings.UsePreTestAndTest = options.UsePreTestAndTest;
                settings.PreTestRequired = options.PreTestRequired;
                settings.PreTestAvailableOnEnrollment = options.PreTestAvailableOnEnrollment;
                settings.PreTestAvailableOneStartDate = options.PreTestAvailableOneStartDate;
                settings.ShowStudentSubmittedPreTestAnswers = options.ShowStudentSubmittedPreTestAnswers;
                settings.ShowCorrectIncorrectPreTestAnswers = options.ShowCorrectIncorrectPreTestAnswers;
                settings.MakeAvailableBeforeDays = options.MakeAvailableBeforeDays;
                settings.FinalTestPassingScore = options.FinalTestPassingScore;
                settings.MakeFinalTestAvailableImmediatelyAfterStartDate = options.MakeFinalTestAvailableImmediatelyAfterStartDate;
                settings.MakeFinalTestAvailableOnClassEndDate = options.MakeFinalTestAvailableOnClassEndDate;
                settings.MakeFinalTestAvailableAfterCBTCompleted = options.MakeFinalTestAvailableAfterCBTCompleted;
                settings.MakeFinalTestAvailableOnSpecificTime = options.MakeFinalTestAvailableOnSpecificTime;
                settings.FinalTestSpecificTimePrior = options.FinalTestSpecificTimePrior;
                settings.FinalTestDueDate = options.FinalTestDueDate;
                settings.ShowStudentSubmittedFinalTestAnswers = options.ShowStudentSubmittedFinalTestAnswers;
                settings.ShowStudentSubmittedRetakeTestAnswers = options.ShowStudentSubmittedRetakeTestAnswers;
                settings.ShowCorrectIncorrectFinalTestAnswers = options.ShowCorrectIncorrectFinalTestAnswers;
                settings.ShowCorrectIncorrectRetakeTestAnswers = options.ShowCorrectIncorrectRetakeTestAnswers;
                settings.AutoReleaseRetake = options.AutoReleaseRetake;
                settings.RetakeEnabled = options.RetakeEnabled;
                settings.NumberOfRetakes = options.NumberOfRetakes;
                settings.PreTestScore = options.PreTestScore;
                settings.DaysOrWeeks = options.DaysOrWeeks;
                settings.MakeAvailableBeforeWeeks = options.MakeAvailableBeforeWeeks;
                settings.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                settings.ModifiedDate = DateTime.UtcNow;
                settings.EmpSettingsReleaseTypeId = options.EmpSettingsReleaseTypeId;



                var validationResult = await _testReleaseEmpSettingService.UpdateAsync(settings);

                await UnlinkAllRetakes(settings.Id);
                if (options.NumberOfRetakes > 0)
                {
                    await LinkRetakes(settings.Id, options.retakesTestIds);

                }

                if (validationResult.IsValid)
                {
                    return settings;
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

        public async Task<TestReleaseEMPSettings> GetTTestReleaseEMPSettingsForILAAsync(int ilaId)

        {
            //var testSettings = await _testReleaseEmpSettingService.FindQuery(x => x.ILAId == ilaId).FirstOrDefaultAsync();
            var testSettings = await _testReleaseEmpSettingService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "TestReleaseEMPSetting_Retake_Links" }).FirstOrDefaultAsync();
            if (testSettings != null)
            {
                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSettings, TestReleaseEMPSettingsOperations.Read);
                if (authResult.Succeeded)
                {
                    return testSettings;
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

        public async Task<TestReleaseEMPSettings> LinkRetakes(int testSettingsId, List<int> retakeTestIds)
        {

            var testSettings = await _testReleaseEmpSettingService.GetWithIncludeAsync(testSettingsId, new string[] { nameof(_testReleaseSettings.TestReleaseEMPSetting_Retake_Links) });
            foreach (var id in retakeTestIds)
            {
                var test = await _testSerivce.GetAsync(id);

                var testSettingsResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSettings, TestSettingOperations.Update);
                var testResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, test, TestOperations.Read);
                if (testSettingsResult.Succeeded && testResult.Succeeded)
                {
                    testSettings.LinkRetake(test);
                    var validationResult = await _testReleaseEmpSettingService.UpdateAsync(testSettings);
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
    }
}
