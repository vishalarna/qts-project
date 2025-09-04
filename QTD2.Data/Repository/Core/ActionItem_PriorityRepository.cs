using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_PriorityRepository : Common.Repository<ActionItem_Priority>, IActionItem_PriorityRepository
    {
        public ActionItem_PriorityRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
