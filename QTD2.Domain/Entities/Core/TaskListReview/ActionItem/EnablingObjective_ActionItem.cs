using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_ActionItem : ActionItem
    {
        public virtual List<ActionItem_EnablingObjective_Operation> ActionItem_EnablingObjective_Operations { get; set; } = new List<ActionItem_EnablingObjective_Operation>();

        public EnablingObjective_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public EnablingObjective_ActionItem() { }

        public ActionItem_EnablingObjective_Operation AddEnablingObjectiveOperation(int operationTypeId, int eoId)
        {
            ActionItem_EnablingObjective_Operation eoOperation = new ActionItem_EnablingObjective_Operation(this.Id, operationTypeId, eoId);
            AddEntityToNavigationProperty<ActionItem_EnablingObjective_Operation>(eoOperation);
            return eoOperation;
        }

        public void UpdateEnablingObjectiveOperation(int id, int operationTypeId, int eoId)
        {
            ActionItem_EnablingObjective_Operation eoOperation = ActionItem_EnablingObjective_Operations.FirstOrDefault(x => x.Id == id);
            if (eoOperation != null)
            {
                eoOperation.UpdateOperation(operationTypeId, eoId);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<EnablingObjective_ActionItem>(createdBy);

            //ensure includes
            copy.ActionItem_EnablingObjective_Operations = new List<ActionItem_EnablingObjective_Operation>();
            foreach (var actionItemEnablingObjectiveOperation in this.ActionItem_EnablingObjective_Operations)
            {
                var actionItemEnablingObjectiveOperationCopy = actionItemEnablingObjectiveOperation.Copy<ActionItem_EnablingObjective_Operation>(createdBy);
                actionItemEnablingObjectiveOperationCopy.ActionItemId = 0;
                copy.ActionItem_EnablingObjective_Operations.Add(actionItemEnablingObjectiveOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
