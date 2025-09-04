using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Notification.Interfaces;

namespace QTD2.Infrastructure.Notification
{
    public class DefaultNotifierFactory : INotifierFactory
    {
        protected Settings.NotificationSettings _notificationSettings { get; set; }

        private ILogger<MimeKitEmailNotifier> _logger;

        public DefaultNotifierFactory(IOptions<Settings.NotificationSettings> options, ILogger<MimeKitEmailNotifier> logger)
        {
            _notificationSettings = options.Value;
            _logger = logger;
        }

        public INotifier GetNotifier(INotification notification)
        {
            if (notification.Method == NotificationMethod.Email)
            {
                return new MimeKitEmailNotifier(_notificationSettings.MimeKitEmailSettings, _logger);
            }

            if (notification.Method == NotificationMethod.SMS)
            {
                return new SmsNotifier(_notificationSettings.SmsSettings);
            }

            throw new QTDServerException("Unknown Notification Method");
        }
    }
}
