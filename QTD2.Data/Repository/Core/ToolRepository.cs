using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ToolRepository : Common.Repository<Tool>, IToolRepository
    {
        public ToolRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
