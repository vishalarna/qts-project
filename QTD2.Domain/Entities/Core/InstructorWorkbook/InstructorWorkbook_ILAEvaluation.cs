using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class InstructorWorkbook_ILAEvaluation : Common.Entity
    {
        public int ILAId { get; set; }
        public string StudentEvaluationResults { get; set; }
        public string InstructorFeedback { get; set; }
        public string Level1Status { get; set; }
        public DateTime? Level1StatusLastUpdatedAt { get; set; }
        public int? Level1StatusLastUpdatedBy { get; set; }
        public string Notes { get; set; }
        public string Level2Status { get; set; }
        public DateTime? Level2StatusLastUpdatedAt { get; set; }
        public int? Level2StatusLastUpdatedBy { get; set; }
        public string OpenTextField { get; set; }
        public string Level3Status { get; set; }
        public DateTime? Level3StatusLastUpdatedAt { get; set; }
        public int? Level3StatusLastUpdatedBy { get; set; }
        public DateTime? EvaluationCompletionDate { get; set; }
        public string Level4TextField { get; set; }
        public string Level4TextStatus { get; set; }
        public DateTime? Level4StatusLastUpdatedAt { get; set; }
        public int? Level4StatusLastUpdatedBy { get; set; }
        public string EvaluationResult { get; set; }
        public bool? SubmitForReview { get; set; }
        public DateTime? Level4EvaluationCompletionDate { get; set; }
        public bool? Behaviour90DEmailSent { get; set; }
        public bool? Behaviour60DEmailSent { get; set; }
        public bool? Behaviour30DEmailSent { get; set; }
        public bool? Results90DEmailSent { get; set; }
        public bool? Results60DEmailSent { get; set; }
        public bool? Results30DEmailSent { get; set; }
        
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
