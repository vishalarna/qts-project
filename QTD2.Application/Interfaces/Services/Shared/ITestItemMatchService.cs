using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemMatch;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemMatchService
    {
        public Task<List<TestItemMatch>> GetAsync();

        public Task<TestItemMatch> GetAsync(int id);

        public List<TestItemMatch> GetByItemIdAsync(int id);

        public Task<TestItemMatch> CreateAsync(TestItemMatchCreateOptions options);

        public Task<TestItemMatch> UpdateAsync(int id, TestItemMatchUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeleteWithItemId(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
