namespace QTD2.Infrastructure.Notification.Interfaces
{
    public interface INotifierFactory
    {
        INotifier GetNotifier(INotification notification);
    }
}
