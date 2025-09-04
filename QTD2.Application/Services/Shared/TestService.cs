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
using QTD2.Infrastructure.Model.Test;
using QTD2.Infrastructure.Model.Test_Item_Link;
using ITestDomainService = QTD2.Domain.Interfaces.Service.Core.ITestService;
using ITestItemLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_Item_LinkService;
using IIILATraineeEvaluationDomainService = QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService;
using ITestItemDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemService;
using ITestStatusDomainService = QTD2.Domain.Interfaces.Service.Core.ITestStatusService;
using IILA_EnablingObjectiveLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILA_EnablingObjective_LinkService;
using ITestReleaseRetakeLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using IClassScheduleDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using IClassSchedule_EmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IClassScheduleEmployeeService;
using IClassSchedule_RosterDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_RosterService;
using IClassSchedule_RosterStatusesDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_Roster_StatusesService;
using IClassTestReleaseEmpSettingDomainService = QTD2.Domain.Interfaces.Service.Core.IClassSchedule_TestReleaseEMPSettingsService;
using ITestReleaseEMPSetting_RetakeLinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITestReleaseEMPSetting_Retake_LinkService;
using IEmployeeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using IPersonDomainService = QTD2.Domain.Interfaces.Service.Core.IPersonService;
using ITaxonomyLevelDomainService = QTD2.Domain.Interfaces.Service.Core.ITaxonomyLevelService;
using ITestItemTypeDomainService = QTD2.Domain.Interfaces.Service.Core.ITestItemTypeService;
using Microsoft.EntityFrameworkCore;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.TestItem;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Model.TreeDataVMs;
using QTD2.Infrastructure.Model.EmployeeTest;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.ExtensionMethods;
using System.Text.RegularExpressions;
using System.Net;


namespace QTD2.Application.Services.Shared
{
    public class TestService : ITestService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<TestService> _localizer;
        private readonly ITestDomainService _testService;
        private readonly IIILATraineeEvaluationDomainService _iLATraineeEvaluationDomainService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITestItemService _testItemService;
        private readonly Test _test;
        private readonly TestItem _testItem;
        private readonly Test_Item_Link _test_Item_Link;
        private readonly ILATraineeEvaluation _iLATraineeEvaluation;
        private readonly ILA_EnablingObjective_Link _ilaEnablingObjective;
        private readonly ITestItemLinkDomainService _testItemLinkService;
        private readonly ITestItemDomainService _testItem_Service;
        private readonly ITestStatusDomainService _testStatusService;
        private readonly IILA_EnablingObjectiveLinkDomainService _iLA_EOLinkService;
        private readonly IILATraineeEvaluationService _ilaTraineeEvaluationService;
        private readonly ITestReleaseRetakeLinkDomainService _testRelease_RetakeLinkService;
        private readonly IClassScheduleDomainService _classScheduleService;
        private readonly IILADomainService _ilaService;
        private readonly IClassSchedule_EmployeeDomainService _classSchedule_employeeService;
        private readonly IClassSchedule_RosterDomainService _classSchedule_RosterService;
        private readonly IClassSchedule_RosterStatusesDomainService _classSchedule_rosterStatusService;
        private readonly ITestReleaseEMPSetting_RetakeLinkDomainService _testReleaseEMPSetting_RetakeLinkService;
        private readonly IEmployeeDomainService _empService;
        private readonly IPersonDomainService _personService;
        private readonly ITaxonomyLevelDomainService _taxonomyLevelService;
        private readonly ITestItemTypeDomainService _testItemTypeService;
        private readonly IClassTestReleaseEmpSettingDomainService _classTestReleaseEmpSettingDomainService;

