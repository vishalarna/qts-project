using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TestService : Common.Service<Test>, ITestService
    {
        public TestService(ITestRepository repository, ITestValidation validation)
            : base(repository, validation)
        {

        }

        public async Task<List<Test>> GetTestSpecificationsAsync(IEnumerable<int> testIds, bool includeTestStatistics)
        {
            var data = await FindWithIncludeAsync(r => testIds.Contains(r.Id), new[] {
                "TestStatus",
                "Test_Item_Links",
                "ClassSchedule_Rosters.Responses.Selections",
                "Test_Item_Links.TestItem.TaxonomyLevel",
                "ILATraineeEvaluations.ILA.Provider",
                "ILATraineeEvaluations.TestType",
                "Test_Item_Links.TestItem.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category",
                "Test_Item_Links.TestItem.TestItemTrueFalses",
                "Test_Item_Links.TestItem.TestItemFillBlanks",
                "Test_Item_Links.TestItem.TestItemMatches",
                "Test_Item_Links.TestItem.TestItemMCQs",
                "Test_Item_Links.TestItem.TestItemShortAnswers"});
            if (includeTestStatistics)
            {
                //data = data.includes(new[] { "Test_Item_Links.TestItem.TestItemTrueFalses" });
            }

            return data.ToList();
        }

        public async Task<List<Test>> GetMinimalTests()
        {
            var tests = await AllQuery().Select(s => new Test
            {
                Id = s.Id,
                Deleted = s.Deleted,
                Active = s.Active,
                IsPublished = s.IsPublished,
                TestStatusId = s.TestStatusId,
                TestTitle = s.TestTitle
            }).ToListAsync();

            return tests;
        }

        public async Task<List<Test>> GetMinimalTestsByTestIds(List<int> testIds)
        {
            var tests = (await FindAsync(x => testIds.Contains(x.Id))).Select(s => new Test
            {
                Id = s.Id,
                Deleted = s.Deleted,
                Active = s.Active,
                IsPublished = s.IsPublished,
                TestStatusId = s.TestStatusId,
                TestTitle = s.TestTitle
            }).ToList();

            return tests;
        }

        public async Task<List<Test>> GetTestsForEMPTestStatistics(List<int> testIds)
        {
            List<Expression<Func<Test, bool>>> predicates = new List<Expression<Func<Test, bool>>>();
            predicates.Add(t => testIds.Contains(t.Id));
            var tests = (await FindWithIncludeAsync(predicates, new string[] {
                "Test_Item_Links.TestItem.TestItemType",
                "Test_Item_Links.TestItem.TaxonomyLevel",
                "Test_Item_Links.TestItem.EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category",
                "Test_Item_Links.TestItem.TestItemTrueFalses",
                "Test_Item_Links.TestItem.TestItemFillBlanks",
                "Test_Item_Links.TestItem.TestItemMatches",
                "Test_Item_Links.TestItem.TestItemMCQs",
                "Test_Item_Links.TestItem.TestItemShortAnswers"
             }, true)).ToList();

            return tests.ToList();
        }

        public Task<Test> GetFullTestAsync(int testId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Test>> GetForPretestAndFinalTestComparison(List<int> testIds)
        {
            List<Expression<Func<Test, bool>>> predicates = new List<Expression<Func<Test, bool>>>();
            predicates.Add(t => testIds.Contains(t.Id));
            var tests = (await FindWithIncludeAsync(predicates, new string[] {
                "Test_Item_Links.TestItem"
             }, true)).ToList();

            return tests.ToList();
        }

        public async Task<List<Test>> GetTestSpecificationsByTestIdsAsync(IEnumerable<int> testIds, bool includeTestStatistics)
        {
            List<Expression<Func<Test, bool>>> predicates = new List<Expression<Func<Test, bool>>>();

            predicates.Add(r => testIds.Contains(r.Id));

           
            var data = await FindWithIncludeAsync(predicates, new[] {
                "TestStatus",
                "Test_Item_Links",
                "ClassSchedule_Rosters.Responses.Selections",
                "ILATraineeEvaluations.ILA.Provider",
                "ILATraineeEvaluations.TestType"
                },true);
            if (includeTestStatistics)
            {
                //data = data.includes(new[] { "Test_Item_Links.TestItem.TestItemTrueFalses" });
            }

            return data.ToList();
        }
        public async Task<List<Test>> GetTestsByTestTypeAsync(string testType)
        {
            List<Expression<Func<Test, bool>>> predicates = new List<Expression<Func<Test, bool>>>();
            predicates.Add(t => t.ILATraineeEvaluations.Any(y => y.TestType.Description == testType));
            return (await FindWithIncludeAsync(predicates, new[] { "ILATraineeEvaluations.ILA" },true)).ToList();
        }
    }
}
