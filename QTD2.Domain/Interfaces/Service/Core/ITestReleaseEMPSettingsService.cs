using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITestReleaseEMPSettingsService : Common.IService<TestReleaseEMPSettings>
    {
        public Task<TestReleaseEMPSettings> GetTestReleaseEmpSettingByIla(ClassSchedule_Roster classRoster);
        public Task<List<TestReleaseEMPSettings>> GetTestReleaseEmpSettingByTestIdAsync(int testId);
    }
}
