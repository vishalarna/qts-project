using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClientSettings_NotificationService : Common.IService<ClientSettings_Notification>
    {
        System.Threading.Tasks.Task<ClientSettings_Notification> GetClientSettingNotificationByName(string name);

        System.Threading.Tasks.Task<ClientSettings_Notification> GetClientSettingNotificationByNameWithoutIncludes(string name);

        System.Threading.Tasks.Task<List<ClientSettings_Notification>> GetClientNotificationSettings();
        System.Threading.Tasks.Task<ClientSettings_Notification> GetCertificationExpirationNotification();
    }
}
