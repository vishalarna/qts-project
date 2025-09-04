using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SubDutyArea_ActionItem : ActionItem
    {
        public virtual List<ActionItem_SubDuty_Operation> ActionItem_SubDuty_Operations { get; set; } = new List<ActionItem_SubDuty_Operation>();


        public SubDutyArea_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public SubDutyArea_ActionItem() { }

        public ActionItem_SubDuty_Operation AddSubDutyOperation(int operationTypeId, int subDutyAreaId)
        {
            ActionItem_SubDuty_Operation subDutyOperation = new ActionItem_SubDuty_Operation(this.Id, operationTypeId, subDutyAreaId);
            AddEntityToNavigationProperty<ActionItem_SubDuty_Operation>(subDutyOperation);
            return subDutyOperation;
        }

        public void UpdateSubDutyOperation(int id, int operationTypeId, int subDutyAreaId)
        {
            ActionItem_SubDuty_Operation subDutyOperation = ActionItem_SubDuty_Operations.FirstOrDefault(x => x.Id == id);
            if (subDutyOperation != null)
            {
                subDutyOperation.UpdateOperation(operationTypeId, subDutyAreaId);
            }
        }


        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<SubDutyArea_ActionItem>(createdBy);

            // Ensure includes
            copy.ActionItem_SubDuty_Operations = new List<ActionItem_SubDuty_Operation>();
            foreach (var actionItemSubDutyOperation in ActionItem_SubDuty_Operations)
            {
                var actionItemSubDutyOperationCopy = actionItemSubDutyOperation.Copy<ActionItem_SubDuty_Operation>(createdBy);
                actionItemSubDutyOperationCopy.ActionItemId = 0;
                copy.ActionItem_SubDuty_Operations.Add(actionItemSubDutyOperationCopy);
            }

            return (T)(object)copy;
        }
    }
}
