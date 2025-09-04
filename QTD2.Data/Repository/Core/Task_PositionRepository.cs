using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Task_PositionRepository : Common.Repository<Task_Position>, ITask_PositionRepository
    {
        public Task_PositionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
