namespace QTD2.Infrastructure.Notification.Content
{
    public interface IContentGeneratorFactory
    {
        IContentGenerator GetGenerator(NotificationMethod notificationMethod);
    }
}
