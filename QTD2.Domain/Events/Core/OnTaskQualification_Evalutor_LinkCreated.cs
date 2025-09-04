using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnTaskQualification_Evalutor_LinkCreated : Common.IDomainEvent, INotification
    {
        public TaskQualification_Evaluator_Link TaskQualification_Evaluator_Link { get; }

        public OnTaskQualification_Evalutor_LinkCreated(TaskQualification_Evaluator_Link taskQualificationEvaluatorLink)
        {
            TaskQualification_Evaluator_Link = taskQualificationEvaluatorLink;
        }
    }
}
