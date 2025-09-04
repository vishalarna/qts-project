using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Task_ToolService : Common.Service<Entities.Core.Task_Tool>, ITask_ToolService
    {
        public Task_ToolService(ITask_ToolRepository task_ToolRepository, ITask_ToolValidation task_ToolValidation)
            : base(task_ToolRepository, task_ToolValidation)
        {
        }
    }
}
