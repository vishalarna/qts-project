using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnTestReleased : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Roster ClassScheduleRoster { get; }

        public OnTestReleased(ClassSchedule_Roster classScheduleRoster)
        {
            ClassScheduleRoster = classScheduleRoster;
        }
    }
}
