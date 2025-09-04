using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationCreateOptions
    {
        public int TaskId { get; set; }

        public int EmpId { get; set; }

        public int? EvaluationId { get; set; }

        public int[] EvaluatorIds { get; set; }

        public DateTime? TaskQualificationDate { get; set; }

        public string TaskQualificationEvaluator { get; set; }

        public DateTime? DueDate { get; set; }

        public bool CriteriaMet { get; set; }

        public string Comments { get; set; }
    }
}
