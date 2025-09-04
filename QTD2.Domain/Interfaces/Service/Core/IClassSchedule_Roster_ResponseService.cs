using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_Roster_ResponseService : Common.IService<ClassSchedule_Roster_Response>
    {
        Task<ClassSchedule_Roster_Response> GetByQuestionAndClassScheduleRoster(int questionId, int rosterId);
        Task<List<ClassSchedule_Roster_Response>> GetWithSelectionsByClassScheduleRosterIdsAsync(List<int> rosterIds);
    }

}
