using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Employee
{
    public class ClassScheduleEMPWithGradesVM
    {
        public int EmpId { get; set; }

        public string Grade { get; set; }

        public int? Score { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool Disclaimer { get; set; }

        public bool Interrupted { get; set; }

        public bool Restarted { get; set; }

        public string EmpEmail { get; set; }

        public string EmployeeName { get; set; }

        public string Image { get; set; }
        public int? RetakeOrder { get; set; }
    }
}
