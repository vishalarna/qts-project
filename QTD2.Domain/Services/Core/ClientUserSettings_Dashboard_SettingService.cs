using System;
using System.Linq;
using System.Collections.Generic;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Services.Core
{
    public class ClientUserSettings_Dashboard_SettingService : Common.Service<ClientUserSettings_DashboardSetting>, IClientUserSettings_Dashboard_SettingService
    {
        public ClientUserSettings_Dashboard_SettingService(IClientUserSettings_Dashboard_SettingRepository repository, IClientUserSettings_Dashboard_SettingValidation validation)
               : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<ClientUserSettings_DashboardSetting>> GetDashboardSettingsAsync(string username)
        {
            return (await Repository.FindWithIncludeAsync(r => r.ClientUser.Person.Username.ToUpper() == username.ToUpper(), new string[] { "DashboardSetting" })).ToList();
        }
    }
}
