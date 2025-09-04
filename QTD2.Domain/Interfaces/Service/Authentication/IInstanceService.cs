using QTD2.Domain.Entities.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Authentication
{
    public interface IInstanceService : Common.IService<Instance>
    {
        Task<List<Instance>> GetActiveInstancesAsync();
        Task<List<Instance>> GetInstancesAndClientsByNamesAsync(List<string> instanceNames);
        Task<Instance> GetInstancesWithIdentityProviderLinksByNamesAsync(string instanceName);
    }
}
