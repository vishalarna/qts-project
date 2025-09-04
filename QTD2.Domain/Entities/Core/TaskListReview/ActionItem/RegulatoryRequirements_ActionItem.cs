using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class RegulatoryRequirements_ActionItem : ActionItem
    {
        public virtual List<ActionItem_RegulatoryRequirement_Operation> ActionItem_RegulatoryRequirement_Operations { get; set; } = new List<ActionItem_RegulatoryRequirement_Operation>();

        public RegulatoryRequirements_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public RegulatoryRequirements_ActionItem() { }

        public ActionItem_RegulatoryRequirement_Operation AddRegulatoryRequirementOperation(int operationTypeId, int rrId)
        {
            ActionItem_RegulatoryRequirement_Operation rrOperation = new ActionItem_RegulatoryRequirement_Operation(this.Id, operationTypeId, rrId);
            AddEntityToNavigationProperty<ActionItem_RegulatoryRequirement_Operation>(rrOperation);
            return rrOperation;
        }

        public void UpdateRegulatoryRequirementOperation(int id, int operationTypeId, int rrId)
        {
            ActionItem_RegulatoryRequirement_Operation rrOperation = ActionItem_RegulatoryRequirement_Operations.FirstOrDefault(x => x.Id == id);
            if (rrOperation != null)
            {
                rrOperation.UpdateOperation(operationTypeId, rrId);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<RegulatoryRequirements_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_RegulatoryRequirement_Operations = new List<ActionItem_RegulatoryRequirement_Operation>();
            foreach (var actionItemRegulatoryRequirementOperation in this.ActionItem_RegulatoryRequirement_Operations)
            {
                var actionItemRegulatoryRequirementOperationCopy = actionItemRegulatoryRequirementOperation.Copy<ActionItem_RegulatoryRequirement_Operation>(createdBy);
                actionItemRegulatoryRequirementOperationCopy.ActionItemId = 0;
                copy.ActionItem_RegulatoryRequirement_Operations.Add(actionItemRegulatoryRequirementOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
