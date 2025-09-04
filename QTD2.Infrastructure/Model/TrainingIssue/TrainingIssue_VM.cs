using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_VM
    {
        public int? Id { get; set; }
        public string IssueCode { get; set; }
        public string IssueTitle { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public int SeverityId { get; set; }
        public int? DriverTypeId { get; set; }
        public int? DriverSubTypeId { get; set; }
        public string? OtherComments { get; set; }
        public TrainingIssue_DataElement_VM DataElement { get; set; }
        public List<TrainingIssue_ActionItem_VM> ActionItems { get; set; }  = new List<TrainingIssue_ActionItem_VM>();
        public string? PlannedResponse { get; set; }
        public string Status { get; set; }
        public string SeverityLevel { get; set; }
        public string? DriverType { get; set; }
        public string? DriverSubType { get; set; }
        public int? TaskReviewId { get; set; }


        public TrainingIssue_VM() { }

        public TrainingIssue_VM(int id, string issueCode, string issueTitle, string? description, DateTime createdDate, DateTime dueDate, int statusId, int severityId, int? driverTypeId, int? driverSubTypeId, string? otherComments, string? plannedResponse, string status, string severityLevel, string? driverType, string? driverSubType, int? taskReviewId)
        {
            Id = id;
            IssueCode = issueCode;
            IssueTitle = issueTitle;
            Description = description;
            CreatedDate = createdDate;
            DueDate = dueDate;
            StatusId = statusId;
            SeverityId = severityId;
            DriverTypeId = driverTypeId;
            DriverSubTypeId = driverSubTypeId;
            OtherComments = otherComments;
            PlannedResponse = plannedResponse;
            Status = status;
            SeverityLevel = severityLevel;
            DriverType = driverType;
            DriverSubType = driverSubType;
            TaskReviewId = taskReviewId;
        }

        public TrainingIssue_VM(int id, string issueCode, string issueTitle, DateTime createdDate, DateTime dueDate, string status, string severityLevel, int? taskReviewId)
        {
            Id = id;
            IssueCode = issueCode;
            IssueTitle = issueTitle;
            CreatedDate = createdDate;
            DueDate = dueDate;
            Status = status;
            SeverityLevel = severityLevel;
            TaskReviewId = taskReviewId;
        }

    }
}
