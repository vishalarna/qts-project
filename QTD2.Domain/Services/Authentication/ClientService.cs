using QTD2.Domain.Entities.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Authentication
{
    public class ClientService : Common.Service<Client>, Interfaces.Service.Authentication.IClientService
    {
        public ClientService(
                Interfaces.Repository.Authentication.IClientRepository clientRepository,
                Interfaces.Validation.Authentication.IClientValidation validation)
            : base(clientRepository, validation)
        {
        }

        public async Task<IEnumerable<Client>> GetByInstanceListAsync(List<string> instanceNames)
        {
            var clients = await FindWithIncludeAsync(x=>x.Active,new[] { "Instances" });
            foreach(var client in clients)
            {
                client.Instances = client.Instances.Where(x => instanceNames.Contains(x.Name) && x.Active).ToList();
            }

            return clients.Where(x => x.Instances.Any());
        }
    }
}
