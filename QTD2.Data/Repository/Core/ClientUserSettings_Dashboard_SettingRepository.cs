using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClientUserSettings_Dashboard_SettingRepository : Common.Repository<ClientUserSettings_DashboardSetting>, IClientUserSettings_Dashboard_SettingRepository
    {
        public ClientUserSettings_Dashboard_SettingRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
