using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_SubDuty_Operation : Common.Entity 
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int SubDutyAreaId { get; set; }
        public virtual SubDutyArea_ActionItem ActionItem { get; set; }
        public virtual SubdutyArea SubDutyArea { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }

        public ActionItem_SubDuty_Operation(int actionItemid, int operationTypeId, int subDutyAreaId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            SubDutyAreaId = subDutyAreaId;
        }

        public void UpdateOperation(int operationTypeId, int subDutyAreaId)
        {
            OperationTypeId = operationTypeId;
            SubDutyAreaId = subDutyAreaId;
        }

        public ActionItem_SubDuty_Operation() { }
    }
}
