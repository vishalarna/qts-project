using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TestReleaseEMPSettingsService : Common.Service<TestReleaseEMPSettings>, ITestReleaseEMPSettingsService
    {
        public TestReleaseEMPSettingsService(ITestReleaseEMPSettingsRepository repository, ITestReleaseEMPSettingsValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<TestReleaseEMPSettings> GetTestReleaseEmpSettingByIla(ClassSchedule_Roster classRoster)
        {
            var testRelease = FindQuery(x => x.ILAId == classRoster.ClassSchedule.ILAID, false).FirstOrDefault();
            return testRelease;
        }

        public async Task<List<TestReleaseEMPSettings>> GetTestReleaseEmpSettingByTestIdAsync(int testId)
        {
            return (await FindAsync(x=>x.FinalTestId == testId || x.PreTestId == testId)).ToList();
        }

    }
}
