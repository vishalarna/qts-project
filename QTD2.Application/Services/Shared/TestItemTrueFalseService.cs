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
using QTD2.Infrastructure.Model.TestItemTrueFalse;
using ITestItemTrueFalseDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemTrueFalseService;

namespace QTD2.Application.Services.Shared
{
    public class TestItemTrueFalseService : ITestItemTrueFalseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestItemTrueFalseService> _localizer;
        private readonly ITestItemTrueFalseDomainService _testItemTrueFalseService;
        private readonly UserManager<AppUser> _userManager;

        public TestItemTrueFalseService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<TestItemTrueFalseService> localizer, ITestItemTrueFalseDomainService testItemTrueFalseService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _testItemTrueFalseService = testItemTrueFalseService;
            _userManager = userManager;
        }

        public async Task<TestItemTrueFalse> GetAsync(int id)
        {
            var obj = await _testItemTrueFalseService.GetAsync(id);

            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read);

                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public List<TestItemTrueFalse> GetByTestItemIdAsync(int id)
        {
            var obj = _testItemTrueFalseService.AllQuery().Where(x => x.TestItemId == id).ToList();
            return obj;
        }

        public async Task<List<TestItemTrueFalse>> GetAsync()
        {
            var obj_list = await _testItemTrueFalseService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<TestItemTrueFalse> CreateAsync(TestItemTrueFalseCreateOptions options)
        {
            var obj = (await _testItemTrueFalseService.FindAsync(x => x.TestItemId == options.TestItemId && x.Choices.ToLower().Trim() == options.Choices.ToLower().Trim())).FirstOrDefault();
            if (obj == null)
            {
                obj = new TestItemTrueFalse(options.TestItemId, options.Choices, options.IsCorrect);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _testItemTrueFalseService.AddAsync(obj);
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

        public async Task<TestItemTrueFalse> UpdateAsync(int id, TestItemTrueFalseUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.Choices = options.Choices;
                obj.IsCorrect = options.IsCorrect;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _testItemTrueFalseService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteWithItemId(int id)
        {
            var obj = _testItemTrueFalseService.AllQuery().Where(x => x.TestItemId == id).ToList();
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].Delete();
                await _testItemTrueFalseService.UpdateAsync(obj[i]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _testItemTrueFalseService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _testItemTrueFalseService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _testItemTrueFalseService.UpdateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
    }
}
