using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Task_StepService : Common.Service<Entities.Core.Task_Step>, ITask_StepService
    {
        public Task_StepService(ITask_StepRepository task_StepRepository, ITask_StepValidation task_StepValidation)
            : base(task_StepRepository, task_StepValidation)
        {
        }
    }
}
