using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationRecentVM
    {
        public int? Id { get; set; }

        public int EmpId { get; set; }

        public int TaskId { get; set; }

        public string TaskNumber { get;set; }

        public string EmpName { get; set; }

        public string EmpImage { get; set; }

        public string EmpEmail { get; set; }

        public DateTime? EmpReleaseDate { get; set; }

        public DateTime? QualificationDate { get; set; }

        public string EvaluatorName { get; set; }

        public DateTime? DueDate { get; set; }

        public bool CriteriaMet { get; set; }

        public string Comments { get; set; }

        public string TotalRequiredTaskRequalifications { get; set; }

        public string Status { get; set; }

        public List<string> PosIds { get; set; } = new List<string>();
    }
}
