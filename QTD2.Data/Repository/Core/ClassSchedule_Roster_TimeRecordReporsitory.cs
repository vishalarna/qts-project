using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class ClassSchedule_Roster_TimeRecordReporsitory : Common.Repository<ClassSchedule_Roster_TimeRecord>, IClassSchedule_Roster_TimeRecordRepository
    {
        public ClassSchedule_Roster_TimeRecordReporsitory(QTDContext context)
            : base(context)
        {
        }
    }
}
