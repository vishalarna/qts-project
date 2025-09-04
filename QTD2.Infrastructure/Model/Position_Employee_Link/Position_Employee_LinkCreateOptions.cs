using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position_Employee
{
    public class Position_Employee_LinkCreateOptions
    {
        public int PositionId { get; set; }

        public int[] EmployeeIds { get; set; }

        public DateOnly StartDate { get; set; }

        public bool Trainee { get; set; }
    }
}
