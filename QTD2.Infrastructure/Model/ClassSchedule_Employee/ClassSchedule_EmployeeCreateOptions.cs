using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Employee
{
    public class ClassSchedule_EmployeeCreateOptions
    {
        public int classScheduleId { get; set; }

        public int[] employeeIds { get; set; }
        public bool Enroll { get; set; } = true;
        public DateTime? PlannedDate { get; set; }
    }
}
