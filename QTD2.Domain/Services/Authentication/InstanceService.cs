using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Domain.Interfaces.Validation.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Authentication
{
    public class InstanceService : Common.Service<Instance>, IInstanceService
    {
        public InstanceService(IInstanceRepository repository, IInstanceValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<Instance>> GetActiveInstancesAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }

        public async Task<List<Instance>> GetInstancesAndClientsByNamesAsync(List<string> instanceNames)
        {
            instanceNames = instanceNames.Select(s => s.ToUpper()).ToList();
            return (await FindWithIncludeAsync(r => instanceNames.Contains(r.Name.ToUpper()), new string[] { "Client" })).ToList();
        }

        public async Task<Instance> GetInstancesWithIdentityProviderLinksByNamesAsync(string instanceName)
        {
            var instance = (await FindWithIncludeAsync(r => r.Name.ToUpper() == instanceName.ToUpper(), new string[] { "InstanceIdentityProviderLinks.IdentityProvider" })).FirstOrDefault();
            return instance;
        }
    }
}
