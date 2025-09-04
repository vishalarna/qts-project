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

namespace QTD2.Application.EventHandlers.Core
{
   public class OnTaskQualificationCompleted_Handler : INotificationHandler<OnTaskQualificationCompleted>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly ITaskQualificationService _taskQualificationService;


        public OnTaskQualificationCompleted_Handler(
            Domain.Interfaces.Service.Core.INotificationService notificationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            ITaskQualificationService taskQualificationService)
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
            _taskQualificationService = taskQualificationService;
        }

        public async Task Handle(OnTaskQualificationCompleted notification, CancellationToken cancellationToken)
        {
            if(notification.TaskQualification.ClassScheduleId != null && notification.TaskQualification.Sequence != null)
            {
                var customSequence = notification.TaskQualification.Sequence + 1;
                var taskQualification = await _taskQualificationService.GetTaskQualificationAsync(notification.TaskQualification.ClassScheduleId,notification.TaskQualification.EmpId, customSequence);
                if(taskQualification != null)
                {
                    taskQualification.IsReleasedToEMP = true;
                    await _taskQualificationService.UpdateAsync(taskQualification);
                }
            }
            else
            {
                return;
            }
        }
    }
}
