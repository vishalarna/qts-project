using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class PositionHistoryRepository : Common.Repository<Position_History>, IPositionHistoryRepository
    {
        public PositionHistoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
