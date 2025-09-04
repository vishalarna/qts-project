using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReview_TaskReviewActionItem_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int PriorityId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        
        public TaskReview_TaskReviewActionItem_VM(int id, string type, int priorityId, DateTime assignedDate, DateTime dueDate, string? notes)
        {
            Id = id;
            Type = type;
            PriorityId = priorityId;
            AssignedDate = assignedDate;
            DueDate = dueDate;
            Notes = notes;
        }
        public TaskReview_TaskReviewActionItem_VM(){}
    }
}
