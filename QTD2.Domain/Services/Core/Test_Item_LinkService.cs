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
    public class Test_Item_LinkService : Common.Service<Test_Item_Link>, ITest_Item_LinkService
    {
        public Test_Item_LinkService(ITest_Item_LinkRepository repository, ITest_Item_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<Test_Item_Link>> GetAllTestSpecificationAsync(IEnumerable<int> testIds)
        {
            List<Expression<Func<Test_Item_Link, bool>>> predicates = new List<Expression<Func<Test_Item_Link, bool>>>();

                predicates.Add(r => testIds.Contains(r.TestId));

            var test_Item_Links = await FindWithIncludeAsync(predicates, new[] {
                "TestItem.TaxonomyLevel",
                "TestItem.EnablingObjective",
                "TestItem.TestItemTrueFalses",
                "TestItem.TestItemFillBlanks",
                "TestItem.TestItemMatches",
                "TestItem.TestItemMCQs",
                "TestItem.TestItemShortAnswers"
                },
                true);

            return test_Item_Links.ToList();
        }

        public async Task<List<Test_Item_Link>> GetTestItemLinksByTestIdAsync(int testId)
        {
            List<Expression<Func<Test_Item_Link, bool>>> predicates = new List<Expression<Func<Test_Item_Link, bool>>>();
            predicates.Add(x=>x.TestId == testId);
            return (await FindWithIncludeAsync(predicates, new[] { "TestItem.TaxonomyLevel", "TestItem.TestItemType" }, true)).ToList();
        }
    }
}
