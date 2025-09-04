using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class EnablingObjective_Employee_Link : Entity
    {
        public int EOID { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual Employee Employee { get; set; }

        public EnablingObjective_Employee_Link()
        {
        }

        public EnablingObjective_Employee_Link(EnablingObjective enablingObjective, Employee employee, DateTime startDate)
        {
            EnablingObjective = enablingObjective;
            Employee = employee;
            EOID = enablingObjective.Id;
            EmployeeId = employee.Id;
            StartDate = startDate;
        }
    }
}
