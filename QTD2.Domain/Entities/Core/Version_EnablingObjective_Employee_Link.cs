using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Employee_Link : Common.Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Employee Version_Employee { get; set; }

        public Version_EnablingObjective_Employee_Link(Version_EnablingObjective eo,Version_Employee emp,DateTime startDate, string version_number = "1.0")
        {
            Version_EmployeeId = emp.Id;
            Version_EnablingObjectiveId = eo.Id;
            Version_Number = version_number;
            StartDate = startDate;
            Version_EnablingObjective = eo;
            Version_Employee = emp;
        }

        public Version_EnablingObjective_Employee_Link()
        {
        }
    }
}
