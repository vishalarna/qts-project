using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Organization;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IOrganizationService
    {
        public Task<List<Organization>> GetAsync();

        public Task<List<OrganizationIdAndNameVM>> GetSimplifiedDataAsync();

        public Task<List<Organization>> GetAllOrderByAsync(string orderBy);

        public Task<Organization> GetAsync(int id);

        public Task<Organization> CreateAsync(OrganizationCreateOptions options);

        public Task<Organization> UpdateAsync(int id, OrganizationUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);
        public Task<List<Organization>> GetPublicOrganizationAsync();
    }
}
