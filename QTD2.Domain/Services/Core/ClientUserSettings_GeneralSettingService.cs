using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClientUserSettings_GeneralSettingService : Common.Service<ClientSettings_GeneralSettings>, IClientUserSettings_GeneralSettingService
    {
        public ClientUserSettings_GeneralSettingService(IClientUserSettings_GeneralSettingRepository repository, IClientUserSettings_GeneralSettingValidation validation)
               : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<ClientSettings_GeneralSettings> GetGeneralSettings()
        {
            return (await AllAsync()).LastOrDefault();
        }
    }
}
