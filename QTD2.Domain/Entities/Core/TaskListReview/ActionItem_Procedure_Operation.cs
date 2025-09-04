using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_Procedure_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? ProcedureId { get; set; }
        public virtual Procedure_ActionItem ActionItem { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }

        public ActionItem_Procedure_Operation(int actionItemid, int operationTypeId, int procedureId)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            ProcedureId = procedureId;
        }

        public ActionItem_Procedure_Operation() { }

        public void UpdateOperation(int operationTypeId, int procedureId)
        {
            OperationTypeId = operationTypeId;
            ProcedureId = procedureId;
        }
    }
}
