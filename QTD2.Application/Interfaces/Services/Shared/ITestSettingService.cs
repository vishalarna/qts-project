using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TestSetting;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestSettingService
    {
        public Task<List<TestSetting>> GetAsync();

        public Task<TestSetting> GetAsync(int id);

        public Task<TestSetting> CreateAync(TestSettingCreateOptions options);

        public Task<TestSetting> UpdateAsync(int id, TestSettingUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeactivateAsync(int id);

        public System.Threading.Tasks.Task ActivateAsync(int id);
    }
}