        public TestService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<TestService> localizer, ITestDomainService testService, UserManager<AppUser> userManager, ITestItemService testItemService, ITestItemLinkDomainService testItemLinkService, IIILATraineeEvaluationDomainService iLATraineeEvaluationService, ITestStatusDomainService testStatusService, ITestItemDomainService testItem_Service, IILA_EnablingObjectiveLinkDomainService iLA_EOLinkService, ITestReleaseRetakeLinkDomainService testRelease_RetakeLinkService, IClassScheduleDomainService classScheduleService, IILADomainService ilaService, IClassSchedule_EmployeeDomainService classSchedule_employeeService, IClassSchedule_RosterDomainService classSchedule_RosterService, IClassSchedule_RosterStatusesDomainService classSchedule_rosterStatusService, ITestReleaseRetakeLinkDomainService testReleaseEMPSetting_RetakeLinkService, IEmployeeDomainService empService, IPersonDomainService personService, ITaxonomyLevelDomainService taxonomyLevelService, ITestItemTypeDomainService testItemTypeService, IClassTestReleaseEmpSettingDomainService classTestReleaseEmpSettingDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _testService = testService;
            _testItemService = testItemService;
            _test = new Test();
            _testItem = new TestItem();
            _test_Item_Link = new Test_Item_Link();
            _iLATraineeEvaluation = new ILATraineeEvaluation();
            _ilaEnablingObjective = new ILA_EnablingObjective_Link();
            _testItemLinkService = testItemLinkService;
            _iLATraineeEvaluationDomainService = iLATraineeEvaluationService;
            _testStatusService = testStatusService;
            _testItem_Service = testItem_Service;
            _iLA_EOLinkService = iLA_EOLinkService;
            _testRelease_RetakeLinkService = testRelease_RetakeLinkService;
            _classScheduleService = classScheduleService;
            _ilaService = ilaService;
            _classSchedule_employeeService = classSchedule_employeeService;
            _classSchedule_RosterService = classSchedule_RosterService;
            _classSchedule_rosterStatusService = classSchedule_rosterStatusService;
            _testReleaseEMPSetting_RetakeLinkService = testReleaseEMPSetting_RetakeLinkService;
            _empService = empService;
            _personService = personService;
            _taxonomyLevelService = taxonomyLevelService;
            _testItemTypeService = testItemTypeService;
            _classTestReleaseEmpSettingDomainService = classTestReleaseEmpSettingDomainService;
        }

        public async Task<List<Test>> GetAsync()
        {
            var obj_list = await _testService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

        public async Task<Test> GetAsync(int id)
        {
            var obj = await _testService.FindQueryWithIncludeAsync(x => x.Id == id, new string[] { nameof(_test.ILATraineeEvaluations) }, true).FirstOrDefaultAsync();
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

        public async Task<List<ILATraineeEvaluation>> GetILAWithTestAsync()
        {
            var obj_list = (await _iLATraineeEvaluationDomainService.AllWithIncludeAsync(new string[] { nameof(_iLATraineeEvaluation.Test), nameof(_iLATraineeEvaluation.TestType) }));
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();

        }

        public async Task<List<Test>> GetILAWithAllTestAsync()
        {
            var obj_list = await _testService.AllWithIncludeAsync(new string[] { "ILATraineeEvaluations.ILA.Provider", "ILATraineeEvaluations.TestType" });
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();

        }

        public async Task<List<TestDataVM>> GetAllTestsByTypeAsync(string testType)
        {
            var testsList = await _testService.GetTestsByTestTypeAsync(testType);
            return testsList.Select(t=>new TestDataVM(t.Id,t.ILATraineeEvaluations?.FirstOrDefault()?.ILA?.ProviderId,t.ILATraineeEvaluations?.FirstOrDefault()?.ILAId,t.TestTitle,t.Active)).ToList();
        }

        public async Task<List<ILAWithTestVM>> GetILAWithTestMinimalTreeAsync()
        {
            List<ILAWithTestVM> toReturnVM = new List<ILAWithTestVM>();
            var trEvals = await _iLATraineeEvaluationDomainService.AllQuery().Select(s => new ILATraineeEvaluation { ILAId = s.ILAId, TestId = s.TestId, Active = s.Active }).Distinct().ToListAsync();
            var ilaIds = trEvals.Select(s => s.ILAId).ToList();
            var ilas = await _ilaService.GetCompactedByIds(ilaIds);
            foreach (var ila in ilas)
            {
                var testIds = trEvals.Where(w => w.ILAId == ila.Id).Select(s => s.TestId).ToList();
                var tests = await _testService.GetMinimalTestsByTestIds(testIds);
                ILAWithTestVM data = new ILAWithTestVM
                {
                    Active = ila.Active,
                    Id = ila.Id,
                    Name = ila.Name,
                    Number = ila.Number,
                    Tests = tests.Select(s => new TestTreeVM
                    {
                        Active = s.Active,
                        Id = s.Id,
                        TestTitle = s.TestTitle,
                    }).ToList(),
                };

                toReturnVM.Add(data);
            }
            return toReturnVM;
        }

        public async Task<List<TestWithCountOptions>> GetTestLinkedtoILAAsync(int ilaId)
        {
            //var obj_list = (await _iLATraineeEvaluationDomainService.AllWithIncludeAsync(new string[] { nameof(_iLATraineeEvaluation.Test), nameof(_iLATraineeEvaluation.TestType) })).Where(x => x.ILAId == ilaId);

            var obj_list = await _iLATraineeEvaluationDomainService.GetLinkedTestsAsync(ilaId);

            var testWithCount = new List<TestWithCountOptions>();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded).ToList();
            foreach (var obj in obj_list)
            {
                if (obj.TestType != null)
                {
                    TestWithCountOptions test = new TestWithCountOptions();
                    test.Id = obj.TestId;
                    test.TestNum = obj.TestId.ToString();
                    test.TestTitle = obj.Test.TestTitle;
                    test.TestType = obj.TestType.Description;
                    test.Active = obj.Test.Active;
                    test.isPublished = obj.Test.IsPublished;

                    var testItems = (await _testItemLinkService.FindAsync(x => x.TestId == obj.TestId)).ToList();
                    var count = testItems.Count();
                    test.NumberOfQuestions = count.ToString();

                    var tests = (await _testService.GetWithIncludeAsync(obj.TestId, new string[] { "ClassSchedule_Rosters", nameof(_test.TestStatus) }));
                    test.TestStatus = tests.TestStatus.Description;
                    test.isReleased = tests.ClassSchedule_Rosters.Count() > 0 ? true : false;
                    testWithCount.Add(test);
                }
            }
            return testWithCount;
        }

        public async System.Threading.Tasks.Task ActiveAsync(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Activate();
                    if (obj.IsPublished)
                    {
                        obj.TestStatusId = 2;
                    }
                    else
                    {
                        obj.TestStatusId = 1;
                    }
                    var validationResult = await _testService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task ChangeILA(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var test = await _testService.FindQuery(x => x.Id == id, true).FirstOrDefaultAsync();
                if (test == null)
                {
                    throw new BadHttpRequestException(message: _localizer["TestNotFoundException"]);
                }
                else
                {
                    var evalList = await _iLATraineeEvaluationDomainService.FindQuery(x => x.TestId == test.Id, true).ToListAsync();
                    foreach (var eval in evalList)
                    {
                        eval.ILAId = (int)options.ILAId;
                        var validationResult = await _iLATraineeEvaluationDomainService.UpdateAsync(eval);
                        if (!validationResult.IsValid)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task MarkAsDraft(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var test = await _testService.FindQuery(x => x.Id == id, true).FirstOrDefaultAsync();
                if (test == null)
                {
                    throw new BadHttpRequestException(message: _localizer["TestNotFoundException"]);
                }
                else
                {
                    test.TestStatusId = 4;
                    var validationResult = await _testService.UpdateAsync(test);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task MarkAsPublished(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var test = await _testService.FindQuery(x => x.Id == id, true).FirstOrDefaultAsync();
                if (test == null)
                {
                    throw new BadHttpRequestException(message: _localizer["TestNotFoundException"]);
                }
                else
                {
                    test.TestStatusId = 2;
                    test.IsPublished = true;
                    var validationResult = await _testService.UpdateAsync(test);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Delete();

                    var validationResult = await _testService.UpdateAsync(obj);
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

        public async System.Threading.Tasks.Task InActiveAsync(TestOptions options)
        {
            foreach (var id in options.TestIds)
            {
                var obj = await GetAsync(id);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Delete);

                if (result.Succeeded)
                {
                    obj.Deactivate();
                    obj.TestStatusId = 3;

                    var validationResult = await _testService.UpdateAsync(obj);
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

        public async Task<Test> UpdateAsync(int id, TestCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);

            //var testExists = (await _testService.FindAsync(x => x.Id != id && x.TestTitle == options.TestTitle)).FirstOrDefault() != null;

            //if (testExists)
            //{
            //    throw new UnauthorizedAccessException(message: _localizer["TestAlreadyExistsWithSameName"].Value);
            //}

            if (result.Succeeded)
            {
                obj.TestTitle = options.TestTitle;
                if (options.Mode == "publish")
                {
                    obj.TestStatusId = 2;
                }
                obj.RandomizeDistractors = options.RandomizeDistractors;
                obj.RandomizeQuestionsSequence = options.RandomizeQuestionsSequence;
                obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.ModifiedDate = DateTime.Now;

                var validationResult = await _testService.UpdateAsync(obj);
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

        public async Task<Test> UpdateTestItemSequenceAsync(int id, Test_Item_Link_LinkOptions options)
        {

            //var obj = await _testService.GetWithIncludeAsync(id, new string[] { nameof(_test.Test_Item_Links) });
            var test = await _testService.GetAsync(options.TestId);
            int seq = 1;
            test.RandomizeDistractors = options.RandomDistractor;
            test.RandomizeQuestionsSequence = options.ItemSeq;
            int[] testItemIds = options.ItemSeq ? ExtensionMethods.RandomizeArray(options.TestItemIds) : options.TestItemIds;
            foreach (var item in testItemIds)
            {
                //var testitem = await _testItemService.GetAsync(item);

                //if (testitem == null)
                //{
                //    throw new Exception(message: _localizer["TestItemNotFound"]);
                //}

                //if (test == null)
                //{
                //    throw new Exception(message: _localizer["TestNotFound"]);
                //}

                //var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
                //var testResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, test, AuthorizationOperations.Create);
                //var testItemResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testitem, AuthorizationOperations.Create);
                var itemLinks = (await _testItemLinkService.FindAsync(x => x.TestId == id && x.TestItemId == item)).FirstOrDefault();
                //var testItemLinkResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, itemLinks, AuthorizationOperations.Create);

                //obj.UnLinkTestItem(testitem);
                //var test_item_link = obj.LinkTestItem(testitem);
                var test_item_link = (await _testItemLinkService.FindAsync(x => x.TestId == id && x.TestItemId == item)).FirstOrDefault();
                test_item_link.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                test_item_link.ModifiedDate = DateTime.Now;

                test_item_link.Sequence = seq;
                seq++;


                await _testItemLinkService.UpdateAsync(test_item_link);
                


            }

            await _testService.UpdateAsync(test);
            return test;
        }

        public async Task<Test> CreateAsync(TestCreateOptions options)
        {
            //var obj = (await _testService.FindAsync(x => x.TestTitle.ToLower().Trim() == options.TestTitle.ToLower().Trim())).FirstOrDefault();
            //  if (obj == null)
            // {
            var obj = new Test(options.TestStatusId ?? 1, options.TestTitle);
            // }
            //else
            //  {
            //   throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            // }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, TestOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                obj.IsPublished = false;
                obj.EffectiveDate = options.EffectiveDate ?? DateTime.Now;
                var validationResult = await _testService.AddAsync(obj);
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

        public async Task<List<TestEOwithCountOptions>> GetLinkedEOs(int id)
        {
            List<TestEOwithCountOptions> eoList = new List<TestEOwithCountOptions>();
            var traineeEvaluation = (await _iLATraineeEvaluationDomainService.GetLinkedTestsByTestIdAsync(id)).FirstOrDefault();
            if (traineeEvaluation == null)
                throw new QTDServerException(_localizer["TestNotFound"].Value,false,HttpStatusCode.NotFound);
            var ilaEOLinks = (await _iLA_EOLinkService.GetILA_EnablingObjective_LinksByILAId(traineeEvaluation.ILAId)).ToList();
            var distinctEoIds = ilaEOLinks.Select(ie => ie.EnablingObjectiveId).Distinct().ToList();
            var testItems = await _testItem_Service.GetTestItemsByEOIdsAsync(distinctEoIds);
            var testItemCounts = testItems.Where(ti => ti.EOId != null).GroupBy(ti => ti.EOId).ToDictionary(g => g.Key.Value, g => g.Count());
            foreach (var eoLink in ilaEOLinks)
            {
                TestEOwithCountOptions eo = new TestEOwithCountOptions();
                eo.EOId = eoLink.EnablingObjectiveId;
                eo.Number = eoLink.EnablingObjective.FullNumber;
                eo.Type = "EO";
                eo.Description = eoLink.EnablingObjective.Description;
                eo.MaximumNumber = testItemCounts.TryGetValue(eoLink.EnablingObjectiveId, out var count) ? count : 0;
                eoList.Add(eo);
            }
            return eoList;
        }

        public async Task<List<RosterTestVM>> GetSpecificTestTypeForILAAsync(int classScheduleId, string type)
        {
            List<RosterTestVM> tests = new();
            var settings = await _classTestReleaseEmpSettingDomainService.FindQuery(x => x.ClassScheduleId == classScheduleId).FirstOrDefaultAsync();
            var classSchedule = await _classScheduleService.GetClassScheduleByIdAsync(classScheduleId);
            var ilaTraineeEvaluations = await _iLATraineeEvaluationDomainService.GetLinkedTestsAsync(classSchedule.ILAID.GetValueOrDefault());
            if (ilaTraineeEvaluations.Count > 0)
            {
                switch (type.ToLower().Trim())
                {
                    case "pretest":
                        var pretests = ilaTraineeEvaluations.Where(x => x.TestType?.Description.ToLower() == "pretest").Select(x => x.Test).ToList();
                        foreach (var pretest in pretests)
                        {
                            var rostPretest = new RosterTestVM();
                            rostPretest.Id = pretest.Id;
                            rostPretest.RetakeOrder = null;
                            rostPretest.TestTitle = pretest.TestTitle;
                            rostPretest.TestStatusId = pretest.TestStatusId;
                            tests.Add(rostPretest);
                        }
                        break;
                    case "cbt":
                        //tests = await _iLATraineeEvaluationDomainService.FindQueryWithIncludeAsync(x => x.ILAId == ilaId && x.TestTypeId == 4, new string[] { "Test" }).Select(m => m.Test).Distinct().ToListAsync();
                        break;
                    case "test":
                        var finalTests = ilaTraineeEvaluations.Where(x => x.TestType?.Description.ToLower() == "final test").Select(x => x.Test).ToList();
                        foreach (var finalTest in finalTests)
                        {
                            var rostTest = new RosterTestVM();
                            rostTest.Id = finalTest.Id;
                            rostTest.RetakeOrder = null;
                            rostTest.TestTitle = finalTest.TestTitle;
                            rostTest.TestStatusId = finalTest.TestStatusId;
                            tests.Add(rostTest);
                        }
                        break;
                    case "retake":
                        int retakeOrder = 0;
                        var retakeTests = ilaTraineeEvaluations.Where(x => x.TestType?.Description.ToLower() == "retake").Select(x => x.Test).ToList();
                        foreach (var retakTest in retakeTests)
                        {
                            var rostRetake = new RosterTestVM();
                            rostRetake.Id = retakTest.Id;
                            rostRetake.RetakeOrder = retakeOrder + 1;
                            rostRetake.TestTitle = retakTest.TestTitle;
                            rostRetake.TestStatusId = retakTest.TestStatusId;
                            tests.Add(rostRetake);
                            retakeOrder++;
                        }

                        break;
                }
            }
            return tests;
        }

        public async Task<List<EnablingObjective>> GetEOsLinkedtoILA(int id)
        {
            List<EnablingObjective> eoList = new List<EnablingObjective>();

            var test = (await _iLATraineeEvaluationDomainService.AllWithIncludeAsync(new string[] { nameof(_iLATraineeEvaluation.ILA) })).Where(x => x.TestId == id);
            var obj = (await _iLATraineeEvaluationDomainService.FindAsync(x => x.TestId == id)).FirstOrDefault();

            if (obj == null)
            {
                throw new UnauthorizedAccessException(message: _localizer["TestNotFound"].Value);
            }
            else
            {

                var ilaLinks = (await _iLA_EOLinkService.FindWithIncludeAsync(x=>x.ILAId==obj.ILAId,new string[] { nameof(_ilaEnablingObjective.EnablingObjective) }));

                foreach (var link in ilaLinks)
                {
                    eoList.Add(link.EnablingObjective);
                }
            }
            return eoList;
        }

        public async Task<List<TestItem>> FilterTestItems(TestItemFilter options)
        {
            List<TestItem> testItems = new List<TestItem>();

            if (options.EOId != null)
            {
                //Get all TestItems matching question-type criteria for the specified Enabling Objective
                testItems = (await _testItem_Service.FindAsync(x => options.TestItemTypeIds.Contains(x.TestItemTypeId) && options.TaxonomyIds.Contains(x.TaxonomyId) && x.EOId == options.EOId, true)).ToList();
            }
            else
            {
                //Get all TestItems matching question-type criteria from all Enabling Objectives linked to the Test's ILA, as no Enabling Objective was specified
                var ilaTraineeEvaluation = (await _iLATraineeEvaluationDomainService.FindAsync(x => x.TestId == options.TestId)).FirstOrDefault();
                var eos = await _iLA_EOLinkService.FindAsync(x => x.ILAId == ilaTraineeEvaluation.ILAId);

                foreach (var eo in eos)
                {
                    testItems.AddRange(await _testItem_Service.FindAsync(x => options.TestItemTypeIds.Contains(x.TestItemTypeId) && options.TaxonomyIds.Contains(x.TaxonomyId) && x.EOId == eo.EnablingObjectiveId, true));
                }
            }
            foreach (var item in testItems)
            {
                item.TaxonomyLevel = (await _taxonomyLevelService.FindAsync(x => x.Id == item.TaxonomyId, true)).FirstOrDefault();
                item.TestItemType = (await _testItemTypeService.FindAsync(x => x.Id == item.TestItemTypeId, true)).FirstOrDefault();
            }

            return testItems;
        }

        public async Task<List<TestItem>> GetUnlinkedQuestionsAsync(int id)
        {
            List<TestItem> testItems = new List<TestItem>();
            var testItemsLinks = await _testItemLinkService.FindQueryWithIncludeAsync(x => x.TestId == id, new string[] { nameof(_test_Item_Link.TestItem), nameof(_test_Item_Link.Test) }, true).ToListAsync();


            testItems = await _testItem_Service.AllQueryWithInclude(new string[] { nameof(_testItem.TestItemType), nameof(_testItem.TaxonomyLevel) }, true).ToListAsync();
            var link = await _testItemLinkService.FindQuery(x => x.TestId == id, true).Select(x => x.TestItemId).ToListAsync();
            foreach (var itemId in link)
            {
                testItems = testItems.Where(x => x.Id != itemId).ToList();
            }



            return testItems.OrderBy(o => o.Id).ToList();
        }

        public async Task<Test> LinkTestItem(int id, Test_Item_Link_LinkOptions options)
        {

            var obj = await _testService.GetWithIncludeAsync(id, new string[] { nameof(_test.Test_Item_Links) });
            //var test = await _testService.FindQuery(x => x.Id == options.TestId).FirstOrDefaultAsync();

            var testItemids = options.TestItemIds;

            if (options.ItemSeq)
            {
                testItemids = testItemids.Shuffle<int>().ToArray();
            }

            var seq = 1;
            for (int i = 0; i < testItemids.Length; i++)
            {
                var testitem = await _testItem_Service.FindQuery(x => x.Id == testItemids[i]).FirstOrDefaultAsync();

                if (testitem == null)
                {
                    throw new QTDServerException(_localizer["TestItemNotFound"]);
                }

                if (obj == null)
                {
                    throw new QTDServerException(_localizer["TestNotFound"]);
                }

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Create);
                //var testResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, test, AuthorizationOperations.Create);
                var testItemResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testitem, AuthorizationOperations.Create);

                if (result.Succeeded && testItemResult.Succeeded)
                {
                    obj.UnLinkTestItem(testitem);
                    obj.LinkTestItem(testitem);
                    var testValidResult = await _testService.UpdateAsync(obj);
                    var test_item_link = await _testItemLinkService.FindQuery(x => x.TestId == obj.Id && x.TestItemId == testitem.Id).FirstOrDefaultAsync();
                    test_item_link.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    test_item_link.CreatedDate = DateTime.Now;
                    test_item_link.Sequence = seq;
                    seq = seq + 1;
                    await _testItemLinkService.UpdateAsync(test_item_link);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }

            }

            obj = await _testService.GetAsync(id);
            return obj;
        }

        public async System.Threading.Tasks.Task UnLinkTestItem(int id, Test_Item_Link_LinkOptions options)
        {
            foreach (var itemId in options.TestItemIds)
            {
                var obj = await _testService.GetWithIncludeAsync(id, new string[] { nameof(_test.Test_Item_Links) });
                var testitem = await _testItemService.GetAsync(itemId);

                if (testitem == null)
                {
                    throw new QTDServerException(_localizer["TestItemNotFound"]);
                }

                if (obj == null)
                {
                    throw new QTDServerException(_localizer["TestNotFound"]);
                }

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Update);
                var testItemResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, testitem, AuthorizationOperations.Create);

                if (result.Succeeded && testItemResult.Succeeded)
                {
                    obj.UnLinkTestItem(testitem);

                    await _testService.UpdateAsync(obj);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
        }

        public async System.Threading.Tasks.Task UnlinkAllTestItemsAsync(int id)
        {
            var testItemLinks = await _testItemLinkService.FindQuery(x => x.TestId == id).ToListAsync();
            foreach (var link in testItemLinks)
            {
                var validationResult = await _testItemLinkService.DeleteAsync(link);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",", validationResult.Errors)]);
                }
            }
        }

        public async Task<List<TestItemLinkVM>> GetLinkedTestItemAsync(int id)
        {
            var testItemLinks = await _testItemLinkService.GetTestItemLinksByTestIdAsync(id);
            return testItemLinks.OrderBy(x=>x.Sequence).Select(t => new TestItemLinkVM(t.Id,t.TestItemId,t.TestItem.TestItemTypeId,t.TestItem.TaxonomyId,t.TestItem?.EOId,t.Sequence,t.TestItem.Number,t.TestItem?.Description,t.TestItem?.TestItemType?.Description,t.TestItem?.TaxonomyLevel?.Description)).ToList();
        }

        public async Task<TestStatsVM> GetTestStatsAsync()
        {
            var tests = await _testService.AllQuery().ToListAsync();
            TestStatsVM testStats = new TestStatsVM();
            testStats.PublishedTests = tests.Where(x => x.TestStatusId == 2).Count(); ;
            testStats.InDevelopmentTests = tests.Where(x => x.TestStatusId == 1).Count();
            testStats.InActiveTests = tests.Where(x => x.Active == false).Count();
            return testStats;
        }

        public async Task<List<Test>> GetTestActiveInactive(string option)
        {
            var rrList = new List<Test>();
            var testStatusId = await _testStatusService.FindQuery(x => x.Description == option).Select(x => x.Id).ToListAsync();

            switch (option.ToLower().Trim())
            {
                case "inactive":
                    rrList = await _testService.FindQuery(x => x.Active == false).Select(s => new Test
                    {
                        Id = s.Id,
                        TestTitle = s.TestTitle,
                    }).ToListAsync();
                    break;
                case "published":
                    rrList = await _testService.FindQuery(x => x.TestStatusId == testStatusId[0]).Select(s => new Test
                    {
                        Id = s.Id,
                        TestTitle = s.TestTitle,
                    }).ToListAsync();
                    break;
                case "in development":
                    rrList = await _testService.FindQuery(x => x.TestStatusId == testStatusId[0]).Select(s => new Test
                    {
                        Id = s.Id,
                        TestTitle = s.TestTitle,
                    }).ToListAsync();
                    break;

            }

            return rrList.OrderBy(x => x.Id).ToList();

        }

        public async Task<List<TestItem>> GetTestItemsForTestAsync(int id)
        {
            var testItemlinks = (await _testItemLinkService.FindQuery(x => x.TestId == id && !x.TestItem.Deleted).OrderBy(o => o.Sequence).ToListAsync());
            List<TestItem> testItems = new List<TestItem>();
            var allTestItems = testItemlinks.Select(x => x.TestItem);
            var testItemIds = testItemlinks.Select(s => s.TestItemId).ToArray();
            var test = await _testService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var random = test.RandomizeDistractors;
            var randomQuesSequence = test.RandomizeQuestionsSequence;
            testItemIds = randomQuesSequence ? ExtensionMethods.RandomizeArray(testItemIds) : testItemIds;
            foreach (var tId in testItemIds)
            {
                var testItem = await _testItem_Service.FindQueryWithIncludeAsync(x => x.Id == tId,
                                new string[] { nameof(_testItem.TaxonomyLevel),
                                               nameof(_testItem.TestItemType),
                                               nameof(_testItem.TestItemMCQs),
                                               nameof(_testItem.TestItemFillBlanks),
                                               nameof(_testItem.TestItemMatches),
                                               nameof(_testItem.TestItemShortAnswers),
                                               nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();
                if (random)
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => Guid.NewGuid()).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => Guid.NewGuid()).ToList();
                }
                else
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();
                }
                testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
                testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();
                testItems.Add(testItem);
            }

            testItems = testItems.Where(t => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, t, TestItemOperations.Read).Result.Succeeded).ToList();
            //if (testItemlinks.Any(a => a.Sequence == 0))
            //{
            //    testItems = testItems.OrderBy(x => x.Id).ToList();
            //}
            return testItems;
        }

        public async Task<List<TestItemVM>> GetTestItemsForTestVMAsync(int id)
        {
            var testItemlinks = (await _testItemLinkService.FindQuery(x => x.TestId == id && !x.TestItem.Deleted).OrderBy(o => o.Sequence).ToListAsync());
            List<TestItem> testItems = new List<TestItem>();
            List<TestItemVM> testItemVMs = new List<TestItemVM>();
            var allTestItems = testItemlinks.Select(x => x.TestItem);
            var testItemIds = testItemlinks.Select(s => s.TestItemId).ToArray();
            var test = await _testService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var random = test.RandomizeDistractors;
            var randomQuesSequence = test.RandomizeQuestionsSequence;
            testItemIds = randomQuesSequence ? ExtensionMethods.RandomizeArray(testItemIds) : testItemIds;
            foreach (var tId in testItemIds)
            {
                var testItem = await _testItem_Service.FindQueryWithIncludeAsync(x => x.Id == tId,
                                new string[] { nameof(_testItem.TaxonomyLevel),
                                               nameof(_testItem.TestItemType),
                                               nameof(_testItem.TestItemMCQs),
                                               nameof(_testItem.TestItemFillBlanks),
                                               nameof(_testItem.TestItemMatches),
                                               nameof(_testItem.TestItemShortAnswers),
                                               nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();
                if (random)
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => Guid.NewGuid()).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => Guid.NewGuid()).ToList();
                }
                else
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();
                }
                testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
                testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();
                testItemVMs.Add(TestItemToTestItemVM(testItem, false, random));
            }

            testItems = testItems.Where(t => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, t, TestItemOperations.Read).Result.Succeeded).ToList();
            if (testItemlinks.Any(a => a.Sequence == 0))
            {
                testItems = testItems.OrderBy(x => x.Id).ToList();
            }
            return testItemVMs;
        }

        public TestItemVM TestItemToTestItemVM(TestItem testItem, bool stripImages = false, bool randomizeDistractors = false)
        {
            List<TestItemTrueFalseVM> trueFalseVMs = new List<TestItemTrueFalseVM>();
            List<TestItemFillBlankVM> testItemFillBlankVMs = new List<TestItemFillBlankVM>();
            List<TestItemMatchVM> testItemMatchVMs = new List<TestItemMatchVM>();
            List<TestItemMCQVM> testItemMCQVMs = new List<TestItemMCQVM>();
            List<TestItemShortAnswerVM> testItemShortAnswerVM = new List<TestItemShortAnswerVM>();

            if (randomizeDistractors)
            {
                var matchChoices = testItem.TestItemMatches.Select(x => new { x.ChoiceDescription, x.CorrectValue,x.Number }).Cast<object>().ToList();
                var matchMatches = testItem.TestItemMatches.Select(x => new { x.MatchDescription, x.MatchValue }).Cast<object>().ToList();
                matchChoices = matchChoices.OrderBy(x => Guid.NewGuid()).ToList();
                matchMatches = matchMatches.OrderBy(x => Guid.NewGuid()).ToList();
                var matchCombined = matchMatches.Select((x, i) =>
                {
                    dynamic choice = i < matchChoices.Count ? matchChoices[i] : null;
                    dynamic match = x;
                    return new
                    {
                        MatchDescription = match.MatchDescription,
                        OriginalMatchValue = match.MatchValue,
                        ChoiceDescription = choice?.ChoiceDescription,
                        OriginalCorrectValue = choice?.CorrectValue,
                        Number = choice.Number
                    };
                }).Cast<object>().ToList();
                testItem.TestItemMatches.ToList().Select((x, i) =>
                {
                    dynamic combined = i < matchCombined.Count ? matchCombined[i] : null;
                    return new TestItemMatchVM(
                        x.Id,
                        x.TestItemId,
                        combined?.ChoiceDescription ?? "",
                        combined?.MatchDescription ?? "",
                        x.MatchValue,
                        combined.Number,
                        null,
                        combined?.OriginalCorrectValue,
                        combined?.OriginalMatchValue
                    );
                }).OrderBy(m=>m.MatchValue).ToList().ForEach(vm => testItemMatchVMs.Add(vm));
            }
            else
            {
                testItem.TestItemMatches.ToList().ForEach(x => { testItemMatchVMs.Add(new TestItemMatchVM(x.Id, x.TestItemId, x.ChoiceDescription, x.MatchDescription, x.MatchValue, x.Number, null, x.CorrectValue, x.MatchValue)); });
            }

            testItem.TestItemTrueFalses.ToList().ForEach(x => { trueFalseVMs.Add(new TestItemTrueFalseVM(x.Id, x.TestItemId, x.Choices, false)); });
            testItem.TestItemFillBlanks.ToList().ForEach(x => { testItemFillBlankVMs.Add(new TestItemFillBlankVM(x.Id, x.TestItemId, x.CorrectIndex, "")); });
            testItem.TestItemMCQs.ToList().ForEach(x => { testItemMCQVMs.Add(new TestItemMCQVM(x.Id, x.TestItemId, x.ChoiceDescription, false, x.Number)); });
            string description = testItem.Description;
            if (stripImages)
            {
                description = Regex.Replace(description, "<img[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            }
            else if (testItem.TestItemTypeId == 2)
            {
                description = ReplaceCharactersWithNbsp(description);
            }
            var testItemVM = new TestItemVM(testItem.Id, testItem.TestItemTypeId, description, testItem.Number, testItem.TestItemType.Description, trueFalseVMs, testItemFillBlankVMs.OrderBy(x => x.CorrectIndex).ToList(), testItemMatchVMs, testItemMCQVMs, testItemShortAnswerVM);
            return testItemVM;
        }
        static string ReplaceCharactersWithNbsp(string input)
        {
            // Replace each character with &nbsp; inside <u> tags
            string pattern = "<u>(.*?)</u>";
            string replacedString = System.Text.RegularExpressions.Regex.Replace(input, pattern, match =>
            {
                string contentInsideUTags = match.Groups[1].Value;
                string nbsp = string.Join(" ", contentInsideUTags.Select(c => "&nbsp;"));
                return "<u>" + nbsp + "</u>";
            });

            return replacedString;
        }
        public async Task<List<TestItem>> GetTestItemsForCopyModeAsync(TestItemCopyOptions option)
        {
            //var testItemlinks = (await _testItemLinkService.FindQuery(x => option.TestItemIds.Contains(x.TestItemId)).OrderBy(o => o.Sequence).ToListAsync());
            List<TestItem> testItems = new List<TestItem>();
            //var allTestItems = testItemlinks.Select(x => x.TestItem);
            //var testItemIds = testItemlinks.Select(s => s.TestItemId).ToList();
            var random = await _testService.FindQuery(x => x.Id == option.TestId).Select(s => s.RandomizeDistractors).FirstOrDefaultAsync();
            foreach (var tId in option.TestItemIds)
            {
                var testItem = await _testItem_Service.FindQueryWithIncludeAsync(x => x.Id == tId,
                                new string[] { nameof(_testItem.TaxonomyLevel),
                                               nameof(_testItem.TestItemType),
                                               nameof(_testItem.TestItemMCQs),
                                               nameof(_testItem.TestItemFillBlanks),
                                               nameof(_testItem.TestItemMatches),
                                               nameof(_testItem.TestItemShortAnswers),
                                               nameof(_testItem.TestItemTrueFalses) }, true).FirstOrDefaultAsync();
                if (random)
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => Guid.NewGuid()).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => Guid.NewGuid()).ToList();
                }
                else
                {
                    testItem.TestItemMCQs = testItem.TestItemMCQs.OrderBy(x => x.Number).ToList();
                    testItem.TestItemMatches = testItem.TestItemMatches.OrderBy(x => x.Number).ToList();
                }
                testItem.TestItemShortAnswers = testItem.TestItemShortAnswers.OrderBy(x => x.Number).ToList();
                testItem.TestItemFillBlanks = testItem.TestItemFillBlanks.OrderBy(x => x.CorrectIndex).ToList();
                testItems.Add(testItem);
            }

            return testItems;
        }

        public async Task<bool> CheckIfRandomItems(int id)
        {
            var isRandom = (await _testItemLinkService.FindQuery(x => x.TestId == id).ToListAsync()).Any(a => a.Sequence == 0);
            return isRandom;
        }

        public async Task<List<RetakeStatusVM>> GetRetakeStatusesAsync(int empId, int classId)
        {
            List<RetakeStatusVM> retakeStatuses = new List<RetakeStatusVM>();
            var cs = await _classScheduleService.FindQuery(x => x.Id == classId).FirstOrDefaultAsync();
            var emp = await _empService.FindQuery(x => x.Id == empId).FirstOrDefaultAsync();
            if (emp == null)
            {
                throw new BadHttpRequestException(message: _localizer["Employee Not Found"]);
            }
            var person = await _personService.FindQuery(x => x.Id == emp.PersonId).FirstOrDefaultAsync();
            if (person == null)
            {
                throw new BadHttpRequestException(message: _localizer["Person Not Found"]);
            }
            if (cs == null)
            {
                throw new BadHttpRequestException(message: _localizer["Class Schedule Not Found"]);
            }
            else
            {
                var ilaId = await _ilaService.FindQuery(x => x.Id == cs.ILAID).Select(s => s.Id).FirstOrDefaultAsync();
                if (ilaId == 0)
                {
                    throw new BadHttpRequestException(message: _localizer["Ila Not found"]);
                }
                else
                {
                    var setting = (await _classTestReleaseEmpSettingDomainService.FindWithIncludeAsync(x => x.ClassScheduleId == classId, new string[] { "ClassSchedule_TestReleaseEMPSetting_RetakeLinks" })).FirstOrDefault();
                    if (setting != null)
                    {
                        var links = setting.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.ToList();
                        foreach (var link in links)
                        {
                            var retakeStatus = new RetakeStatusVM();
                            var roster = await _classSchedule_RosterService.FindQuery(x => x.ClassScheduleId == classId && x.EmpId == empId && x.TestId == link.RetakeTestId).FirstOrDefaultAsync();
                            if (roster == null)
                            {
                                var newTest = await _testService.FindQuery(x => x.Id == link.RetakeTestId).FirstOrDefaultAsync();
                                if (newTest != null)
                                {
                                    retakeStatus.EmployeeEmail = person.Username;
                                    retakeStatus.EmployeeName = person.FirstName + " " + person.LastName;
                                    retakeStatus.EmployeeImage = person.Image;
                                    retakeStatus.TestTitle = newTest.TestTitle;
                                    retakeStatus.Status = "Not Started";
                                    retakeStatuses.Add(retakeStatus);
                                }

                            }
                            else
                            {
                                var test = await _testService.FindQuery(x => x.Id == roster.TestId).FirstOrDefaultAsync();
                                if (test != null)
                                {
                                    var status = await _classSchedule_rosterStatusService.FindQuery(x => x.Id == roster.StatusId).FirstOrDefaultAsync();
                                    if (status != null)
                                    {
                                        retakeStatus.EmployeeEmail = person.Username;
                                        retakeStatus.EmployeeName = person.FirstName + " " + person.LastName;
                                        retakeStatus.EmployeeImage = person.Image;
                                        retakeStatus.TestTitle = test.TestTitle;
                                        retakeStatus.Status = status.Name;
                                        retakeStatuses.Add(retakeStatus);
                                    }
                                }
                            }
                        }
                        return retakeStatuses;
                    }
                    else
                    {
                        return retakeStatuses;
                    }
                }
            }
        }

        public async Task<List<ILA_EnablingObjective_Link>> GetEOsLinkedToTestsILAAsync(int testId)
        {
            var ilaId = await _iLATraineeEvaluationDomainService.FindQuery(x => x.TestId == testId).Select(s => s.ILAId).FirstOrDefaultAsync();
            if (ilaId != 0)
            {
                var eoLinks = await _iLA_EOLinkService.FindQuery(x => x.ILAId == ilaId).ToListAsync();
                //eoLinks = eoLinks.Where(w => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, w, AuthorizationOperations.Read).Result.Succeeded).ToList();
                return eoLinks;
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["ILATestLinkNotFoundException"]);
            }
        }

        public async System.Threading.Tasks.Task ReorderTestItemsAsync(int id, ReorderTestItemOptions options)
        {
            var testItemLinks = await _testItemLinkService.FindQuery(x => x.TestId == id).ToListAsync();
            foreach (var link in testItemLinks)
            {
                var item = options.TestItemOrder.Find(x => x.ItemId == link.TestItemId);
                if (item != null)
                {
                    link.Sequence = item.Order;
                    await _testItemLinkService.UpdateAsync(link);
                }
            }
        }
    }
}
