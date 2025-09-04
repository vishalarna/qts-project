using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Notification.Content.Models;
using QTD2.Infrastructure.Notification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnPublicClassScheduleRequestRejectedHandler : INotificationHandler<OnPublicClassScheduleRequestRejected>
    {
        private readonly Domain.Interfaces.Service.Core.INotificationService _notificationService;
        private readonly IClientSettings_NotificationService _clientSettings_NotificationService;
        private readonly IPublicClassScheduleRequestService _publicClassScheduleRequestService;
        private readonly IClientUserSettings_GeneralSettingService _clientUserSettings_GeneralSettingService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotifierFactory _notifierFactory;
        private INotifier _notifier;
        public OnPublicClassScheduleRequestRejectedHandler(Domain.Interfaces.Service.Core.INotificationService notificationService, IClientSettings_NotificationService clientSettings_NotificationService, IPublicClassScheduleRequestService publicClassScheduleRequestService, IClientUserSettings_GeneralSettingService clientUserSettings_GeneralSettingService, INotificationFactory notificationFactory, INotifierFactory notifierFactory)
        {
            _notificationService = notificationService;
            _clientSettings_NotificationService = clientSettings_NotificationService;
            _publicClassScheduleRequestService = publicClassScheduleRequestService;
            _clientUserSettings_GeneralSettingService = clientUserSettings_GeneralSettingService;
            _notificationFactory = notificationFactory;
            _notifierFactory = notifierFactory;
        }
        public async System.Threading.Tasks.Task Handle(OnPublicClassScheduleRequestRejected notification, CancellationToken cancellationToken)
        {
            var publicClassSchedule = await _publicClassScheduleRequestService.GetWithIncludeAsync(notification.PublicClassScheduleRequest.Id, new string[] { "ClassSchedule" });

            var clientSettings_Notification = await _clientSettings_NotificationService.GetClientSettingNotificationByName("Public Class Schedule Request Rejected");
            var generalSettings = await _clientUserSettings_GeneralSettingService.GetGeneralSettings();
            if (!clientSettings_Notification.Enabled) return;

            var declineNotification = _notificationFactory.CreatePublicClassScheduleRequestDeclineNotification(clientSettings_Notification, publicClassSchedule, generalSettings.DefaultTimeZone);
            await sendNotificationAsync(declineNotification);

        }

        protected async Task<bool> sendNotificationAsync(Infrastructure.Notification.Interfaces.INotification notification)
        {
            try
            {
                _notifier = _notifierFactory.GetNotifier(notification);
                return await _notifier.NotifyAsync(notification);
            }
            catch (Exception e)
            {
                // TODO Log
                var ex = e;
                return false;
            }
        }
    }
}
