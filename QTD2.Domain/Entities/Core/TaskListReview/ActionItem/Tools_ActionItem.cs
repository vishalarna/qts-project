using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Tools_ActionItem : ActionItem
    {
        public virtual List<ActionItem_Tool_Operation> ActionItem_Tool_Operations { get; set; } = new List<ActionItem_Tool_Operation>();

        public Tools_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public Tools_ActionItem() { }

        public ActionItem_Tool_Operation AddToolOperation(int operationTypeId, int toolId)
        {
            ActionItem_Tool_Operation toolOperation = new ActionItem_Tool_Operation(this.Id, operationTypeId, toolId);
            AddEntityToNavigationProperty<ActionItem_Tool_Operation>(toolOperation);
            return toolOperation;
        }

        public void UpdateToolOperation(int id, int operationTypeId, int toolId)
        {
            ActionItem_Tool_Operation toolOperation = ActionItem_Tool_Operations.FirstOrDefault(x => x.Id == id);
            if (toolOperation != null)
            {
                toolOperation.UpdateOperation(operationTypeId, toolId);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Tools_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_Tool_Operations = new List<ActionItem_Tool_Operation>();
            foreach (var actionItemToolOperation in ActionItem_Tool_Operations)
            {
                var actionItemToolOperationCopy = actionItemToolOperation.Copy<ActionItem_Tool_Operation>(createdBy);
                actionItemToolOperationCopy.ActionItemId = 0;
                copy.ActionItem_Tool_Operations.Add(actionItemToolOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
