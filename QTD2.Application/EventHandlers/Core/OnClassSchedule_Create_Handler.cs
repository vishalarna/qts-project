using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Entities.Core;


namespace QTD2.Application.EventHandlers.Core
{
	public class OnClassSchedule_Create_Handler : INotificationHandler<OnClassSchedule_Create>
	{
        private readonly IClassScheduleService _classScheduleService;
        private readonly IILAService _ilaService;

        public OnClassSchedule_Create_Handler(Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IClassScheduleService classScheduleService,
			IILAService ilaService

			)
        {
            _classScheduleService = classScheduleService;
			_ilaService = ilaService;

		}

        public async System.Threading.Tasks.Task Handle(OnClassSchedule_Create notification, CancellationToken cancellationToken)
        {
            await CreateClassScheduleStudentEvaluationLink(notification);
        }

        async System.Threading.Tasks.Task CreateClassScheduleStudentEvaluationLink(OnClassSchedule_Create notification)
		{
			var ila = await _ilaService.GetWithILAEvalsAsync(notification.ILAId);

			if (ila.ILA_StudentEvaluation_Links.Count > 0)
			{
				foreach (var ilaStudentEvaluationLink in ila.ILA_StudentEvaluation_Links) {
					notification.ClassSchedule.LinkStudentEvaluation(ilaStudentEvaluationLink.StudentEvaluationForm);
				}

				if (notification.ClassSchedule.Id != null)
				{
					await _classScheduleService.UpdateAsync(notification.ClassSchedule);
				}
			}
			else
			{
				return;
			}
		}
    }
}
