using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_SafetyHazard_OperationRepository : Common.Repository<ActionItem_SafetyHazard_Operation>, IActionItem_SafetyHazard_OperationRepository
    {
        public ActionItem_SafetyHazard_OperationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}