using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ActivityNotificationRepository : Common.Repository<ActivityNotification>, IActivityNotificationRepository
    {
        public ActivityNotificationRepository(QTDContext context)
            : base(context)
        {
        }
    }
}