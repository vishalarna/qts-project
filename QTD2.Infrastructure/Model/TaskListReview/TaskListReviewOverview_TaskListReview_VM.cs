using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReviewOverview_TaskListReview_VM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool Active { get; set; }
        public List<string> Positions = new List<string>();
        public List<TaskListReview_TaskReview_VM> TaskReviews { get; set; } = new List<TaskListReview_TaskReview_VM>();

        public TaskListReviewOverview_TaskListReview_VM(int id, string title, string type, DateTime startdate, DateTime endDate, string status, DateTime? approvaldate, bool active, List<string> positions)
        {
            Id = id;
            Title = title;
            Type = type;
            StartDate = startdate;
            EndDate = endDate;
            Status = status;
            ApprovalDate = approvaldate;
            Active = active;
            Positions = positions ?? new List<string>();
        }
    }
}
