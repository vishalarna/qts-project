using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_OperationTypesRepository : Common.Repository<ActionItem_OperationTypes>, IActionItem_OperationTypesRepository
    {
        public ActionItem_OperationTypesRepository(QTDContext context) : base(context)
        {
        }
    }
}