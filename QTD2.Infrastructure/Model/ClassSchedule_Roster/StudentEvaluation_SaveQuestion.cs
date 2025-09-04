using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class StudentEvaluation_SaveQuestion
    {
        public int EvaluationId { get; set; }
        public int QuestionId { get; set; }
        public int ClassId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Notes { get; set; }
    }

    public class StudentEvaluationSubmitOptions
    {
        public int EvaluationId { get; set; }
        public int ClassId { get; set; }
        public int EmployeeId { get; set; }
    }
}
