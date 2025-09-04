using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface INotificationRecipietService : Common.IService<NotificationRecipiet>
    {
        public Task<List<NotificationRecipiet>> GetForPersonAndTypeInLast24HoursAsync(int personId, int clientSettingsNotificationStepId);
    }
}
