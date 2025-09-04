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

namespace QTD2.Application.EventHandlers.Core
{
    public class OnClientSetting_Notification_DisabledHandler : INotificationHandler<OnClientSetting_Notification_Disabled>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationDomainService;
        public OnClientSetting_Notification_DisabledHandler(Domain.Interfaces.Service.Core.INotificationService notificationDomainService)
        {
            _notificationDomainService = notificationDomainService;
        }

        public async Task Handle(OnClientSetting_Notification_Disabled notification, CancellationToken cancellationToken)
        {
            //notification.ClientSettings_Notification

            foreach (var step in notification.ClientSettings_Notification.Steps)
            {
                var pendingNotifications = await _notificationDomainService.GetPendingNotificationsByClientSettingsNotificationStepIdAsync(step.Id);

                foreach (var pendingNotification in pendingNotifications)
                {
                    pendingNotification.Reject("Client Settings Notification Disabled");
                    await _notificationDomainService.UpdateAsync(pendingNotification);
                }
            }
        }
    }
}
