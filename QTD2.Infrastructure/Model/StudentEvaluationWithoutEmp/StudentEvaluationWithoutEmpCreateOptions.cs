using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluationWithoutEmp
{
    public class StudentEvaluationWithoutEmpCreateOptions
    {
        public int StudentEvaluationId { get; set; }
        public int ClassScheduleId { get; set; }
        public string DataMode { get; set; }
        public string AdditionalComments { get; set; }

        public int? EmpId { get; set; }

        public List<StudentEval> studentEvalData { get; set; }


        public StudentEvaluationWithoutEmpCreateOptions()
        {
            studentEvalData = new List<StudentEval>();
        }
    }

    public class StudentEval
    {
        public int QuestionId { get; set; }
        public Nullable<int> RatingScale { get; set; }

        public double High { get; set; }
        public double Average { get; set; }
        public double Low { get; set; }
        public string Notes { get; set; }

    }
}
