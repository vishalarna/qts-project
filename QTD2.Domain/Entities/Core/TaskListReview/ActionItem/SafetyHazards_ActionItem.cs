using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class SafetyHazards_ActionItem : ActionItem
    {
        public virtual List<ActionItem_SafetyHazard_Operation> ActionItem_SafetyHazard_Operations { get; set; } = new List<ActionItem_SafetyHazard_Operation>();

        public SafetyHazards_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public SafetyHazards_ActionItem() { }

        public ActionItem_SafetyHazard_Operation AddSafetyhazardOperation(int operationTypeId, int safetyHazardId)
        {
            ActionItem_SafetyHazard_Operation safetyHazardOperation = new ActionItem_SafetyHazard_Operation(this.Id, operationTypeId, safetyHazardId);
            AddEntityToNavigationProperty<ActionItem_SafetyHazard_Operation>(safetyHazardOperation);
            return safetyHazardOperation;
        }

        public void UpdateSafetyHazardOperation(int id, int operationTypeId, int safetyHazardId)
        {
            ActionItem_SafetyHazard_Operation safetyHazardOperation = ActionItem_SafetyHazard_Operations.FirstOrDefault(x => x.Id == id);
            if (safetyHazardOperation != null)
            {
                safetyHazardOperation.UpdateOperation(operationTypeId, safetyHazardId);
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<SafetyHazards_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_SafetyHazard_Operations = new List<ActionItem_SafetyHazard_Operation>();
            foreach (var actionItemSafetyHazardOperation in this.ActionItem_SafetyHazard_Operations)
            {
                var actionItemSafetyHazardOperationCopy = actionItemSafetyHazardOperation.Copy<ActionItem_SafetyHazard_Operation>(createdBy);
                actionItemSafetyHazardOperationCopy.ActionItemId = 0;
                copy.ActionItem_SafetyHazard_Operations.Add(actionItemSafetyHazardOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}