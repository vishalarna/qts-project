using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_ActionItem_VM
    {
        public int? Id { get; set; }
        public int Order { get; set; }
        public string ActionItemName { get; set; }
        public int? PriorityId { get; set; }
    
        public DateTime? DateAssigned { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int? StatusId { get; set; }
        public string Notes { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? AssigneeName { get; set; }

        public TrainingIssue_ActionItem_VM(int id, int order, string actionItemName, int? priorityId, DateTime? dateAssigned, DateTime? dueDate, DateTime? dateCompleted, int? statusId, string notes,string? priority, string? status,string? assigneeName)
        {
            Id = id;
            Order = order;
            ActionItemName = actionItemName;
            PriorityId = priorityId;
            DateAssigned = dateAssigned;
            DueDate = dueDate;
            DateCompleted = dateCompleted;
            StatusId = statusId;
            Notes = notes;
            Priority = priority;
            Status = status;
            AssigneeName = assigneeName;
        }

        public TrainingIssue_ActionItem_VM()
        {
        }
    }
}
