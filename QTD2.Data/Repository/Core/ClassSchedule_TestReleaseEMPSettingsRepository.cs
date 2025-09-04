using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_TestReleaseEMPSettingsRepository : Common.Repository<ClassSchedule_TestReleaseEMPSetting>, IClassSchedule_TestReleaseEMPSettingsRepository
    {
        public ClassSchedule_TestReleaseEMPSettingsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}