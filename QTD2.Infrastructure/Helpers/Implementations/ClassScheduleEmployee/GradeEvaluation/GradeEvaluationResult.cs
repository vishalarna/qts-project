using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Helpers.Implementations.ClassScheduleEmployee.GradeEvaluation
{
    public class GradeEvaluationResult
    {
        public bool IsComplete { get; set; }
        public int Score { get; set; }
        public string Grade { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
