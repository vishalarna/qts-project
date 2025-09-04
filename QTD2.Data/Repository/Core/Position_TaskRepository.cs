using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;


namespace QTD2.Data.Repository.Core
{
    public class Position_TaskRepository : Common.Repository<Position_Task>, IPosition_TaskRepository
    {
        public Position_TaskRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
