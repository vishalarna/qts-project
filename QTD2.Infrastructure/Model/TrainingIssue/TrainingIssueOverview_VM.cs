using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
   public class TrainingIssueOverview_VM
    {
        public int TrainingIssuesOpen { get; set; }
        public int TrainingIssuesClosed { get; set; }
        public int TrainingIssuesWithPendingActionItems { get; set; }
        public int TrainingIssuesWithNoActionItems { get; set; }
        public List<TrainingIssueOverview_TrainingIssue_VM> TrainingIssues { get; set; } = new List<TrainingIssueOverview_TrainingIssue_VM>();
        public TrainingIssueOverview_VM()
        {

        }

        public TrainingIssueOverview_VM(int trainingIssuesOpen,int trainingIssuesClosed, int trainingIssuesWithPendingActionItems, int trainingIssuesWithNoActionItems)
        {
            TrainingIssuesOpen = trainingIssuesOpen;
            TrainingIssuesClosed = trainingIssuesClosed;
            TrainingIssuesWithPendingActionItems = trainingIssuesWithPendingActionItems;
            TrainingIssuesWithNoActionItems = trainingIssuesWithNoActionItems;
        }
    }
}
