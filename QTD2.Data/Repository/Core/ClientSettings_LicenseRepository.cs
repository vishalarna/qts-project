using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClientSettings_LicenseRepository : Common.Repository<ClientSettings_License>, IClientSettings_LicenseRepository
    {
        public ClientSettings_LicenseRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
