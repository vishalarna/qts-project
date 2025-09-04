using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClientUserSettings_GeneralSettingRepository : Common.Repository<ClientSettings_GeneralSettings>, IClientUserSettings_GeneralSettingRepository
    {
        public ClientUserSettings_GeneralSettingRepository(QTDContext context)
           : base(context)
        {
        }

    }
}
