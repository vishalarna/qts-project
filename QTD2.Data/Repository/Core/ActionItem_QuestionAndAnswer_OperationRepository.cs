using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class ActionItem_QuestionAndAnswer_OperationRepository : Common.Repository<ActionItem_QuestionAndAnswer_Operation>, IActionItem_QuestionAndAnswer_OperationRepository
    {
        public ActionItem_QuestionAndAnswer_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
