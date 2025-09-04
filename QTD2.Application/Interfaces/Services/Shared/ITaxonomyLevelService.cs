using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TaxonomyLevel;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITaxonomyLevelService
    {
        public Task<List<TaxonomyLevel>> GetAsync();

        public Task<TaxonomyLevel> GetAsync(int id);

        public Task<TaxonomyLevel> CreateAsync(TaxonomyLevelCreateOptions options);

        public Task<TaxonomyLevel> UpdateAsync(int id, TaxonomyLevelUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
