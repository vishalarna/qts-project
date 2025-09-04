using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskListReview : Common.Entity
    {
        public string Title { get; set; }
        public int TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public string? Conclusion { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Signature { get; set; }
        public string? ReviewedBy { get; set; }
        public List<TaskListReview_GeneralReviewer> GeneralReviewers { get; set; } = new List<TaskListReview_GeneralReviewer>();
        public List<TaskReview> TaskReviews { get; set; } = new List<TaskReview>();
        public virtual TaskListReview_Status Status { get; set; }
        public virtual TaskListReview_Type Type { get; set; }
        public virtual List<TaskListReview_PositionLink> TaskListReview_PositionLinks { get; set; } = new List<TaskListReview_PositionLink>();
        public TaskListReview(string title, int typeId, DateTime startDate, DateTime endDate, int statusId, string? reviewedBy)
        {
            Title = title;
            TypeId = typeId;
            StartDate = startDate;
            EndDate= endDate;
            StatusId = statusId;
            ReviewedBy = reviewedBy;
        }
        public TaskListReview() { }
        public TaskListReview_GeneralReviewer AddGeneralReviewer(int generalReviewerId)
        {
            TaskListReview_GeneralReviewer generalReviewer = GeneralReviewers.FirstOrDefault(x => x.GeneralReviewerId == generalReviewerId && x.TaskListReviewId == this.Id);
            if (generalReviewer == null)
            {
                generalReviewer = new TaskListReview_GeneralReviewer(this.Id, generalReviewerId);
                AddEntityToNavigationProperty<TaskListReview_GeneralReviewer>(generalReviewer);
            }
            TaskReviews.ForEach(x => x.AddReviewer(generalReviewerId));
            return generalReviewer;
        }
        public TaskListReview_GeneralReviewer RemoveGeneralReviewer(TaskListReview_GeneralReviewer generalReviewer)
        {
            generalReviewer.Delete();
            TaskReviews.ForEach(x => x.RemoveReviewer(generalReviewer.GeneralReviewerId));
            return generalReviewer;

        }
        public TaskReview AddTaskReview(int taskId)
        {
            TaskReview taskReview = TaskReviews.FirstOrDefault(x => x.TaskId == taskId && x.TaskListReviewId == this.Id);
            if (taskReview == null)
            {
                taskReview = new TaskReview(this.Id,1,taskId);
                AddEntityToNavigationProperty<TaskReview>(taskReview);
                GeneralReviewers.ForEach(x => taskReview.AddReviewer(x.GeneralReviewerId));
            }
            return taskReview;
        }
        public void RemoveTaskReview(int taskReviewId)
        {
            TaskReview taskReview = TaskReviews.FirstOrDefault(x => x.Id == taskReviewId);
            if (taskReview != null)
            {
                taskReview.Delete();
            }
        }
        public void Publish()
        {
            StatusId =  2 ;
        }
        public override void Delete()
        {
            base.Delete();
            GeneralReviewers.ForEach(x => x.Delete());
            TaskReviews.ForEach(x => x.Delete());
            TaskListReview_PositionLinks.ForEach(x => x.Delete());
            AddDomainEvent(new Domain.Events.Core.OnTaskListReview_Deleted(this));
        }
        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as TaskListReview;

            copy.Title = this.Title + " -Copy";
            copy.StatusId = 1;

            //ensure includes
            copy.GeneralReviewers = new List<TaskListReview_GeneralReviewer>();
            foreach (var generalReviewer in this.GeneralReviewers)
            {
                var generalReviewerCopy = generalReviewer.Copy<TaskListReview_GeneralReviewer>(createdBy);
                generalReviewerCopy.TaskListReviewId = 0;
                copy.GeneralReviewers.Add(generalReviewerCopy);
            }

            copy.TaskReviews = new List<TaskReview>();
            foreach (var taskReview in this.TaskReviews)
            {
                var taskReviewCopy = taskReview.Copy<TaskReview>(createdBy);
                taskReviewCopy.TaskListReviewId = 0;
                copy.TaskReviews.Add(taskReviewCopy);
            }

            copy.TaskListReview_PositionLinks = new List<TaskListReview_PositionLink>();
            foreach(var positionLink in this.TaskListReview_PositionLinks)
            {
                var positionLinkCopy = positionLink.Copy<TaskListReview_PositionLink>(createdBy);
                positionLinkCopy.TaskListReviewId = 0;
                copy.TaskListReview_PositionLinks.Add(positionLinkCopy);
            }

            return (T)(object)copy;
        }

        public void UpdateTaskListReview(string title, int typeId, DateTime startDate, DateTime endDate, string conclusion, DateTime? approvalDate,string signature, string reviewedBy)
        {
            Title = title;
            TypeId = typeId;
            StartDate = startDate;
            EndDate = endDate;
            Conclusion = conclusion;
            ApprovalDate = approvalDate;
            Signature = signature;
            ReviewedBy = reviewedBy;
        }

        public TaskListReview_PositionLink AddPosition(int positionId)
        {
            TaskListReview_PositionLink position = TaskListReview_PositionLinks.FirstOrDefault(x => x.PositionId == positionId && x.TaskListReviewId == this.Id);
            if (position == null)
            {
                position = new TaskListReview_PositionLink(this.Id, positionId);
                AddEntityToNavigationProperty<TaskListReview_PositionLink>(position);
            }
            return position;
        }

        public void RemovePosition(int positionId)
        {
            var position = TaskListReview_PositionLinks.FirstOrDefault(x => x.PositionId == positionId && x.TaskListReviewId == this.Id);

            if (position != null)
            {
                position.Delete();
            }
        }

    }
}
