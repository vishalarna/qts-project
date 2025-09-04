using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClientUserSettings_Dashboard_SettingService : Common.IService<ClientUserSettings_DashboardSetting>
    {
        System.Threading.Tasks.Task<List<ClientUserSettings_DashboardSetting>> GetDashboardSettingsAsync(string username);
    }
}
