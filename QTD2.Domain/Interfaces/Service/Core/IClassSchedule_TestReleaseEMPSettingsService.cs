using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_TestReleaseEMPSettingsService : Common.IService<ClassSchedule_TestReleaseEMPSetting>
    {
        public Task<ClassSchedule_TestReleaseEMPSetting> GetTestSettingsByClassId(int id);
        public Task<List<ClassSchedule_TestReleaseEMPSetting>> GetClassScheduleTestReleaseSettingsByTestIdAsync(int testId);
    }
}
