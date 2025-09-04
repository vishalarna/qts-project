using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;

namespace QTD2.Data.Repository.Authentication
{
    public class InstanceSettingRepository : Common.Repository<InstanceSetting>, IInstanceSettingRepository
    {
        public InstanceSettingRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
