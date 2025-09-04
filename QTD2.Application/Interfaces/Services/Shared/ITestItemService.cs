using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItem;
using QTD2.Infrastructure.Model.TestItemTrueFalse;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemService
    {
        public Task<List<TestItem>> GetAsync();

        public Task<TestItem> GetAsync(int id);

        public Task<int> GetTestItemNumberAsync();

        public Task<List<TestItem>> GetTestItemWithEOAsync(int eoId);

        public Task<List<TestItemEoVM>> GetTestItemsByEOIdsAsync(TestItemByEoOptions options);

        public Task<List<TestItem>> GetWithFilterAsync(string option,int id);

        public Task<TestItem> CreateAsync(TestItemCreateOptions options);

        public Task<TestItem> UpdateAsync(int id, TestItemCreateOptions options);

        public Task<TestItem> UpdateDescriptionAndEoId(int id, TestItemUpdateOptions options);

        public System.Threading.Tasks.Task ChangeEOForItemAsync(int id, TestItemChangeOptions opions);

        public System.Threading.Tasks.Task DeleteAsync(TestItemOptions options);

        public System.Threading.Tasks.Task ActiveAsync(TestItemOptions options);

        public System.Threading.Tasks.Task InActiveAsync(TestItemOptions options);

        public Task<TestItemStatsVM> GetTestItemStats();

        public Task<TestItem> GetItemWithDataAsync(int id);

        public Task<string> GetTestItemType(int typeId);

        public Task<List<TestItem>> GetTestItemList(string option);

    }
}
