using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClientUser;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClientUserService
    {
        public Task<List<ClientUser>> GetAsync();

        public Task<ClientUser> GetAsync(int personId);

        public Task<ClientUser> CreateAsync(ClientUserCreateOptions options, bool isReturnConflictExp = false);

        public System.Threading.Tasks.Task DeleteAsync(int personId);
        public Task<ClientUser> DeactivateAsync(int personId);
        public Task<ClientUser> ActivateAsync(int personId);
    }
}
