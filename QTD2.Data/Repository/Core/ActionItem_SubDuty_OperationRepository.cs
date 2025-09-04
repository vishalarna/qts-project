using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_SubDuty_OperationRepository : Common.Repository<ActionItem_SubDuty_Operation>, IActionItem_SubDuty_OperationRepository
    {
        public ActionItem_SubDuty_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
