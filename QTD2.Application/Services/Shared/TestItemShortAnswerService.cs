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
using QTD2.Infrastructure.Model.TestItemShortAnswer;
using ITestItemShortAnswerDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemShortAnswerService;

namespace QTD2.Application.Services.Shared
{
    public class TestItemShortAnswerService : ITestItemShortAnswerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestItemShortAnswerService> _localizer;
        private readonly ITestItemShortAnswerDomainService _testItemShortAnswerService;
        private readonly UserManager<AppUser> _userManager;

        public TestItemShortAnswerService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<TestItemShortAnswerService> localizer, ITestItemShortAnswerDomainService testItemShortAnswerService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _testItemShortAnswerService = testItemShortAnswerService;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Activate();

                var validationResult = await _testItemShortAnswerService.UpdateAsync(obj);
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

        public async Task<TestItemShortAnswer> CreateAsync(TestItemShortAnswerCreateOptions options)
        {
            // var obj = (await _testItemShortAnswerService.FindAsync(x => x.Responses == options.Responses && x.TestItemId == options.TestItemId)).FirstOrDefault();
            // if (obj == null)
            // {
            //     obj = new TestItemShortAnswer(options.TestItemId, options.Responses, options.IsCaseSensitive, options.AcceptableResponses);
            // }
            // else
            // {
            //     throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            // }

            var obj = new TestItemShortAnswer(options.TestItemId, options.Responses, options.IsCaseSensitive, options.AcceptableResponses, options.Number);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _testItemShortAnswerService.AddAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Delete();

                var validationResult = await _testItemShortAnswerService.UpdateAsync(obj);
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

        public async Task<List<TestItemShortAnswer>> GetAsync()
        {
            var obj_list = await _testItemShortAnswerService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<TestItemShortAnswer> GetAsync(int id)
        {
            var obj = await _testItemShortAnswerService.GetAsync(id);
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
                throw new QTDServerException( _localizer["RecordNotFound"].Value);
            }
        }

        public List<TestItemShortAnswer> GetShortAnswersByIdAsync(int id)
        {
            var obj = _testItemShortAnswerService.AllQuery().Where(x => x.TestItemId == id).OrderBy(o => o.Number).ToList();
            return obj;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

            if (result.Succeeded)
            {
                obj.Deactivate();

                var validationResult = await _testItemShortAnswerService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteWithItemId(int id)
        {
            var obj = _testItemShortAnswerService.AllQuery().Where(x => x.TestItemId == id).ToList();
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].Delete();
                await _testItemShortAnswerService.UpdateAsync(obj[i]);
            }
        }

        public async Task<TestItemShortAnswer> UpdateAsync(int id, TestItemShortAnswerUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            if (result.Succeeded)
            {
                obj.AcceptableResponses = options.AcceptableResponses;
                obj.Responses = options.Responses;
                obj.IsCaseSensitive = options.IsCaseSensitive;
                obj.TestItemId = options.TestItemId;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _testItemShortAnswerService.UpdateAsync(obj);
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
    }
}
