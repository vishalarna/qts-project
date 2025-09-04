using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_RegulatoryRequirement_OperationRepository : Common.Repository<ActionItem_RegulatoryRequirement_Operation>, IActionItem_RegulatoryRequirement_OperationRepository
    {
        public ActionItem_RegulatoryRequirement_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}