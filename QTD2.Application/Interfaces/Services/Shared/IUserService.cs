using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IUserService
    {
        Task<AppUser> GetAsync(string username);

        Task<List<AppUser>> GetAsync();

        Task<AppUser> UpdateUserAsync(UpdateUserOptions options);

        Task<AppUser> CreateUserAsync(CreateUserOptions options);

        Task RemoveUserByInstanceAsync(string name, string instanceName);

        Task<List<Instance>> GetUserInstancesAsync(string name);

        Task AddToClientAsync(string clientName, string username);

        Task RemoveFromClientAsync(string clientName, string username);

        Task<List<Claim>> GetUserClaimsAsync(AppUser user);

        Task<string> GetJwtAsync(ClaimsBuilderOptions options, AppUser user);
        public Task RemoveUserAsync(string name);

        Task<bool> GetUserIsAdminClaimAsync(string username);
    }
}
