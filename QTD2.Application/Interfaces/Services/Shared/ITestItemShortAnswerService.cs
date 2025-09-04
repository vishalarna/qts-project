using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestItemShortAnswer;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestItemShortAnswerService
    {
        public Task<List<TestItemShortAnswer>> GetAsync();

        public Task<TestItemShortAnswer> GetAsync(int id);

        public List<TestItemShortAnswer> GetShortAnswersByIdAsync(int id);

        public Task<TestItemShortAnswer> CreateAsync(TestItemShortAnswerCreateOptions options);

        public Task<TestItemShortAnswer> UpdateAsync(int id, TestItemShortAnswerUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeleteWithItemId(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
