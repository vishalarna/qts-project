using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemMCQ;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemMCQService
    {
        public Task<List<TestItemMCQ>> GetAsync();

        public Task<TestItemMCQ> GetAsync(int id);

        public List<TestItemMCQ> GetByTestItemIdAsync(int id);

        public Task<TestItemMCQ> CreateAsync(TestItemMCQCreateOptions options);

        public Task<TestItemMCQ> UpdateAsync(int id, TestItemMCQUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeleteWithItemId(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
