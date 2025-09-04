using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_RegulatoryRequirement_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? RegulatoryRequirementId { get; set; }
        public virtual RegulatoryRequirements_ActionItem ActionItem { get; set; }
        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }

        public ActionItem_RegulatoryRequirement_Operation(int actionItemid, int operationTypeId, int rrId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            RegulatoryRequirementId = rrId;
        }

        public ActionItem_RegulatoryRequirement_Operation() { }

        public void UpdateOperation(int operationTypeId, int rrId)
        {
            OperationTypeId = operationTypeId;
            RegulatoryRequirementId = rrId;
        }
    }
}
