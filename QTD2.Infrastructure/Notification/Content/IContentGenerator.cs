namespace QTD2.Infrastructure.Notification.Content
{
    public interface IContentGenerator
    {
        string GetContent(string templatePath, object model);
        string GetContent(string templateName, string template, object model);
    }
}
