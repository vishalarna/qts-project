using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActionItem_OperationType_LinksRepository : Common.Repository<ActionItem_OperationType_Links>, IActionItem_OperationType_LinksRepository
    {
        public ActionItem_OperationType_LinksRepository(QTDContext context)
            : base(context)
        {
        }
    }
}