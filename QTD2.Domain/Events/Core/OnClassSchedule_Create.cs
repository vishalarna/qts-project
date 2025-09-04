using MediatR;
using QTD2.Domain.Entities.Core;


namespace QTD2.Domain.Events.Core
{
	public class OnClassSchedule_Create : Common.IDomainEvent, INotification
	{
		public ClassSchedule ClassSchedule { get; }
		public int ILAId { get; }
		public OnClassSchedule_Create(ClassSchedule classSchedule, int ilaId)
		{
			ClassSchedule = classSchedule;
			ILAId = ilaId;
		}
	}
}
