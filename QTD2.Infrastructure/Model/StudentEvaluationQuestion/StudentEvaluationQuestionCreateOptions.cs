using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluationQuestion
{
    public class StudentEvaluationQuestionCreateOptions
    {
        public int EvalFormID { get; set; }

        public string QuestionText { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionImage { get; set; }

        public byte[] QuestionFiles { get; set; }
    }
}
