using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.MetaILA_SummaryTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMetaILA_SummaryTestDomainService = QTD2.Domain.Interfaces.Service.Core.IMetaILA_SummaryTestService;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using IILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ITest_TestItemLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class MetaILA_SummaryTestService : IMetaILA_SummaryTestService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<MetaILA_SummaryTestService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMetaILA_SummaryTestDomainService _metaILA_SummaryTestDomainService;
        private readonly ITestItemDomainService _testItemService;
        private readonly IILATraineeEvaluationDomainService _ila_traineeEvalService;
        private readonly ITest_TestItemLinkDomainService _test_item_linkService;


        public MetaILA_SummaryTestService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<MetaILA_SummaryTestService> localizer, UserManager<AppUser> userManager,IMetaILA_SummaryTestDomainService metaILA_SummaryTestDomainService, ITestItemDomainService testItemService, IILATraineeEvaluationDomainService ila_traineeEvalService, ITest_TestItemLinkDomainService test_item_linkService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _metaILA_SummaryTestDomainService = metaILA_SummaryTestDomainService;
            _testItemService = testItemService;
            _ila_traineeEvalService = ila_traineeEvalService;
            _test_item_linkService = test_item_linkService;
        }

        public async Task<MetaILA_SummaryTest_ViewModel> CreateAsync(MetaILA_SummaryTest_ViewModel options)
        {
            var metaILA_SummaryTest = (await _metaILA_SummaryTestDomainService.FindAsync(x => x.Id == options.Id)).FirstOrDefault();
            if (metaILA_SummaryTest == null)
            {
                metaILA_SummaryTest = new MetaILA_SummaryTest(options.Test.Id, options.TestInstruction, options.TestTimeLimitHours, options.TestTimeLimitMinutes, options.TestTypeId, options.PositionId);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaILA_SummaryTest, AuthorizationOperations.Create);
            if (result.Succeeded)
            {
                var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                metaILA_SummaryTest.Create(userName);
                var validationResult = await _metaILA_SummaryTestDomainService.AddAsync(metaILA_SummaryTest);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
                else
                {
                    return await GetAsync(metaILA_SummaryTest.Id);
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
        public async Task<MetaILA_SummaryTest_ViewModel> GetAsync(int id)
        {
            var metaILA_SummaryTest = await _metaILA_SummaryTestDomainService.GetAsync(id);
            var metaILA_SummaryTest_VM = new MetaILA_SummaryTest_ViewModel(metaILA_SummaryTest.Id, metaILA_SummaryTest.TestInstruction, metaILA_SummaryTest.TestTimeLimitHours, metaILA_SummaryTest.TestTimeLimitMinutes, metaILA_SummaryTest.TestTypeId, metaILA_SummaryTest.PositionId,metaILA_SummaryTest.Test, metaILA_SummaryTest.TestType.Description);
            return metaILA_SummaryTest_VM;
        }
        public async Task<List<TestItem>> GetTestItemsFromILAsAsync(GetTestItemsByILAsOption option )
        {
            List<TestItem> testItems = new List<TestItem>();
            var ILATests = await _ila_traineeEvalService.FindQueryWithIncludeAsync(x =>option.ilaIDs.Contains(x.ILAId),  new string[] { "Test" }).Select(x => x.Test).ToListAsync();
            foreach (var test in ILATests)
            {
                var ilaLinks = await _test_item_linkService.FindQuery(x => x.TestId == test.Id).Select(x => x.TestItemId).ToListAsync();
                foreach (var itemId in ilaLinks)
                {
                    if (testItems.Find(f => f.Id == itemId) == null)
                    {
                        var item = await _testItemService.FindQueryWithIncludeAsync(x => x.Id == itemId,
                                       new string[] {"TaxonomyLevel" , "TestItemType" }, true).FirstOrDefaultAsync();
                        testItems.Add(item);
                    }
                }
            }
            return testItems;
        }

        public async Task<MetaILA_SummaryTest_ViewModel> UpdateAsync(int id, MetaILA_SummaryTest_ViewModel options)
        {
            var metaILA_SummaryTest = await _metaILA_SummaryTestDomainService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, metaILA_SummaryTest, MetaILA_SummaryTestOperations.Update);
            if (result.Succeeded)
            {
                updateMetaILA_SummaryTest(metaILA_SummaryTest, options);
                var validationResult = await _metaILA_SummaryTestDomainService.UpdateAsync(metaILA_SummaryTest);
                if (validationResult.IsValid)
                {
                    return await GetAsync(metaILA_SummaryTest.Id);
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: "OperationNotAllowed");
            }
        }

        protected void updateMetaILA_SummaryTest(MetaILA_SummaryTest metaILA_SummaryTest, MetaILA_SummaryTest_ViewModel options)
        {
            if (metaILA_SummaryTest.TestInstruction != options.TestInstruction)
            {
                metaILA_SummaryTest.SetTestInstruction(options.TestInstruction);
            }
            if (metaILA_SummaryTest.TestTimeLimitHours != options.TestTimeLimitHours)
            {
                metaILA_SummaryTest.SetTestTimeLimitHours(options.TestTimeLimitHours);
            }
            if (metaILA_SummaryTest.TestTimeLimitMinutes != options.TestTimeLimitMinutes)
            {
                metaILA_SummaryTest.SetTestTimeLimitMinutes(options.TestTimeLimitMinutes);
            }
            if (metaILA_SummaryTest.TestTypeId != options.TestTypeId)
            {
                metaILA_SummaryTest.SetTestTypeId(options.TestTypeId);
            }
            if (metaILA_SummaryTest.PositionId != options.PositionId)
            {
                metaILA_SummaryTest.SetPositionId(options.PositionId);
            }
            var modifiedBy = _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (modifiedBy != null)
            {
                metaILA_SummaryTest.Modify(modifiedBy.Result.UserName);
            }
        }
    }
}
