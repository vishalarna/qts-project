using System;
using System.Threading.Tasks;
using System.Linq;

using QTD2.Domain.Entities.Authentication;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using QTD2.Infrastructure.Notification.Settings;
using Microsoft.Extensions.Options;

namespace QTD2.Application.Services.Authentication
{
    public class NotificationService : Interfaces.Services.Authentication.INotificationService
    {
        private readonly Infrastructure.Notification.Interfaces.INotificationFactory _notificationFactory;
        private readonly Infrastructure.Notification.Interfaces.INotifierFactory _notifierFactory;
        private readonly NotificationSettings _notificationSettings;
        private Infrastructure.Notification.Interfaces.INotifier _notifier;

        public NotificationService(
            Infrastructure.Notification.Interfaces.INotificationFactory notificationFactory,
            Infrastructure.Notification.Interfaces.INotifierFactory notifierFactory,
             IOptions<NotificationSettings> notificationSettingOptions
            )
        {
            _notificationFactory = notificationFactory;
            _notifierFactory = notifierFactory;
            _notificationSettings = notificationSettingOptions.Value;
        }

        public async Task<bool> Send2FANotificationAsync(string emailAddress, string verificationToken)
        {
            var notification = _notificationFactory.Create2FANotification(emailAddress, verificationToken);
            return await sendNotificationAsync(notification);
        }
        public async Task<bool> SendResetPasswordNotificationAsync(string emailAddress, string url)
        {
            var notification = _notificationFactory.CreateForgotPassword(emailAddress, url);
            return await sendNotificationAsync(notification);
        }

        public async Task<bool> SendAccountLockedNotificationAsync(string emailAddress, string url)
        {
            var notification = _notificationFactory.CreateAccountLockedNotification(emailAddress, url);
            var defaultFromEmail =  _notificationSettings.MimeKitEmailSettings.DefaultFrom ;
            notification.Destination.Add(defaultFromEmail);
            return await sendNotificationAsync(notification);
        }

        protected async Task<bool> sendNotificationAsync(Infrastructure.Notification.Interfaces.INotification notification)
        {
            _notifier = _notifierFactory.GetNotifier(notification);
            return await _notifier.NotifyAsync(notification);
        }
    }
}
