using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class RosterOverviewVM
    {
        public int ClassEmployeeId { get; set; }
        
        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeeImage { get; set; }

        public string PretestStatus { get; set; }
        
        public string CbtStatus { get; set; }
        
        public string TestStatus { get; set; }
        
        public int RetakeCount { get; set; }

        public int? Score { get; set; }
        
        public string Grade { get; set; }
        
        public string GradeNotes { get; set; }

        public bool? isTestReleased { get; set; }
        public bool? isPreTestReleased { get; set; }
        public bool? isCBTReleased { get; set; }

        public bool CompletedDate { get; set; }
        public DateTime? EvaluationCompletedDate { get; set; }
        public int ClassScheduleEmployeeId { get; set; }
        public bool TaskQualificationCompleted { get; set; }

    }
}
