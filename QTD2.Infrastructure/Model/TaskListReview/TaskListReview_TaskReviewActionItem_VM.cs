using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReview_TaskReviewActionItem_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Assignees { get; set; }
        public string Priority { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }

        public TaskListReview_TaskReviewActionItem_VM(int id, string type, string assignees,string priority, DateTime assignedDate, DateTime dueDate)
        {
            Id = id;
            Type = type;
            Assignees = assignees;
            Priority = priority;
            AssignedDate = assignedDate;
            DueDate = dueDate;
        }
    }
}
