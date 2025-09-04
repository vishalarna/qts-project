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
using QTD2.Infrastructure.Model.TestSetting;
using ITestSettingDomainService = QTD2.Domain.Interfaces.Service.Core.ITestSettingService;

namespace QTD2.Application.Services.Shared
{
    public class TestSettingService : ITestSettingService
    {
        private readonly ITestSettingDomainService _testSettingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestSettingService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public TestSettingService(ITestSettingDomainService testSettingService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<TestSettingService> localizer, UserManager<AppUser> userManager)
        {
            _testSettingService = testSettingService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActivateAsync(int id)
        {
            var testSetting = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSetting, TestSettingOperations.Update);
            if (result.Succeeded)
            {
                testSetting.Activate();
                var validationResult = await _testSettingService.UpdateAsync(testSetting);

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

        public async Task<TestSetting> CreateAync(TestSettingCreateOptions options)
        {
            var obj = (await _testSettingService.FindAsync(x => x.Description == options.Description)).FirstOrDefault();
            if (obj == null)
            {
                obj = new TestSetting(options.Description, options.IsDefault, options.IsOverride);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestSettingOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _testSettingService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return obj;
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task DeactivateAsync(int id)
        {
            var testSetting = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSetting, TestSettingOperations.Update);
            if (result.Succeeded)
            {
                testSetting.Deactivate();
                var validationResult = await _testSettingService.UpdateAsync(testSetting);

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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var testSetting = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSetting, TestSettingOperations.Delete);
            if (result.Succeeded)
            {
                testSetting.Delete();
                var validationResult = await _testSettingService.UpdateAsync(testSetting);

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

        public async Task<List<TestSetting>> GetAsync()
        {
            var testSettings = await _testSettingService.AllAsync();
            testSettings = testSettings.Where(p => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, p, TestSettingOperations.Read).Result.Succeeded);
            return testSettings.ToList();
        }

        public async Task<TestSetting> GetAsync(int id)
        {
            var testSetting = await _testSettingService.GetAsync(id);
            if (testSetting != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSetting, TestSettingOperations.Read);
                if (result.Succeeded)
                {
                    return testSetting;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return testSetting;
        }

        public async Task<TestSetting> UpdateAsync(int id, TestSettingUpdateOptions option)
        {
            var testSetting = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testSetting, TestSettingOperations.Update);
            if (result.Succeeded)
            {
                testSetting.Description = option.Description;
                testSetting.IsOverride = option.IsOverride;
                testSetting.IsDefault = option.IsDefault;
                testSetting.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                testSetting.ModifiedDate = DateTime.Now;
                var validationResult = await _testSettingService.UpdateAsync(testSetting);

                if (validationResult.IsValid)
                {
                    return testSetting;
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
