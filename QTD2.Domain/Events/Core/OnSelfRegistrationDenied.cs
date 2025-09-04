using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnSelfRegistrationDenied : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Employee ClassScheduleEmployee { get; }

        public OnSelfRegistrationDenied(ClassSchedule_Employee classScheduleEmployee)
        {
            ClassScheduleEmployee = classScheduleEmployee ;
        }
    }
}
