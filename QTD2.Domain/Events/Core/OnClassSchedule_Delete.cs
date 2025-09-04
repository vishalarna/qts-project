using MediatR;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Events.Core
{
   public class OnClassSchedule_Delete : Common.IDomainEvent, INotification
    {
        public ClassSchedule ClassSchedule { get; }
        public OnClassSchedule_Delete(ClassSchedule classSchedule)
        {
            ClassSchedule = classSchedule;
        }
    }
}
