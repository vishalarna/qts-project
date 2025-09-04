using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Employee_Task
{
    public class EmployeeTaskVM
    {
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }

        public string EmployeeName { get; set; }

        public string EmpNumber { get; set; }

        public string EmpEmail { get; set; }

        public string PositionName { get; set; }

        public DateTime? LastQualification { get; set; }

        public string QualificationStatus { get; set; }

        public bool Active { get; set; }
    }
}
