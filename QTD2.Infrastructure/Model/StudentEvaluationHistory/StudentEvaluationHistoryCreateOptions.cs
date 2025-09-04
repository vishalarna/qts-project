using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluationHistory
{
    public class StudentEvaluationHistoryCreateOptions
    {
        public int StudentEvaluationId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string StudentEvaluationNotes { get; set; }

        public string ActionType { get; set; }
        public int[] StudentEvaluationIds { get; set; }
    }
}
