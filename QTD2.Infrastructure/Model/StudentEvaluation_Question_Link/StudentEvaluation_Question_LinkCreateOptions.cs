using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluation_Question_Link
{
    public class StudentEvaluation_Question_LinkCreateOptions
    {
        public int StudentEvaluationId { get; set; }

        public bool isReordered { get; set; }

        public int[] QuestionIds { get; set; }
    }
}
