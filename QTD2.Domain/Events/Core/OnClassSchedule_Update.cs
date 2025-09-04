using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Update : Common.IDomainEvent, INotification
    {
        public ClassSchedule ClassSchedule { get; }
        public OnClassSchedule_Update(ClassSchedule classSchedule)
        {
            ClassSchedule = classSchedule;
        }
    }
}