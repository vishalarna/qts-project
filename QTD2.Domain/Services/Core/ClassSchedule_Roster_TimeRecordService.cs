using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_Roster_TimeRecordService : Common.Service<ClassSchedule_Roster_TimeRecord>,
                            IClassSchedule_Roster_TimeRecordService
    {
        public ClassSchedule_Roster_TimeRecordService(IClassSchedule_Roster_TimeRecordRepository repository, IClassSchedule_Roster_TimeRecordValidation
            validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ClassSchedule_Roster_TimeRecord>> GetClassScheduleRosterTimeRecordByRosterId(int rosterId)
        {
            var RosterTimeRecords = (await FindWithIncludeAsync(x => x.ClassSchedule_RosterId == rosterId,new string[] { "ClassSchedule_Roster" })).ToList();
            return RosterTimeRecords;
        }
    }
}