using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ClassScheduleEnrollOptions
    {
        public int EmployeeId { get;set; }
        public int ClassId { get;set; }
        public DateTime? PlannedDate { get; set; }
    }
}
