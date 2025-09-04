using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.IDP
{
    public class IDPClass
    {

        public int classScheduleId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string location { get; set; }
        public bool IsEnrolled { get; set; }
        public string InstructorName { get; set; }
    }
}
