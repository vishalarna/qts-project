using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class InstructorWorkbook_ILAEvaluation_TrainingIssues : Common.Entity
    {
        public int? ILAId { get; set; }
        public string IssueTitle { get; set; }
        public string IssueDescription { get; set; }
        public string FeedbackType { get; set; }
        public string Severity { get; set; }
        public int? LevelNum { get; set; }
        //public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
