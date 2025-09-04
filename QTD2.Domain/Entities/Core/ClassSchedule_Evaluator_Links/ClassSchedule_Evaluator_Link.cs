using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Evaluator_Link : Common.Entity
    {
        public int ClassScheduleId { get; set; }
        public int EvaluatorId { get; set; }
        public virtual ClassSchedule ClassSchedule { get; set; }
        public virtual Employee Evaluator { get; set; }

        public ClassSchedule_Evaluator_Link()
        {
        }

        public ClassSchedule_Evaluator_Link(int classScheduleId, int evaluatorId)
        {
            ClassScheduleId = classScheduleId;
            EvaluatorId = evaluatorId;
        }
    }
}
