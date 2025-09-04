using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluationQuestion : Entity
    {
        public int EvalFormID { get; set; }

        public string QuestionText { get; set; }

        public int QuestionNumber { get; set; }

        public string QuestionImage { get; set; }

        public byte[] QuestionFiles { get; set; }

        public virtual StudentEvaluationForm StudentEvaluationForm { get; set; }

        public virtual List<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; } = new List<StudentEvaluationWithoutEmp>();

        public StudentEvaluationQuestion(int evalFormID, string questionText, int questionNumber, string questionImage, byte[] questionFile)
        {
            EvalFormID = evalFormID;
            QuestionText = questionText;
            QuestionNumber = questionNumber;
            QuestionImage = questionImage;
            QuestionFiles = questionFile;
        }

        public StudentEvaluationQuestion()
        {
        }
    }
}
