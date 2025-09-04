using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
   public class OnTaskQualificationCompleted : Common.IDomainEvent, INotification
    {
        public TaskQualification TaskQualification { get; }

        public OnTaskQualificationCompleted(TaskQualification taskQualification)
        {
            TaskQualification = taskQualification;
        }
    }
}
