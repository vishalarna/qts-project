using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskReview : Common.Entity
    {
        public int TaskListReviewId { get; set; }
        public int StatusId { get; set; }
        public int TaskId { get; set; }
        public int? FindingId { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? RequalificationDueDate { get; set; }
        public string? Notes { get; set; }
        public int? TrainingIssueId { get; set; }
        public virtual List<ActionItem> ActionItems { get; set; } = new List<ActionItem>();
        public virtual TaskListReview TaskListReview { get; set; }
        public virtual TaskReview_Status Status { get; set; }
        public virtual List<TaskReview_Reviewer> Reviewers { get; set; } = new List<TaskReview_Reviewer>();
        public virtual TaskReview_Finding Finding { get; set; }
        public virtual Task Task { get; set; }
        public virtual TrainingIssue TrainingIssue { get; set; }
        
        public TaskReview(int taskListReviewId, int statusId, int taskId)
        {
            TaskListReviewId = taskListReviewId;
            StatusId = statusId;
            TaskId = taskId;
        }
        public TaskReview()
        {

        }
        public TaskReview_Reviewer AddReviewer(int reviewerId)
        {
            TaskReview_Reviewer reviewer = Reviewers.FirstOrDefault(x => x.ReviewerId == reviewerId && x.TaskReviewId == this.Id);
            if (reviewer == null)
            {
                reviewer = new TaskReview_Reviewer(this.Id,reviewerId);
                AddEntityToNavigationProperty<TaskReview_Reviewer>(reviewer);
            }
            return reviewer;
        }
        public TaskReview_Reviewer RemoveReviewer(int reviewerId)
        {
            TaskReview_Reviewer reviewer = Reviewers.FirstOrDefault(x => x.ReviewerId == reviewerId && x.TaskReviewId == this.Id);
            if (reviewer != null)
            {
                reviewer.Delete();
            }
            return reviewer;
        }
        public void UpdateTaskReview(DateTime? reviewDate, DateTime? requalificationDueDate, string notes)
        {
            ReviewDate = reviewDate;
            RequalificationDueDate = requalificationDueDate;
            Notes = notes;
        }
        public void SetFinding(int? findingId)
        {
            FindingId = findingId;
            if(findingId != 3)
            {
                RequalificationDueDate = null;
            }
        }
        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnTaskReview_Deleted(this));
            base.Delete();
            ActionItems.ForEach(x => x.Delete());
            Reviewers.ForEach(x => x.Delete());
        }
        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as TaskReview;

            //ensure includes
            copy.ActionItems = new List<ActionItem>();
            foreach (var actionItem in this.ActionItems)
            {
                var actionItemCopy = actionItem.Copy<ActionItem>(createdBy);
                actionItemCopy.TaskReviewId = 0;
                copy.ActionItems.Add(actionItemCopy);
            }

            copy.Reviewers = new List<TaskReview_Reviewer>();
            foreach (var reviewer in this.Reviewers)
            {
                var reviewerCopy = reviewer.Copy<TaskReview_Reviewer>(createdBy);
                reviewerCopy.TaskReviewId = 0;
                copy.Reviewers.Add(reviewerCopy);
            }

            return (T)(object)copy;
        }

        public void SetTaskReview_StatusId(int? findingId)
        {
            if (findingId != null)
            {
                StatusId = 2;
            }
            else
            {
              StatusId = 1;
            }
        }

        public void SetStatusNotStarted()
        {
            StatusId = 3;
        }
    }
}
