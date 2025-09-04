using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_StatusHistoryRepository : Common.Repository<Procedure_StatusHistory>, IProcedure_StatusHistoryRepository
    {
        public Procedure_StatusHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
