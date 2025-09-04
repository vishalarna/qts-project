using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestStatus;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestStatusService
    {
        public Task<List<TestStatus>> GetAsync();

        public Task<TestStatus> GetAsync(int id);

        public Task<TestStatus> CreateAsync(TestStatusCreateOptions options);

        public Task<TestStatus> UpdateAsync(int id, TestStatusUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
