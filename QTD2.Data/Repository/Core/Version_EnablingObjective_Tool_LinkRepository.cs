using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_EnablingObjective_Tool_LinkRepository : Common.Repository<Version_EnablingObjective_Tool_Link>, IVersion_EnablingObjective_Tool_LinkRepository
    {
        public Version_EnablingObjective_Tool_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
