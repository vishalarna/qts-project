using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_Tool_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? ToolId { get; set; }
        public virtual Tools_ActionItem ActionItem { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }

        public ActionItem_Tool_Operation(int actionItemid, int operationTypeId, int toolId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            ToolId = toolId;
        }

        public ActionItem_Tool_Operation() { }

        public void UpdateOperation(int operationTypeId, int toolId)
        {
            OperationTypeId = operationTypeId;
            ToolId = toolId;
        }
    }
}
