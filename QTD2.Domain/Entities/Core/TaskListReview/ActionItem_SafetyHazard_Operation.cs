using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_SafetyHazard_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? SafetyHazardId { get; set; }
        public virtual SafetyHazards_ActionItem ActionItem { get; set; }
        public virtual SaftyHazard SafetyHazard { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }

        public ActionItem_SafetyHazard_Operation(int actionItemid, int operationTypeId, int safetyHazardId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            SafetyHazardId = safetyHazardId;
        }

        public ActionItem_SafetyHazard_Operation() { }

        public void UpdateOperation(int operationTypeId, int safetyHazardId)
        {
            OperationTypeId = operationTypeId;
            SafetyHazardId = safetyHazardId;
        }

    }
}
