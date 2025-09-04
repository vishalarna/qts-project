using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem_Step_Operation : Common.Entity
    {
        public int ActionItemId { get; set; }
        public int OperationTypeId { get; set; }
        public int? Task_StepId { get; set; }
        public string? Description { get; set; }
        public virtual Steps_ActionItem ActionItem { get; set; }
        public virtual ActionItem_OperationTypes OperationType { get; set; }
        public virtual Task_Step Task_Step { get; set; }
        public ActionItem_Step_Operation(int actionItemid, int operationTypeId, int? task_StepId, string description)
        {
            ActionItemId = actionItemid;
            OperationTypeId = operationTypeId;
            Task_StepId = task_StepId;
            Description = description;
        }

        public ActionItem_Step_Operation() { }

        public void UpdateOperation(int operationTypeId, int? task_StepId, string description)
        {
            OperationTypeId = operationTypeId;
            Task_StepId = task_StepId;
            Description = description;
        }

    }

}
