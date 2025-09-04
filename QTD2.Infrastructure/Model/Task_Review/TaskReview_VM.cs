using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReview_VM
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Statement { get; set; }
        public DateTime? RecentHistoryDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public List<TaskReview_Reviewer_VM> Reviewers { get; set; }
        public int? FindingId { get; set; }
        public DateTime? RequalificationDueDate { get; set; }
        public string Notes { get; set; }
        public List<TaskReview_TaskReviewActionItem_VM> TaskReviewActionItems { get; set; }
        public int? NextTaskReviewId { get; set; }
        public int? TaskId { get; set; }
        public string? FullNumber { get; set; }
        public int? TrainingIssueId { get; set; }
        public TaskReview_VM() { }
    }
}
