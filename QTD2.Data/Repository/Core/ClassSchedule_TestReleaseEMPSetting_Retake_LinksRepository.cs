using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository : Common.Repository<ClassSchedule_TestReleaseEMPSetting_Retake_Link>, IClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository
    {
        public ClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository(QTDContext context)
            : base(context)
        {
        }
    }
}