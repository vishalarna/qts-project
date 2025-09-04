using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Evaluator_Link : Common.IDomainEvent, INotification
    {
        public ClassSchedule ClassSchedule { get; }
        public Employee Evaluator { get; }

        public OnClassSchedule_Evaluator_Link(ClassSchedule classSchedule, Employee evaluator)
        {
            ClassSchedule = classSchedule;
            Evaluator = evaluator;
        }
    }
}
