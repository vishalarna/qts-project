using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.TestItem;
using QTD2.Infrastructure.Model.TestItemTrueFalse;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using IILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ITest_TestItemLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using ITestItemTypeServiceDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemTypeService;
using IEnablingObjectiveDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjectiveService;
using ITaxonomyLevelDomainService = QTD2.Domain.Interfaces.Service.Core.ITaxonomyLevelService;
using IEnablingObjective_SubCategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_SubCategoryService;
using IEnablingObjective_CategoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_CategoryService;
using IEnablingObjective_TopicDomainService = QTD2.Domain.Interfaces.Service.Core.IEnablingObjective_TopicService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class TestItemService : ITestItemService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestItemService> _localizer;
        private readonly ITestItemDomainService _testItemService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TestItem _testItem;
        private readonly IILATraineeEvaluationDomainService _ila_traineeEvalService;
        private readonly ILATraineeEvaluation _ila_traineeEval;
        private readonly ITest_TestItemLinkDomainService _test_item_linkService;
        private readonly ITestItemTypeServiceDomainService _test_item_typeService;
        private readonly IEnablingObjectiveDomainService _eoService;
        private readonly ITaxonomyLevelDomainService _taxonomyService;
        private readonly IEnablingObjective_SubCategoryDomainService _subCatService;
        private readonly IEnablingObjective_CategoryDomainService _catService;
        private readonly IEnablingObjective_TopicDomainService _topicService;

        public TestItemService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<TestItemService> localizer, ITestItemDomainService testItemService, UserManager<AppUser> userManager, IILATraineeEvaluationDomainService ila_traineeEvalService, ITest_TestItemLinkDomainService test_item_linkService, ITestItemTypeServiceDomainService test_item_typeService, IEnablingObjectiveDomainService eoService, ITaxonomyLevelDomainService taxonomyService, IEnablingObjective_SubCategoryDomainService subCatService, IEnablingObjective_CategoryDomainService catService, IEnablingObjective_TopicDomainService topicService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _testItemService = testItemService;
            _userManager = userManager;
            _testItem = new TestItem();
            _ila_traineeEvalService = ila_traineeEvalService;
            _ila_traineeEval = new ILATraineeEvaluation();
            _test_item_linkService = test_item_linkService;
            _test_item_typeService = test_item_typeService;
            _eoService = eoService;
            _taxonomyService = taxonomyService;
            _subCatService = subCatService;
            _catService = catService;
            _topicService = topicService;
        }

        public async Task<List<TestItem>> GetAsync()
        {
            var obj_list = await _testItemService.AllQueryWithInclude(new string[] { nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).Where(r => !r.Deleted).ToListAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Read).Result.Succeeded).ToList();
            return obj_list?.ToList();
        }

        public async Task<TestItem> GetAsync(int id)
        {
            var obj = await _testItemService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).FirstOrDefaultAsync();
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Read);
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

        public async Task<int> GetTestItemNumberAsync()
        {
            var number = await _testItemService.AllQueryWitDeletedCount();
            return number + 1;
        }

        public async Task<List<TestItem>> GetTestItemWithEOAsync(int eoId)
        {
            var obj_list = (await _testItemService.AllQueryWithInclude(new string[] { nameof(_testItem.EnablingObjective), nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).ToListAsync()).Where(x => x.EOId == eoId);
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();

        }

        public async Task<List<TestItemEoVM>> GetTestItemsByEOIdsAsync(TestItemByEoOptions options)
        {
            List<TestItemEoVM> testItemsList = new List<TestItemEoVM>();
            var testItems = (await _testItemService.FindWithIncludeAsync(x => x.EOId != null && options.EOIds.Contains(x.EOId ?? 0), new string[] { "TaxonomyLevel", "TestItemType" })).ToList();
            foreach (var testitem in testItems)
            {
                var testItemVM = new TestItemEoVM();
                var number  = "";
                var enablingObjective = await _eoService.FindQuery(x => x.Id == testitem.EOId).FirstOrDefaultAsync();
                if (enablingObjective != null && !enablingObjective.Number.Contains("."))
                {
                    if (enablingObjective.TopicId == null)
                    {
                        var subcat = await _subCatService.FindQuery(x => x.Id == enablingObjective.SubCategoryId).FirstOrDefaultAsync();
                        var cat = await _catService.FindQuery(x => x.Id == enablingObjective.CategoryId).FirstOrDefaultAsync();
                        if (subcat != null && cat != null)
                        {
                            number = cat.Number.ToString() + "." + subcat.Number.ToString() + "." + ".0." + enablingObjective.Number;
                        }
                    }
                    else
                    {
                        var subcat = await _subCatService.FindQuery(x => x.Id == enablingObjective.SubCategoryId).FirstOrDefaultAsync();
                        var cat = await _catService.FindQuery(x => x.Id == enablingObjective.CategoryId).FirstOrDefaultAsync();
                        var topic = await _topicService.FindQuery(x => x.Id == enablingObjective.TopicId).FirstOrDefaultAsync();
                        if (subcat != null && cat != null)
                        {
                            number = cat.Number.ToString() + "." + subcat.Number.ToString() + "." + topic.Number.ToString() + "." + enablingObjective.Number;
                        }
                    }
                }
                testItemVM.Id = testitem.Id;
                testItemVM.EoId = enablingObjective.Id;
                testItemVM.Number = testitem.Number;
                testItemVM.QuestionDescription = testitem.Description;
                testItemVM.TestItemtypeDescription = testitem.TestItemType?.Description;
                testItemVM.TaxonomyDescription = testitem.TaxonomyLevel?.Description;
                testItemVM.TaxonomyId = testitem.TaxonomyId;
                testItemVM.TestItemTypeId = testitem.TestItemTypeId;
                testItemVM.EoNumber = number;
                testItemVM.EoDescription = enablingObjective?.Description;
                testItemsList.Add(testItemVM);
            }
            return testItemsList.OrderBy(m => m.EoDescription).ToList();

        }

        public async Task<List<TestItem>> GetWithFilterAsync(string option, int id)
        {
            List<TestItem> testItems = new List<TestItem>();
            switch (option.Trim().ToLower())
            {
                case "unlinked":
                    testItems = await _testItemService.FindQueryWithIncludeAsync(x => x.EOId == null, new string[] { nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).ToListAsync();
                    break;
                case "eo":
                    testItems = await _testItemService.FindQueryWithIncludeAsync(x => x.EOId == id, new string[] { nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).ToListAsync();
                    break;
                case "ila":
                    var ILATests = await _ila_traineeEvalService.FindQueryWithIncludeAsync(x => x.ILAId == id, new string[] { nameof(_ila_traineeEval.Test) }).Select(x => x.Test).ToListAsync();
                    foreach (var test in ILATests)
                    {
                        var ilaLinks = await _test_item_linkService.FindQuery(x => x.TestId == test.Id).Select(x => x.TestItemId).ToListAsync();
                        foreach (var itemId in ilaLinks)
                        {
                            if (testItems.Find(f => f.Id == itemId) == null)
                            {
                                var item = await _testItemService.FindQueryWithIncludeAsync(x => x.Id == itemId,
                                               new string[] { nameof(_testItem.TaxonomyLevel),
                                               nameof(_testItem.TestItemType), }, true).FirstOrDefaultAsync();
                                testItems.Add(item);
                            }
                        }
                    }
                    break;
                case "none":
                    testItems = await _testItemService.AllQueryWithInclude(new string[] { nameof(_testItem.TaxonomyLevel), nameof(_testItem.TestItemType) }, true).ToListAsync();
                    break;
            }
            return testItems.OrderBy(o => o.Number).ToList();

        }

        public async System.Threading.Tasks.Task ActiveAsync(TestItemOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = await _testItemService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Activate();

                    var validationResult = await _testItemService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task DeleteAsync(TestItemOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = (await _testItemService.FindWithIncludeAsync(x => x.Id == id, new string[] { "Test_Item_Links" })).FirstOrDefault();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Delete();

                    var validationResult = await _testItemService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task InActiveAsync(TestItemOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = await _testItemService.FindQuery(x=> x.Id == id).FirstOrDefaultAsync();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Deactivate();

                    var validationResult = await _testItemService.UpdateAsync(obj);
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

        public async Task<TestItem> UpdateAsync(int id, TestItemCreateOptions options)
        {
            var obj = await _testItemService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Update);

            if (result.Succeeded)
            {
                obj.TaxonomyId = options.TaxonomyId;
                obj.TestItemTypeId = options.TestItemTypeId;
                obj.IsActive = options.isActive;
                obj.Description = options.Description;
               
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;
                int? eoId = null;
                if (options.EOId != 0)
                {
                    eoId = options.EOId;
                }
                obj.EOId = eoId;

                var validationResult = await _testItemService.UpdateAsync(obj);
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

        public async Task<TestItem> UpdateDescriptionAndEoId(int id, TestItemUpdateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Update);

            var testExists = (await _testItemService.FindAsync(x => x.Id != id && x.TaxonomyId == options.TaxonomyId && x.TestItemTypeId == options.TestItemTypeId)).FirstOrDefault() != null;

            if (testExists)
            {
                throw new UnauthorizedAccessException(message: _localizer["TestItemAlreadyExistsWithSameName"].Value);
            }

            if (result.Succeeded)
            {
                obj.Description = options.Description;
                obj.EOId = options.EOId;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _testItemService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task ChangeEOForItemAsync(int id, TestItemChangeOptions options)
        {
            var testItem = await _testItemService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (testItem != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testItem, TestItemOperations.Update);
                if (result.Succeeded)
                {
                    testItem.EOId = options.EOId;
                    if (testItem.EOId == 0)
                    {
                        testItem.EOId = null;
                    }
                    var validationResult = await _testItemService.UpdateAsync(testItem);
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
            else
            {
                throw new BadHttpRequestException(message: _localizer["TestItemNotFoundException"]);
            }
        }

        public async Task<TestItem> CreateAsync(TestItemCreateOptions options)
        {
            //var obj = new TestItem();

            int? eoId = options.EOId;
            if (options.EOId == 0)
            {
                eoId = null;
            }
            var number = (await _testItemService.AllQueryWitDeletedCount()) + 1;
            var obj = new TestItem(options.TestItemTypeId, options.TaxonomyId, options.isActive, options.Description, "ITM-" + number, eoId);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestItemOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _testItemService.AddAsync(obj);
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

        public async Task<TestItemStatsVM> GetTestItemStats()
        {
            TestItemStatsVM stats = new TestItemStatsVM();
            var testItems = await _testItemService.AllQuery().ToListAsync();
            stats.Active = testItems.Where(x => x.Active == true).Count();
            stats.Inactive = testItems.Where(x => x.Active == false).Count();
            stats.NotLinkedEOs = testItems.Where(x => x.EOId == null).Count();

            var testItemIds = await _test_item_linkService.AllQuery().Select(x => x.TestItemId).ToListAsync();
            stats.NotLinkedTests = testItems.Select(x => x.Id).Except(testItemIds).Count();

            return stats;
        }


        public async Task<TestItem> GetItemWithDataAsync(int id)
        {
            var testItem = await _testItemService.FindQueryWithIncludeAsync(x => x.Id == id,
                                new string[] { nameof(_testItem.TaxonomyLevel),
                                               nameof(_testItem.TestItemType),
                                               nameof(_testItem.TestItemMCQs),
                                               nameof(_testItem.TestItemFillBlanks),
                                               nameof(_testItem.TestItemMatches),
                                               nameof(_testItem.TestItemShortAnswers),
                                               nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();
            testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
            testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();
            testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
            testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();

            return testItem;
        }

        public async Task<string> GetTestItemType(int typeId)
        {
            var type = await _test_item_typeService.FindQuery(x => x.Id == typeId, true).FirstOrDefaultAsync();
            return type.Description;
        }

        public async Task<List<TestItem>> GetTestItemList(string option)
        {
            var rrList = new List<TestItem>();


            switch (option.ToLower().Trim())
            {
                case "inactive":
                    rrList = await _testItemService.FindQuery(x => x.Active == false).Select(s => new TestItem
                    {
                        Id = s.Id,
                        EOId = s.EOId,
                        Description = s.Description,
                        Number = s.Number,
                        TestItemTypeId = s.TestItemTypeId,
                        TaxonomyId = s.TaxonomyId
                    }).ToListAsync();
                    break;

                case "active":
                    rrList = await _testItemService.FindQuery(x => x.Active == true).Select(s => new TestItem
                    {
                        Id = s.Id,
                        EOId = s.EOId,
                        Description = s.Description,
                        Number = s.Number,
                        TestItemTypeId = s.TestItemTypeId,
                        TaxonomyId = s.TaxonomyId
                    }).ToListAsync();
                    break;
                case "noteo":
                    rrList = await _testItemService.FindQuery(x => x.EOId == null).Select(s => new TestItem
                    {
                        Id = s.Id,
                        EOId = s.EOId,
                        Description = s.Description,
                        Number = s.Number,
                        TestItemTypeId = s.TestItemTypeId,
                        TaxonomyId = s.TaxonomyId
                    }).ToListAsync();
                    break;
                case "notlinked":
                    var testItemIds = await _test_item_linkService.AllQuery().Select(x => x.TestItemId).ToListAsync();
                    rrList = await _testItemService.AllQueryWithInclude(new string[] { "TaxonomyLevel", "TestItemType" })
                        .Where(x => !testItemIds.Contains(x.Id))
                        .ToListAsync(); 
                    break;

            }
            return rrList.OrderBy(x=>x.Description).ToList();
        }


    }
}
