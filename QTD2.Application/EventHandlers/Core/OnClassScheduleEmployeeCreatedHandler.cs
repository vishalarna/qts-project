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
    public class OnClassScheduleEmployeeCreatedHandler : INotificationHandler<OnClassScheduleEmployeeCreated>
    {
        private readonly INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;

        public OnClassScheduleEmployeeCreatedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService, IClientSettings_NotificationService clientSettings_NotificationService)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
        }

        public async Task Handle(OnClassScheduleEmployeeCreated notification, CancellationToken cancellationToken)
        {
            
        }
    }
}
