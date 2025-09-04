using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_EnablingObjective_LinkRepository : Common.Repository<Procedure_EnablingObjective_Link>, IProcedure_EnablingObjective_LinkRepository
    {
        public Procedure_EnablingObjective_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
