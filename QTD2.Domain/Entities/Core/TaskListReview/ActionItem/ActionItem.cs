using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace QTD2.Domain.Entities.Core
{
    public class ActionItem : Common.Entity
    {
        public int TaskReviewId { get; set; }
        public int? AssigneeId { get; set; }
        public int PriorityId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public virtual TaskReview TaskReview { get; set; }
        public virtual QTDUser Assignee { get; set; }
        public virtual ActionItem_Priority Priority { get; set; }
        
        public string ActionItemTypeToDisplay
        {
            get {
                return this.GetType().Name.Replace("_ActionItem", "").Replace("_","");
            }
            set{}
        }
        public ActionItem(int taskReviewId,int? assigneeId,int priorityId, DateTime assignedDate, DateTime dueDate, string notes)
        {
            TaskReviewId = taskReviewId;
            AssigneeId = assigneeId;
            PriorityId = priorityId;
            AssignedDate = assignedDate;
            DueDate = dueDate;
            Notes = notes;
        }

        public ActionItem() { }

        public override void Delete()
        {
            base.Delete();
            switch (this)
            {
                case SubDutyArea_ActionItem subDutyAreaActionItem:
                    subDutyAreaActionItem.ActionItem_SubDuty_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case Steps_ActionItem step_ActionItem:
                    step_ActionItem.ActionItem_Step_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case Task_Specific_Suggestions_ActionItem suggestion_ActionItem:
                    suggestion_ActionItem.ActionItem_Suggestion_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case QuestionAndAnswer_ActionItem questionAnswer_ActionItem:
                    questionAnswer_ActionItem.ActionItem_QuestionAndAnswer_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case Tools_ActionItem tool_ActionItem:
                    tool_ActionItem.ActionItem_Tool_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case Procedure_ActionItem procedure_ActionItem:
                    procedure_ActionItem.ActionItem_Procedure_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case EnablingObjective_ActionItem enablingObjective_ActionItem:
                    enablingObjective_ActionItem.ActionItem_EnablingObjective_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case RegulatoryRequirements_ActionItem rr_ActionItem:
                    rr_ActionItem.ActionItem_RegulatoryRequirement_Operations?.ToList().ForEach(x => x.Delete());
                    break;

                case SafetyHazards_ActionItem safetyHazard_ActionItem:
                    safetyHazard_ActionItem.ActionItem_SafetyHazard_Operations?.ToList().ForEach(x => x.Delete());
                    break;
            }
        }

    }
}
