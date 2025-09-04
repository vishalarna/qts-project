using System;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluationHistory : Common.Entity
    {
        public int StudentEvaluationId { get; set; }

        public string StudentEvaluationNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual StudentEvaluation StudentEvaluation { get; set; }

        public StudentEvaluationHistory()
        {
                
        }
        public StudentEvaluationHistory(int studentEvaluationId, string studentEvaluationNotes, DateTime? effectiveDate)
        {
            StudentEvaluationId = studentEvaluationId;
            StudentEvaluationNotes = studentEvaluationNotes;
            EffectiveDate = effectiveDate;
        }
    }
}
