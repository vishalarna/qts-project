using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Employee
{
    public class EmployeeOptions
    {
        public int[] employeeIds { get; set; }

        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }
    }
}
