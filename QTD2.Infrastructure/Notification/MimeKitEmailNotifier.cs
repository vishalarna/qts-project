using System;
using System.Threading.Tasks;
using MimeKit;
using QTD2.Infrastructure.Notification.Interfaces;
using QTD2.Infrastructure.Notification.Notifications;
using Microsoft.Extensions.Logging;

namespace QTD2.Infrastructure.Notification
{
    public class MimeKitEmailNotifier : INotifier
    {
        private readonly MimeMessage _message;
        private readonly Settings.MimeKitEmailSettings _notificationSettings;

        private ILogger<MimeKitEmailNotifier> _logger;

        public MimeKitEmailNotifier(Settings.MimeKitEmailSettings notificationSettings, ILogger<MimeKitEmailNotifier> logger)
        {
            _message = new MimeMessage();
            _notificationSettings = notificationSettings;
            _logger = logger;
        }

        public async Task<bool> NotifyAsync(INotification notification)
        {
            EmailNotification _notification = notification as EmailNotification;

            _message.Subject = _notification.Subject;
            _message.From.Add(MailboxAddress.Parse(_notificationSettings.DefaultFrom));

            foreach (var bcc in _notificationSettings.BCC)
            {
                if (!string.IsNullOrEmpty(bcc))
                    _message.Bcc.Add(MailboxAddress.Parse(bcc));
            }

            foreach (var destination in notification.Destination)
            {
                _message.To.Add(MailboxAddress.Parse(destination));
            }

            var builder = new BodyBuilder();
            builder.HtmlBody = notification.Content;

            foreach (var attachment in _notification.Attachments)
            {
                builder.Attachments.Add(attachment.SourceFile);
            }

            _message.Body = builder.ToMessageBody();

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.MessageSent += (sender, args) =>
                    {
                        // args.Response
                    };
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await smtp.ConnectAsync(_notificationSettings.Server, _notificationSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_notificationSettings.UserName, _notificationSettings.Password);
                    await smtp.SendAsync(_message);
                    await smtp.DisconnectAsync(true);
                }
            } catch(Exception ex)
            {
                string s = ex.Message;
                _logger.LogError($"Failed to send email {ex}", ex);
                return false;
            }


            return true;
        }
    }
}
