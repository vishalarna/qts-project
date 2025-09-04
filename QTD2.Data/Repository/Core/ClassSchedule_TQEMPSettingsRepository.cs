using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_TQEMPSettingsRepository : Common.Repository<ClassSchedule_TQEMPSetting>, IClassSchedule_TQEMPSettingsRepository
    {
        public ClassSchedule_TQEMPSettingsRepository(QTDContext context)
            : base(context)
        {
        }
    }
}