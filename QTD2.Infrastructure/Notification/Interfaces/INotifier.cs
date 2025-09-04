using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Interfaces
{
    public interface INotifier
    {
        Task<bool> NotifyAsync(INotification notification);
    }
}
