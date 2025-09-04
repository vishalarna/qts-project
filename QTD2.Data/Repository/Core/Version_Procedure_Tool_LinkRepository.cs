using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Procedure_Tool_LinkRepository : Common.Repository<Version_Procedure_Tool_Link>, IVersion_Procedure_Tool_LinkRepository
    {
        public Version_Procedure_Tool_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
