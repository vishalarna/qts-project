using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgramReview_TrainingIssue_Link : Entity
    {
        public int TrainingProgramId { get; set; }
        public int TrainingIssueId { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }
        public virtual TrainingIssue TrainingIssue { get; set; }

        public TrainingProgramReview_TrainingIssue_Link()
        {
        }

        public TrainingProgramReview_TrainingIssue_Link(int trainingProgramId, int trainingIssueId)
        {
            TrainingProgramId = trainingProgramId;
            TrainingIssueId = trainingIssueId;
        }

        public TrainingProgramReview_TrainingIssue_Link(int trainingProgramId, int trainingIssueId, TrainingProgram trainingProgram, TrainingIssue trainingIssue)
        {
            TrainingProgramId = trainingProgramId;
            TrainingIssueId = trainingIssueId;
            TrainingProgram = trainingProgram;
            TrainingIssue = trainingIssue;
        }
    }
}
