using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Roster_Deleted : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Roster ClassSchedule_Roster { get; }

        public OnClassSchedule_Roster_Deleted(ClassSchedule_Roster classSchedule_Roster)
        {
            ClassSchedule_Roster = classSchedule_Roster;
        }
    }
}
