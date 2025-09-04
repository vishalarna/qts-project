using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_TestReleaseEMPSetting_Retake_LinksService : Common.IService<ClassSchedule_TestReleaseEMPSetting_Retake_Link>
    {
        public Task<List<ClassSchedule_TestReleaseEMPSetting_Retake_Link>> GetClassScheduleTestReleaseEmpSettingRetakeLinksByTestIdAsync(int testId);
    }
}
