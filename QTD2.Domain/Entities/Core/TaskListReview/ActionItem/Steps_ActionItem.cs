using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Steps_ActionItem : ActionItem
    {
        public virtual List<ActionItem_Step_Operation> ActionItem_Step_Operations { get; set; } = new List<ActionItem_Step_Operation>();


        public Steps_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public Steps_ActionItem() { }

        public ActionItem_Step_Operation AddStepOperation(int operationTypeId, int? taskStepId,string description)
        {
            ActionItem_Step_Operation stepOperation = new ActionItem_Step_Operation(this.Id, operationTypeId, taskStepId, description);
            AddEntityToNavigationProperty<ActionItem_Step_Operation>(stepOperation);
            return stepOperation;
        }

        public void UpdateStepOperation(int id, int operationTypeId, int? taskStepId,string description)
        {
            ActionItem_Step_Operation stepOperation = ActionItem_Step_Operations.FirstOrDefault(x => x.Id == id);
            if (stepOperation != null)
            {
                stepOperation.UpdateOperation(operationTypeId, taskStepId, description);
            }
        }


        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Steps_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_Step_Operations = new List<ActionItem_Step_Operation>();
            foreach (var actionItemStepOperation in this.ActionItem_Step_Operations)
            {
                var actionItemStepOperationCopy = actionItemStepOperation.Copy<ActionItem_Step_Operation>(createdBy);
                actionItemStepOperationCopy.ActionItemId = 0;
                copy.ActionItem_Step_Operations.Add(actionItemStepOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
