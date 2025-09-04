using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Employee : Entity
    {
        public int EmployeeId { get; set; }

        public int PersonId { get; set; }

        public string EmployeeNumber { get; set; }

        public string Version_Number { get; set; }

        public virtual Employee Employee { get; set; }

        public ICollection<Version_EnablingObjective_Employee_Link> Version_EnablingObjective_Employee_Links { get; set; } = new List<Version_EnablingObjective_Employee_Link>();

        public Version_Employee(Employee employee, string version_Number = "1.0")
        {
            PersonId = employee.PersonId;
            EmployeeNumber = employee.EmployeeNumber;
            Version_Number = version_Number;
        }

        public Version_Employee()
        {
        }
    }
}
