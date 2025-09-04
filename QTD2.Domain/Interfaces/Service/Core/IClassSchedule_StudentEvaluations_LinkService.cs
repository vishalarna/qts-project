using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_StudentEvaluations_LinkService : IService<ClassSchedule_StudentEvaluations_Link>
    {
        public System.Threading.Tasks.Task<List<ClassSchedule_StudentEvaluations_Link>> GetClassScheduleLinksByIlaIdEvalIdAsync(List<int> ilaIds, List<int> evalIds,List<DateTime>dateRange);
        public System.Threading.Tasks.Task<List<ClassSchedule_StudentEvaluations_Link>> GetLinksByClassScheduleAndEvaluationIdsAsync(List<int> classScheduleIDs, List<int> studentEvalIds);
    }
}

