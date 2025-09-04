using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemFillBlank;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemFillBlankService
    {
        public Task<List<TestItemFillBlank>> GetAsync();

        public Task<TestItemFillBlank> GetAsync(int id);

        public Task<TestItemFillBlank> CreateAsync(TestItemFillBlankCreateOptions options);

        public Task<List<TestItemFillBlank>> GetFIBByTestItemId(int id);

        public Task<TestItemFillBlank> UpdateAsync(int id, TestItemFillBlankUpdateOptions options);

        public System.Threading.Tasks.Task DeleteWithItemId(int id);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
