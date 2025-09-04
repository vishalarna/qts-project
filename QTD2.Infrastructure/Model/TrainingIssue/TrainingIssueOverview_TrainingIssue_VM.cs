using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
   public class TrainingIssueOverview_TrainingIssue_VM
    {
        public int Id { get; set; }
        public string IssueCode{ get; set; }
        public DateTime DueDate { get; set; }
        public string IssueTitle { get; set; }
        public string Severity { get; set; }
        public string DriverType { get; set; }
        public string PendingActionItems { get; set; }
        public string Status { get; set; }
        public string DriverSubType { get; set; }
        public bool Active { get; set; }
        public int? TaskReviewId { get; set; }

        public TrainingIssueOverview_TrainingIssue_VM(int id, string issueCode, DateTime dueDate, string issueTitle, string severity, string driverType, string driverSubType, string pendingActionItems, string status, bool active, int? taskReviewId)
        {
            Id = id;
            IssueCode = issueCode;
            DueDate = dueDate;
            IssueTitle = issueTitle;
            Severity = severity;
            DriverType = driverType;
            DriverSubType = driverSubType;
            PendingActionItems = pendingActionItems;
            Status = status;
            Active = active;
            TaskReviewId = taskReviewId;
        }

        public TrainingIssueOverview_TrainingIssue_VM() { }

    }
}
