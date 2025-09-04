using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster
{
    public class ClassRoasterUpdateOptions
    {
        public int? Score { get; set; }
        public string Grade { get; set; }
        public string BulkGrade { get; set; }
        public string TestItemType { get; set; }

        public int? TestId { get; set; }

        public string TestType { get; set; }

        public int? ClassId { get; set; }

        public DateTime? CompDate { get; set; }
      
        public int? RetakeOrder { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
