using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemTrueFalse;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemTrueFalseService
    {
        public Task<List<TestItemTrueFalse>> GetAsync();

        public Task<TestItemTrueFalse> GetAsync(int id);

        public List<TestItemTrueFalse> GetByTestItemIdAsync(int id);

        public Task<TestItemTrueFalse> CreateAsync(TestItemTrueFalseCreateOptions options);

        public Task<TestItemTrueFalse> UpdateAsync(int id, TestItemTrueFalseUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeleteWithItemId(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
