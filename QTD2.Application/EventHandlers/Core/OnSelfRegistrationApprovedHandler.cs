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
    public class OnSelfRegistrationApprovedHandler : INotificationHandler<OnSelfRegistrationApproved>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        public OnSelfRegistrationApprovedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService, IClientSettings_NotificationService clientSettings_NotificationService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
        }

        public async Task Handle(OnSelfRegistrationApproved notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Self-Registration Approval");

            if (!clientSettings_Notification.Enabled) return;

            Domain.Entities.Core.EMPSelfRegistrationApprovalNotification selfRegistrationApprovalNotification = new Domain.Entities.Core.EMPSelfRegistrationApprovalNotification(DateTime.Now.ToUniversalTime(), notification.ClassScheduleEmployee.Id, notification.ClassScheduleEmployee.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(selfRegistrationApprovalNotification);
            //save to db
        }
    }
}
