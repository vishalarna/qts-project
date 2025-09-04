using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReview_VM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public string? ReviewedBy { get; set; }
        public List<int> PositionIds { get; set; } = new List<int>();
        public List<TaskListReview_GeneralReviewer_VM> GeneralReviewers { get; set; } = new List<TaskListReview_GeneralReviewer_VM>();
        public List<TaskListReview_TaskReview_VM> TaskReviews { get; set; } = new List<TaskListReview_TaskReview_VM>();
        public string? Conclusion { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Signature { get; set; }
        public bool Active { get; set; }

        public TaskListReview_VM(int id, string title, int typeId, DateTime startDate, DateTime endDate,int statusId, List<TaskListReview_GeneralReviewer_VM> generalReviewers, List<TaskListReview_TaskReview_VM> taskReviews, string conclusion, DateTime? approvalDate, string signature, bool active,List<int> positionIds, string? reviewedBy)
        {
            Id = id;
            Title = title;
            TypeId = typeId;
            StartDate = startDate;
            EndDate = endDate;
            StatusId = statusId;
            GeneralReviewers = generalReviewers;
            TaskReviews = taskReviews;
            Conclusion = conclusion;
            ApprovalDate = approvalDate;
            Signature = signature;
            Active = active;
            PositionIds = positionIds;
            ReviewedBy = reviewedBy;
        }
        public TaskListReview_VM()
        {

        }
    }
}
