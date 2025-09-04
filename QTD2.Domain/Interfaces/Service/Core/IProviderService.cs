using System.Collections.Generic;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProviderService : Common.IService<Provider>
    {
        System.Threading.Tasks.Task<Provider> GetProviderByIdAsync(int providerId);
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Provider>> GetByProvidersListAsync(System.Collections.Generic.List<int> providerIds);

        System.Threading.Tasks.Task<List<Provider>> GetCompactedProvider();

        System.Threading.Tasks.Task<Provider> GetCompactedProviderById(int? providerId);

        System.Threading.Tasks.Task<List<Provider>> GetProvidersWithILAsAndClassSchedules();

        System.Threading.Tasks.Task<List<Provider>> GetProvidersWithILAClassScheduleEmployees();

        System.Threading.Tasks.Task<List<Provider>> GetProvidersWithILAClassScheduleEmployeesAndTests();
        System.Threading.Tasks.Task<List<Provider>> GetFilteredProvidersAsync(string filter, bool activeStatus, List<int> providerIds);

    }
}
