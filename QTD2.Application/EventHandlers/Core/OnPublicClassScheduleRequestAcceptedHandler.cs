using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnPublicClassScheduleRequestAcceptedHandler : INotificationHandler<OnPublicClassScheduleRequestAccepted>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IPublicClassScheduleRequestService _publicClassScheduleRequestService;

        public OnPublicClassScheduleRequestAcceptedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService, IClientSettings_NotificationService clientSettings_NotificationService, IPublicClassScheduleRequestService publicClassScheduleRequestService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _publicClassScheduleRequestService = publicClassScheduleRequestService;
        }

        public async System.Threading.Tasks.Task Handle(OnPublicClassScheduleRequestAccepted notification, CancellationToken cancellationToken)
        {
            var publicClassSchedule = await _publicClassScheduleRequestService.GetWithIncludeAsync(notification.PublicClassScheduleRequest.Id, new string[] { "ClassSchedule_Employee.Employee.Person" });

            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("Public Class Schedule Request Accepted");

            if (!clientSettings_Notification.Enabled) return;

            Domain.Entities.Core.PublicClassScheduleRequestAcceptedNotification publicClassScheduleRequestApprovalNotification = new Domain.Entities.Core.PublicClassScheduleRequestAcceptedNotification(DateTime.UtcNow.ToUniversalTime(), notification.PublicClassScheduleRequest.Id, publicClassSchedule.ClassSchedule_Employee.Employee.Person.Id, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(publicClassScheduleRequestApprovalNotification);
        }
    }
}
