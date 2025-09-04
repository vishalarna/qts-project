using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class ClassRoasterModel
    {
        
        public int ClassRoasterId { get; set; }

        public int ClassScheduleId { get; set; }

        public int TestId { get; set; }

        public int TestTypeId { get; set; }

        public int EmpId { get; set; }

        public bool Disclaimer { get; set; }

        public string Grade { get; set; }

        public bool Interrupted { get; set; }

        public bool Restarted { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? Score { get; set; }

        public List<int> empIDs { get; set; }

        public string EmpEmail { get; set; }

        public string EmployeeName { get; set; }

        public string Image { get; set; }

        public string TestItemType { get; set; }

    }
}
