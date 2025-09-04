using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnEmployeeCreatedEventHandler : INotificationHandler<OnNewEmployeeAdded>
    {
        private readonly INotificationService _notificationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;


        public OnEmployeeCreatedEventHandler(
            INotificationService notificationService, 
            IAuthorizationService authorizationService,
            IClientSettings_NotificationService clientSettings_NotificationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _notificationService = notificationService;
            _authorizationService = authorizationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(OnNewEmployeeAdded notification, CancellationToken cancellationToken)
        {
            var clientSettings_notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("EMP Login");

            if (!clientSettings_notification.Enabled) return;

            Domain.Entities.Core.EMPLoginNotification loginNotification = new Domain.Entities.Core.EMPLoginNotification(DateTime.Now.ToUniversalTime(), notification.Employee.Id, notification.Employee.PersonId, clientSettings_notification.Steps.First().Id);
            await _notificationService.AddAsync(loginNotification);
        }
    }
}
