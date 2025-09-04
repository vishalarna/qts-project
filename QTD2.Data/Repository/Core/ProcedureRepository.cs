using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ProcedureRepository : Common.Repository<Procedure>, IProcedureRepository
    {
        public ProcedureRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
