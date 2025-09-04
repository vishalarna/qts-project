using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseEvaluation
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
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
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Level4EvaluationCompletionDate { get; set; }
        public bool? Behaviour90DemailSent { get; set; }
        public bool? Behaviour60DemailSent { get; set; }
        public bool? Behaviour30DemailSent { get; set; }
        public bool? Results90DemailSent { get; set; }
        public bool? Results60DemailSent { get; set; }
        public bool? Results30DemailSent { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
    }
}
