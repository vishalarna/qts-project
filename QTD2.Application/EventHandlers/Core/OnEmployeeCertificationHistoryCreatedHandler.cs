using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnEmployeeCertificationHistoryCreatedHandler : INotificationHandler<OnEmployeeCertificationHistoryCreated>
    {
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly INotificationService _notificationService;
        public OnEmployeeCertificationHistoryCreatedHandler(IClientSettings_NotificationService clientSettings_NotificationService, INotificationService notificationService)
        {
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _notificationService = notificationService;
        }

        public async System.Threading.Tasks.Task Handle(OnEmployeeCertificationHistoryCreated notification, CancellationToken cancellationToken)
        {

        }
    }
}
