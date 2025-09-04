using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TestItemService : Common.Service<TestItem>, ITestItemService
    {
        public TestItemService(ITestItemRepository repository, ITestItemValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<IEnumerable<TestItem>> GetAllTestItemsAsync()
        {
            return await AllWithIncludeAsync(new string[] {
               "TestItemTrueFalses",
                "TestItemFillBlanks",
                "TestItemMatches",
                "TestItemMCQs",
                "TestItemShortAnswers",
                "EnablingObjective"
               });
        }

        public async Task<IEnumerable<TestItem>> GetTestItemsByEnablingObjectivesAsync(List<int> list)
        {
            return await AllWithIncludeAsync(new string[] {
               "TestItemTrueFalses",
                "TestItemFillBlanks",
                "TestItemMatches",
                "TestItemMCQs",
                "TestItemShortAnswers",
                "EnablingObjective"
               });
        }

        public async Task<List<TestItem>> GetTestItemsByIdandTaxonomyIdAsync(List<int> ids, List<int> taxonomyIds, string testitemstatus)
        {
            List<Expression<Func<TestItem, bool>>> predicates = new List<Expression<Func<TestItem, bool>>>();
            predicates.Add(r => ids.Contains(r.TestItemTypeId));
            predicates.Add(r => taxonomyIds.Contains(r.TaxonomyId));
            if (testitemstatus == "Active")
            {
                predicates.Add(r => r.Active);
            }
            if (testitemstatus == "Inactive")
            {
                predicates.Add(r => !r.Active);
            }
            return (await FindWithIncludeAsync(predicates, new string[] { "TestItemType", "TaxonomyLevel", "Test_Item_Links", "EnablingObjective.EnablingObjective_Topic.EnablingObjectives_SubCategory.EnablingObjectives_Category", "EnablingObjective.EnablingObjective_SubCategory.EnablingObjectives_Category" })).ToList();
        }

        public async Task<List<TestItem>> GetTestItemsByEOIdsAsync(List<int> eoIds)
        {
            return (await FindAsync(x => eoIds.Contains(x.EOId.Value))).ToList();
        }
    }
}
