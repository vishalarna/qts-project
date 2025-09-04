using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class PositionRepository : Common.Repository<Position>, IPositionRepository
    {
        public PositionRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
