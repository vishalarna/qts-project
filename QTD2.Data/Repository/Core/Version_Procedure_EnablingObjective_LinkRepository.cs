using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Procedure_EnablingObjective_LinkRepository : Common.Repository<Version_Procedure_EnablingObjective_Link>, IVersion_Procedure_EnablingObjective_LinkRepository
    {
        public Version_Procedure_EnablingObjective_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
