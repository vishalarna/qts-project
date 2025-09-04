using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ScheduleClassesStats
    {
        public int NeedingApproval { get; set; }

        public int NeedReRelease { get; set; }

        public int NeedScheduling { get; set; }

        public int Waitlist { get; set; }

        public int RetakeRelease { get; set; }
    }
}
