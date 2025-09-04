using QTD2.Infrastructure.Notification.Interfaces;
using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Notification.Notifications
{
    public class EmailNotification : INotification
    {
        public NotificationMethod Method { get; private set; }
        public string Content { get; private set; }
        public List<string> Destination { get; private set; }
        public string Subject { get; private set; }
        public List<Attachment> Attachments { get; set; }

        public EmailNotification(string content, string subject, NotificationMethod method, List<string> destination)
        {
            Subject = subject;
            Content = content;
            Method = method;
            Destination = destination;
            Attachments = new List<Attachment>();
        }

        public void AddAttachment(string file)
        {
            if (Attachments == null) Attachments = new List<Attachment>();

            Attachments.Add(new Attachment(file));
        }
        public void SetContent(string content)
        {
            Content = content;
        }
    }
}
