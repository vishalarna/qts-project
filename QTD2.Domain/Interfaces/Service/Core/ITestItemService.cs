using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITestItemService : Common.IService<TestItem>
    {
        System.Threading.Tasks.Task<IEnumerable<TestItem>> GetTestItemsByEnablingObjectivesAsync(List<int> list);
        System.Threading.Tasks.Task<IEnumerable<TestItem>> GetAllTestItemsAsync();
        System.Threading.Tasks.Task<List<TestItem>> GetTestItemsByIdandTaxonomyIdAsync(List<int> ids, List<int>taxonomyIds,string testitemstatus);
        System.Threading.Tasks.Task<List<TestItem>> GetTestItemsByEOIdsAsync(List<int> eoIds);
    }
}
