using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TaskRepository : Common.Repository<Domain.Entities.Core.Task>, ITaskRepository
    {
        public TaskRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
