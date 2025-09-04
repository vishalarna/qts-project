using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_ActionItem : ActionItem
    {
        public virtual List<ActionItem_Procedure_Operation> ActionItem_Procedure_Operations { get; set; } = new List<ActionItem_Procedure_Operation>();

        public Procedure_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public Procedure_ActionItem() { }

        public ActionItem_Procedure_Operation AddProcedureOperation(int operationTypeId, int procedureId)
        {
            ActionItem_Procedure_Operation procedureOperation = new ActionItem_Procedure_Operation(this.Id, operationTypeId, procedureId);
            AddEntityToNavigationProperty<ActionItem_Procedure_Operation>(procedureOperation);
            return procedureOperation;
        }

        public void UpdateProcedureOperation(int id, int operationTypeId, int procedureId)
        {
            ActionItem_Procedure_Operation procedureOperation = ActionItem_Procedure_Operations.FirstOrDefault(x => x.Id == id);
            if (procedureOperation != null)
            {
                procedureOperation.UpdateOperation(operationTypeId, procedureId);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Procedure_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_Procedure_Operations = new List<ActionItem_Procedure_Operation>();
            foreach (var actionItemProcedureOperation in this.ActionItem_Procedure_Operations)
            {
                var actionItemProcedureOperationCopy = actionItemProcedureOperation.Copy<ActionItem_Procedure_Operation>(createdBy);
                actionItemProcedureOperationCopy.ActionItemId = 0;
                copy.ActionItem_Procedure_Operations.Add(actionItemProcedureOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}