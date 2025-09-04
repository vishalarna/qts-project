using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings
{
    public class ClassScheduleEvaluatorLinksVM
    {
        public int ClassScheduleId { get; set; }
        public int[] EvaluatorIds { get; set; }

        public ClassScheduleEvaluatorLinksVM() { }

    }
}
