using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class Task_EnablingObjective_LinkService : Common.Service<Entities.Core.Task_EnablingObjective_Link>, ITask_EnablingObjective_LinkService
    {
        public Task_EnablingObjective_LinkService(ITask_EnablingObjective_LinkRepository task_EnablingObjective_LinkRepository, ITask_EnablingObjective_LinkValidation task_EnablingObjective_LinkValidation)
            : base(task_EnablingObjective_LinkRepository, task_EnablingObjective_LinkValidation)
        {
        }

        public async Task<List<Task_EnablingObjective_Link>> GetTaskEnablingObjectiveLinksByTaskIdAsync(int taskId)
        {
            var task_EnablingObjective_Links = await FindAsync(r => r.TaskId == taskId);
            return task_EnablingObjective_Links.ToList();
        }

        public async Task<List<Task_EnablingObjective_Link>> GetTaskEnablingObjectiveLinksByEOIdAsync(int eoId)
        {
            var task_EnablingObjective_Links = await FindAsync(r => r.EnablingObjectiveId == eoId);
            return task_EnablingObjective_Links.ToList();
        }
    }
}
