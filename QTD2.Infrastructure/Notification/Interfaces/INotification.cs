using System.Collections.Generic;

namespace QTD2.Infrastructure.Notification.Interfaces
{
    public interface INotification
    {
        List<string> Destination { get; }

        NotificationMethod Method { get; }

        string Content { get; }

        string Subject { get; }
    }
}
