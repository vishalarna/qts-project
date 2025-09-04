using MediatR;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Roster_TestCompleted : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Roster ClassSchedule_Roster { get; }
        public OnClassSchedule_Roster_TestCompleted(ClassSchedule_Roster classSchedule_Roster)
        {
            ClassSchedule_Roster =  classSchedule_Roster;
        }
    }
}
