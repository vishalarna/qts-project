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
using QTD2.Infrastructure.Model.TestItem_History;
using ITestItem_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItem_HistoryService;

namespace QTD2.Application.Services.Shared
{
    public class TestItem_HistoryService : ITestItem_HistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestItem_History> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITestItem_HistoryDomainService _testItemHistService;

        public TestItem_HistoryService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<TestItem_History> localizer,
            UserManager<AppUser> userManager,
            ITestItem_HistoryDomainService testItemHistService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _testItemHistService = testItemHistService;
        }

        public async Task<TestItem_History> CreateTestItemHistory(TestItem_HistoryCreateOptions options)
        {
            var testItemHist = new TestItem_History();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testItemHist, TestItem_HistoryOperations.Create);
            if (result.Succeeded)
            {
                testItemHist.ChangeNotes = options.ChangeNotes;
                testItemHist.EffectiveDate = options.EffectiveDate;
                testItemHist.TestItemId = options.TestItemId;
                testItemHist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                testItemHist.CreatedDate = DateTime.Now;
                var validationResult = await _testItemHistService.AddAsync(testItemHist);
                if (validationResult.IsValid)
                {
                    return testItemHist;
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
