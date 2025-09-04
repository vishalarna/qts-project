using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItemRepository : Common.Repository<ActionItem>, IActionItemRepository
    {
        public ActionItemRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
