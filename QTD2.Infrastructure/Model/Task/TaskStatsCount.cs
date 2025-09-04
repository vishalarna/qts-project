using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskStatsCount
    {
        public int ActiveTask { get; set; }

        public int InActiveTask { get; set; }

        public int Positions { get; set; }

        public int ILA { get; set; }

        public int EnablingObjectives { get; set; }

        public int Procedures { get; set; }

        public int SafetyHazards { get; set; }

        public int Regulations { get; set; }

        public int TrainingGroups { get; set; }
    }
}
