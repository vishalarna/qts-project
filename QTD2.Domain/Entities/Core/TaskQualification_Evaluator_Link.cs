using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskQualification_Evaluator_Link : Common.Entity
    {
        public int EvaluatorId { get; set; }

        public int TaskQualificationId { get; set; }

        public int Number { get; set; }

        public virtual Employee Evaluator { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }

        public TaskQualification_Evaluator_Link(int evaluatorId, int taskQualificationId, int number)
        {
            EvaluatorId = evaluatorId;
            TaskQualificationId = taskQualificationId;
            Number = number;
        }

        public void LinkEvaluator()
        {
            AddDomainEvent(new Domain.Events.Core.OnTaskQualification_Evalutor_LinkCreated(this));
        }

        public TaskQualification_Evaluator_Link()
        {
        }
    }
}
