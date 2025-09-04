using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_Roster_TimeRecordService : Common.IService<ClassSchedule_Roster_TimeRecord>
    {
        Task<List<ClassSchedule_Roster_TimeRecord>> GetClassScheduleRosterTimeRecordByRosterId(int rosterId);
    }
}
