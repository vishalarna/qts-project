using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestTypeService
    {
        public Task<List<TestType>> GetAsync();

        public Task<TestType> GetAsync(int id);

        public Task<TestType> CreateAsync(TestTypeCreateOptions options);

        public Task<TestType> UpdateAsync(int id, TestTypeUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
