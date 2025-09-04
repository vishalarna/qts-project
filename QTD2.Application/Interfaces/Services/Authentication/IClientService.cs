using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Domain.Entities.Authentication;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IClientService
    {
        Task<Client> GetAsync(int clientId);

        Task<Client> GetAsync(string name);

        Task<List<Client>> GetByUserAsync(string username);
        Task<List<Client>> GetAllClientsAsync(string username);
        Task<List<Instance>> GetInstancesAsync(string name);

        Task<Client>  CreateClientAsync(CreateClientOptions client);

        Task UpdateClientAsync(string name, string updateName);

        Task DeleteClientAsync(string name);
    }
}
