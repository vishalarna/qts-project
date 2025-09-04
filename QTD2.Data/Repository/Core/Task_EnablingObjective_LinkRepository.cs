using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Task_EnablingObjective_LinkRepository : Common.Repository<Task_EnablingObjective_Link>, ITask_EnablingObjective_LinkRepository
    {
        public Task_EnablingObjective_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
