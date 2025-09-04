using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingIssue_ActionItem : Common.Entity
    {
        public int TrainingIssueId { get; set; }
        public int Order { get; set; }
        public string ActionItemName { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? DateAssigned { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int? StatusId { get; set; }
        public string? Notes { get; set; }
        public string? AssigneeName { get; set; }
        public TrainingIssue TrainingIssue { get; set; }
        public TrainingIssue_ActionItemPriority Priority { get; set; }
        public TrainingIssue_ActionItemStatus Status { get; set; }

        public TrainingIssue_ActionItem() { }

        public TrainingIssue_ActionItem(int trainingIssueId, int order,string actionItemName,int? priorityId,DateTime? dateAssigned,DateTime? dueDate,DateTime? dateCompleted,int? statusId, string? notes,string? assigneeName)
        {
            TrainingIssueId = trainingIssueId;
            Order = order;
            ActionItemName = actionItemName;
            PriorityId = priorityId;
            DateAssigned = dateAssigned;
            DueDate = dueDate;
            DateCompleted = dateCompleted;
            StatusId = statusId;
            Notes = notes;
            AssigneeName = assigneeName;
        }

        public void UpdateActionItem(int order, string actionItemName, int? priorityId, DateTime? dateAssigned, DateTime? dueDate, DateTime? dateCompleted, int? statusId, string? notes, string assigneeName)
        {
            Order = order;
            ActionItemName = actionItemName;
            PriorityId = priorityId;
            DateAssigned = dateAssigned;
            DueDate = dueDate;
            DateCompleted = dateCompleted;
            StatusId = statusId;
            Notes = notes;
            AssigneeName = assigneeName;
        }
    }
}
