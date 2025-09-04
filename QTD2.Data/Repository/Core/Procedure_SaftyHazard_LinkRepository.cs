using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_SaftyHazard_LinkRepository : Common.Repository<Procedure_SaftyHazard_Link>, IProcedure_SaftyHazard_LinkRepository
    {
        public Procedure_SaftyHazard_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
