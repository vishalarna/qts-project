using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingIssue : Common.Entity
    {
        public string IssueID { get; set; }
        public string IssueTitle { get; set; }
        public string? Description { get; set; }
        public DateTime TrainingIssueCreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public int SeverityId { get; set; }
        public int? DriverTypeId { get; set; }
        public int? DriverSubTypeId { get; set; }
        public string? OtherComments { get; set; }
        public int? DataElementId { get; set; }
        public int? TaskReviewId { get; set; }
        public List<TrainingIssue_ActionItem> ActionItems { get; set; } = new List<TrainingIssue_ActionItem>();
        public string? PlannedResponse { get; set; }
        public TrainingIssue_Status Status { get; set; }
        public TrainingIssue_Severity Severity { get; set; }
        public TrainingIssue_DriverType DriverType { get; set; }
        public TrainingIssue_DriverSubType DriverSubType { get; set; }
        public TrainingIssue_DataElement DataElement { get; set; }

        public TrainingIssue(string issueID, string issueTitle, string? description, DateTime trainingIssueCreatedDate, DateTime dueDate, int statusId, int severityId, int? taskReviewId)
        {
            IssueID = issueID;
            IssueTitle = issueTitle;
            Description = description;
            TrainingIssueCreatedDate = trainingIssueCreatedDate;
            DueDate = dueDate;
            StatusId = statusId;
            SeverityId = severityId;
            TaskReviewId = taskReviewId;
        }
        public TrainingIssue() { }

        public void UpdateDataElement(TrainingIssue_DataElement dataElement)
        {
            DataElementId = dataElement?.Id;
            DataElement = dataElement;
        }

        public void UpdateActionItems()
        {
            if (ActionItems.Where(x => !x.Deleted).All(x => x.StatusId == 3))
            {
                StatusId = 2;
            }
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as TrainingIssue;

            copy.IssueTitle = "Copy - " + this.IssueTitle;
            copy.IssueID = this.IssueID + " - Copy";
            copy.StatusId = this.StatusId;

            foreach (var actionItem in ActionItems)
            {
                var trainingIssue_ActionItem = actionItem.Copy<TrainingIssue_ActionItem>(createdBy);
                trainingIssue_ActionItem.Id = 0;
                copy.ActionItems.Add(trainingIssue_ActionItem);
            }
            if(this.DataElement != null)
            {
                var dataElement = this.DataElement.CopyDataElement<TrainingIssue_DataElement>(createdBy);
                dataElement.Id = 0;
                dataElement.TrainingIssueId = 0;
                copy.DataElement = dataElement;
            }
            else
            {
                copy.DataElementId = null;
            }
            return (T)(object)copy;
        }

        public override void Delete()
        {
            base.Delete();
            ActionItems.ForEach(x => x.Delete());
            DataElement?.Delete();
        }
    }
}
