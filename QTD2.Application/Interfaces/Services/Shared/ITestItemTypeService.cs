using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemTypeService
    {
        public Task<List<TestItemType>> GetAsync();

        public Task<TestItemType> GetAsync(int id);

        public Task<TestItemType> CreateAsync(TestItemTypeCreateOptions options);

        public Task<TestItemType> UpdateAsync(int id, TestItemTypeUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
