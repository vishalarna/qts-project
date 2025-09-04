using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_Suggestion_OperationRepository : Common.Repository<ActionItem_Suggestion_Operation>, IActionItem_Suggestion_OperationRepository
    {
        public ActionItem_Suggestion_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
