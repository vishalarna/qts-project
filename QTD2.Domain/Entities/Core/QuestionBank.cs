using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Domain.Entities.Core
{
    public class QuestionBank : Common.Entity
    {
        public string Stem { get; set; }
        public virtual ICollection<QuestionBankHistory> QuestionBankHistories { get; set; } = new List<QuestionBankHistory>();
        public virtual ICollection<StudentEvaluation_Question> StudentEvaluationQuestions { get; set; } = new List<StudentEvaluation_Question>();
        public virtual ICollection<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; } = new List<StudentEvaluationWithoutEmp>();

        public QuestionBank()
        {

        }
        public QuestionBank(string stem)
        {
            Stem = stem;
        }
    }
}
