using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Positions_SQRepository : Common.Repository<Positions_SQ>, IPositions_SQRepository
    {
        public Positions_SQRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }


}
