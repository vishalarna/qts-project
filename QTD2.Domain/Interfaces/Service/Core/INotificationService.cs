using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
  public  interface INotificationService : Common.IService<Notification>
    {
        public Task<List<Notification>> GetDueNotificationsAsync();
        Task<List<Notification>> GetPendingNotificationsByClientSettingsNotificationStepIdAsync(int clientSettingsNotificationTypeId);
    }
}
