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
    public class OnClientSetting_Notification_Step_ChangedHandler : INotificationHandler<OnClientSetting_Notification_Step_Changed>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationDomainService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly Domain.Interfaces.Service.Core.IEmployeeCertificationService _employeeCertificationService;

        public OnClientSetting_Notification_Step_ChangedHandler(
            QTD2.Domain.Interfaces.Service.Core.INotificationService notificationDomainService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            Domain.Interfaces.Service.Core.IEmployeeCertificationService employeeCertificationService
        )
        {
            _notificationDomainService = notificationDomainService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _employeeCertificationService = employeeCertificationService;
        }

        public async Task Handle(OnClientSetting_Notification_Step_Changed notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetAsync(notification.ClientSettings_Notification_Step.ClientSettingsNotificationId);

            //if ClientSetting_Notification is CertificationExpiration
            if (clientSettings_Notification.Name != "Certification Expiration") return;

            var pendingNotifications = await _notificationDomainService.GetPendingNotificationsByClientSettingsNotificationStepIdAsync(notification.ClientSettings_Notification_Step.Id);

            foreach(var pendingNotification in pendingNotifications)
            {
                //Reject all pending emails from the step
                pendingNotification.Reject("Certification Expiration Settings Changed");
            }
        }
    }
}
