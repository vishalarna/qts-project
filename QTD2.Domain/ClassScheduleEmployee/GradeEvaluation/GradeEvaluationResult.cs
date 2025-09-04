using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.ClassScheduleEmployee.GradeEvaluation
{
    public class GradeEvaluationResult
    {
        public GradeEvaluationResult(double? score, string grade, DateTime? completionDate)
        {
            Grade = grade;
            Score = score;
            CompletionDate = completionDate;
        }

        public GradeEvaluationResult() { }

        public bool IsComplete { get { return Grade == "P" && CompletionDate.HasValue; } }
        public double? Score { get; set; }
        public int? ScoreAsInt { get { return Score.HasValue ? (int)Score.Value : null; } }
        public string Grade { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
