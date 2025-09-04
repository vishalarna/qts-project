using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
   public class ActionItem_Procedure_OperationRepository : Common.Repository<ActionItem_Procedure_Operation>, IActionItem_Procedure_OperationRepository
    {
        public ActionItem_Procedure_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
