using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ToolGroup_ToolRepository : Common.Repository<ToolGroup_Tool>, IToolGroup_ToolRepository
    {
        public ToolGroup_ToolRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
