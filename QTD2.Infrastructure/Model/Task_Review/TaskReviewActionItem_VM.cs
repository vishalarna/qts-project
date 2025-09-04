using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? AssigneeId { get; set; }
        public int PriorityId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Notes { get; set; }
        public List<TaskReviewActionItem_SubDutyArea_Operation_VM> SubDuty_Operations { get; set; } = new List<TaskReviewActionItem_SubDutyArea_Operation_VM>();
        public int? Task_number { get; set; }
        public string? Task_statement { get; set; }
        public List<TaskReviewActionItem_Step_Operation_VM> Step_Operations { get; set; } = new List<TaskReviewActionItem_Step_Operation_VM>();
        public List<TaskReviewActionItem_QuestionAndAnswer_Operation_VM> QuestionAndAnswer_Operations { get; set; } = new List<TaskReviewActionItem_QuestionAndAnswer_Operation_VM>();
        public List<TaskReviewActionItem_Suggestion_Operation_VM> Suggestion_Operations { get; set; } = new List<TaskReviewActionItem_Suggestion_Operation_VM>();
        public string? Conditions { get; set; }
        public string? Criteria { get; set; }
        public string? References { get; set; }
        public List<TaskReviewActionItem_Tool_Operation_VM> Tool_Operations { get; set; } = new List<TaskReviewActionItem_Tool_Operation_VM>();
        public List<TaskReviewActionItem_EnablingObjective_Operation_VM> EnablingObjective_Operations { get; set; } = new List<TaskReviewActionItem_EnablingObjective_Operation_VM>();
        public List<TaskReviewActionItem_Procedure_Operation_VM> Procedure_Operations { get; set; }  = new List<TaskReviewActionItem_Procedure_Operation_VM>();
        public List<TaskReviewActionItem_RegulatoryRequirement_Operation_VM> RegulatoryRequirement_Operations { get; set; } = new List<TaskReviewActionItem_RegulatoryRequirement_Operation_VM>();
        public List<TaskReviewActionItem_SafetyHazard_Operation_VM> SafetyHazard_Operations { get; set; } = new List<TaskReviewActionItem_SafetyHazard_Operation_VM>();
        public bool? IsMeta { get; set; }

        public string? Priority { get; set; }
        public string? Assignees { get; set; }
    }
}
