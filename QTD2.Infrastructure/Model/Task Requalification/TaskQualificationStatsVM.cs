using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TaskQualificationStatsVM
    {
        public int WithoutPositionQualDateCount { get; set; }

        public int PendingRequalificationCount { get; set; }

        public int WithoutTaskQualificationCount { get; set; }

        public int TaskQualificationEvaluatorCount { get; set; }
    }
}
