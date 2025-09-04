using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_EnablingObjective_OperationRepository : Common.Repository<ActionItem_EnablingObjective_Operation>, IActionItem_EnablingObjective_OperationRepository
    {
        public ActionItem_EnablingObjective_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
