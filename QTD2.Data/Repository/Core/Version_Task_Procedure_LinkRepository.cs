using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_Procedure_LinkRepository : Common.Repository<Version_Task_Procedure_Link>, IVersion_Task_Procedure_LinkRepository
    {
        public Version_Task_Procedure_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
