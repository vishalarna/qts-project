using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using QTD2.Domain.Validation;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClientSettings_LicenseService : Common.IService<ClientSettings_License>
    {
        System.Threading.Tasks.Task<ClientSettings_License> GetCurrentLicense();
        System.Threading.Tasks.Task<List<ClientSettings_License>> GetLicenseHistoryAsync();
    }
}
