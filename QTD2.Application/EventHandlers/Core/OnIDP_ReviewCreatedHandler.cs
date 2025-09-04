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
    public class OnIDP_ReviewCreatedHandler : INotificationHandler<OnIDP_ReviewCreated>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        public OnIDP_ReviewCreatedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService, IClientSettings_NotificationService clientSettings_NotificationService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
        }

        public async Task Handle(OnIDP_ReviewCreated notification, CancellationToken cancellationToken)
        {
            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP IDP Review");

            if (!clientSettings_Notification.Enabled) return;

            Domain.Entities.Core.EMPIdpReviewNotification idpReviewNotification = new Domain.Entities.Core.EMPIdpReviewNotification(notification.IDP_Review.ReleaseDate.HasValue ? notification.IDP_Review.ReleaseDate.Value : DateTime.Now.ToUniversalTime(),notification.IDP_Review.Id ,notification.IDP_Review.Employee.Id, notification.IDP_Review.Employee.PersonId, clientSettings_Notification.Steps.First().Id);
            await _notificationService.AddAsync(idpReviewNotification);
        }
    }
}
