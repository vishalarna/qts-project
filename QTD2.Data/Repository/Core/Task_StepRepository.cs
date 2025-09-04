using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Task_StepRepository : Common.Repository<Task_Step>, ITask_StepRepository
    {
        public Task_StepRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
