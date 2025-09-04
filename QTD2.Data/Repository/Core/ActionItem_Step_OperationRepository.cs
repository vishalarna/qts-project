using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_Step_OperationRepository : Common.Repository<ActionItem_Step_Operation>, IActionItem_Step_OperationRepository
    {
        public ActionItem_Step_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
