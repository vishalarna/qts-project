using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Version_ToolRepository : Common.Repository<Version_Tool>, IVersion_ToolRepository
    {
        public Version_ToolRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
