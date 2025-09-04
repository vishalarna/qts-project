using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_ProcedureRepository : Common.Repository<Version_Procedure>, IVersion_ProcedureRepository
    {
        public Version_ProcedureRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
