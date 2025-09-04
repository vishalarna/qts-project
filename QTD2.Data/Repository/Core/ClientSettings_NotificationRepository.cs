using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClientSettings_NotificationRepository : Common.Repository<ClientSettings_Notification>, IClientSettings_NotificationRepository
    {
        public ClientSettings_NotificationRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
