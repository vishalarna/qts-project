using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Provider;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProviderService
    {
        public Task<List<Provider>> GetAsync();

        public Task<List<Provider>> GetProviderWithoutIncludes();

        public Task<List<Provider>> GetActiveProvidersAsync();

        public Task<List<Provider>> GetWithILAAsync();

        public Task<List<ILA_ProviderVM>> GetWithILACountAsync();
        public Task<List<ILA_ProviderVM>> GetProviderWithFilterAndILACount(FilterByOptions filterOptions);

        public Task<Provider> GetAsync(int id);
        
        public Task<Provider> GetOnlyProviderAsync(int id);

        public Task<Provider> CreateAsync(ProviderCreateOptions options);

        public Task<Provider> UpdateAsync(int id, ProviderUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
