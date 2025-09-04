using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITask_EnablingObjective_LinkService : Common.IService<Entities.Core.Task_EnablingObjective_Link>
    {
        public System.Threading.Tasks.Task<List<Task_EnablingObjective_Link>> GetTaskEnablingObjectiveLinksByTaskIdAsync(int taskId);
        public System.Threading.Tasks.Task<List<Task_EnablingObjective_Link>> GetTaskEnablingObjectiveLinksByEOIdAsync(int eoId);
    }
}
