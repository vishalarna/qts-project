using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA_TestRelease_EMPSettings;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITestReleaseEmpSettingsService
    {
        public Task<List<TestReleaseEMPSettings>> GetAsync();

        public Task<TestReleaseEMPSettings> GetAsync(int id);

        public Task<TestReleaseEMPSettings> CreateAsync(ILATestReleaseEmpSettingsCreateOptions options);


        public Task<TestReleaseEMPSettings> UpdateAsync(int id, ILATestReleaseEmpSettingsCreateOptions options);

        public System.Threading.Tasks.Task<TestReleaseEMPSettings> DeleteAsync(int id);

        public Task<TestReleaseEMPSettings> GetTTestReleaseEMPSettingsForILAAsync(int ilaId);
    }
}
