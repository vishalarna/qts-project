using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_Evaluator_LinksService : Common.IService<ClassSchedule_Evaluator_Link>
    {
        public System.Threading.Tasks.Task<List<ClassSchedule_Evaluator_Link>> GetClassScheduleTQEvaluators(int classScheduleId);
    }
}
