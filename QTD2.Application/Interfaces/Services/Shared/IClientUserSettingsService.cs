using QTD2.Infrastructure.Model.ClientUserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClientUserSettingsService
    {
        System.Threading.Tasks.Task UpdateDashboardSettingsAsync(CustomizeDashboardUpdateOptions options);
        Task<List<ClientUserSettings_DashboardSetting>> GetDashboardSettingsAsync();
    }
}
