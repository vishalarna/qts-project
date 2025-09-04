using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ClassSchedule_Roster_ResponseService : Common.Service<ClassSchedule_Roster_Response>, IClassSchedule_Roster_ResponseService
    {
        public ClassSchedule_Roster_ResponseService(IClassSchedule_Roster_ResponseRepository repository, IClassSchedule_Roster_ResponseValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ClassSchedule_Roster_Response> GetByQuestionAndClassScheduleRoster(int questionId, int rosterId)
        {
            var q = await FindWithIncludeAsync
                (r => r.TestItemId == questionId && r.ClassScheduleRosterId == rosterId, new string[] { "Selections", "ClassSchedule_Roster", "TestItem" });

            return q.FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Roster_Response>> GetWithSelectionsByClassScheduleRosterIdsAsync(List<int> rosterIds)
        {
            return (await FindWithIncludeAsync(r => rosterIds.Contains(r.ClassScheduleRosterId), new string[] { "Selections" })).ToList();
        }
    }
}
