using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReview_TaskReview_VM
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Number { get; set; }
        public string Statement { get; set; }
        public string Positions { get; set; }
        public DateTime? RecentHistoryDate { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Finding { get; set; }
        public string Status { get; set; }
        public List<TaskListReview_TaskReviewActionItem_VM> TaskReviewActionItems { get; set; } = new List<TaskListReview_TaskReviewActionItem_VM>();
        public List<TaskListReview_TaskReviewReviewer_VM> Reviewers { get; set; } = new List<TaskListReview_TaskReviewReviewer_VM>();
        public TaskListReview_TaskReview_VM(int id,int taskId, string number, string statement, string positions, DateTime? recentHistoryDate, string reviewedBy, DateTime? reviewDate, string finding, string status)
        {
            Id = id;
            TaskId = taskId;
            Number = number;
            Statement = statement;
            Positions = positions;
            RecentHistoryDate = recentHistoryDate;
            ReviewedBy = reviewedBy;
            ReviewDate = reviewDate;
            Finding = finding;
            Status = status;
        }
        public TaskListReview_TaskReview_VM()
        {

        }
    }
}
