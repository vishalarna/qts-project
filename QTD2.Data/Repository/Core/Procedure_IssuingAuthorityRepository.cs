using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_IssuingAuthorityRepository : Common.Repository<Procedure_IssuingAuthority>, IProcedure_IssuingAuthorityRepository
    {
        public Procedure_IssuingAuthorityRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
