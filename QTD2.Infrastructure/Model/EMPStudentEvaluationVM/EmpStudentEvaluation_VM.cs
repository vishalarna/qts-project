using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EMPStudentEvaluationVM
{
    public class EmpStudentEvaluation_VM
    {
        public string ilaTitle { get; set; }
        public DateTime? dueDate { get; set; }
        public bool isCompleted { get; set; }
        public string status { get; set; }
        public int classSchedule_Evaluation_RosterId { get; set; }
        public bool isStarted { get; set; }
        public int number { get; set; }
        public string providerName { get; set; }
        public string ilaTitleOnly { get; set; }
        public string instructorName { get; set; }
        public string ilaNumber { get; set; }
        public string locationName { get; set; }
        public DateTime? completionDate { get; set; }
        public int? employeeId { get; set; }
        public int? evaluationId { get; set; }
        public int? classScheduleId { get; set; }
        public string evaluationTitle { get; set; }
        public DateTime? classStartDate { get; set; }
        public DateTime? classEndDate { get; set; }
        public bool? isAllowed { get; set; }
        public int? iLAID { get; set; }
    }
}
