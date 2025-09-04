using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_EnablingObjective_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? EnablingObjectiveId { get; set; }
        public virtual EnablingObjective_ActionItem ActionItem { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }
        public virtual EnablingObjective EnablingObjective { get; set; }

        public ActionItem_EnablingObjective_Operation(int actionItemid, int operationTypeId, int enablingObjectiveId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            EnablingObjectiveId = enablingObjectiveId;
        }

        public ActionItem_EnablingObjective_Operation() { }

        public void UpdateOperation(int operationTypeId, int enablingObjectiveId)
        {
            OperationTypeId = operationTypeId;
            EnablingObjectiveId = enablingObjectiveId;
        }
    }
}
