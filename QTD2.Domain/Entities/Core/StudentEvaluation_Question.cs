using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluation_Question : Common.Entity
    {
        public int StudentEvaluationId { get; set; }

        public int QuestionBankId { get; set; }

        public virtual StudentEvaluation StudentEvaluation { get; set; }

        public virtual QuestionBank QuestionBank { get; set; }

        public StudentEvaluation_Question(StudentEvaluation studentEvaluation, QuestionBank questionBank)
        {
            StudentEvaluationId = studentEvaluation.Id;
            QuestionBankId = questionBank.Id;
            StudentEvaluation = studentEvaluation;
            QuestionBank = questionBank;
        }
        public StudentEvaluation_Question()
        {
        }
    }
}
