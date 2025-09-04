using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class ClassSchedule_TestReleaseEMPSettingsService : Common.Service<ClassSchedule_TestReleaseEMPSetting>, IClassSchedule_TestReleaseEMPSettingsService
    {
        public ClassSchedule_TestReleaseEMPSettingsService(IClassSchedule_TestReleaseEMPSettingsRepository repository, IClassSchedule_TestReleaseEMPSettingsValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ClassSchedule_TestReleaseEMPSetting> GetTestSettingsByClassId(int id)
        {
            return (await FindWithIncludeAsync(x => x.ClassScheduleId == id, new string[]{ "ClassSchedule_TestReleaseEMPSetting_RetakeLinks" })).FirstOrDefault();
        }

        public async Task<List<ClassSchedule_TestReleaseEMPSetting>> GetClassScheduleTestReleaseSettingsByTestIdAsync(int testId)
        {
            return (await FindWithIncludeAsync(x => x.FinalTestId == testId || x.PreTestId == testId, new[] { "ClassSchedule" })).ToList();
        }

    }

}