using System;
using System.Threading.Tasks;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Notification.Interfaces;

namespace QTD2.Infrastructure.Notification
{
    public class SmsNotifier : INotifier
    {
        private readonly Settings.SmsSettings _notificationSettings;

        public SmsNotifier(Settings.SmsSettings notificationSettings)
        {
            _notificationSettings = notificationSettings;
        }

        public Task<bool> NotifyAsync(INotification notification)
        {
            throw new QTDServerException("SMS Notifications are not hooked up yet.");
        }
    }
}
