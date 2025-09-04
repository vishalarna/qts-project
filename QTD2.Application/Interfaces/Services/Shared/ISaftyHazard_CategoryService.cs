using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SafetyHazard_Category;
using QTD2.Infrastructure.Model.SafetyHazard_CategoryHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISaftyHazard_CategoryService
    {
        public Task<List<SaftyHazard_Category>> GetAsync();

        public Task<SaftyHazard_Category> GetAsync(int id);

        public Task<List<SaftyHazardCategoryCompactOptions>> GetSHCategoryWithSH();

        public Task<SaftyHazard_Category> SaveCategoryAsync(SafetyHazard_CategoryCreateOptions options);

        public System.Threading.Tasks.Task ActiveAsync(SaftyHazardCategoryOptions options);

        public System.Threading.Tasks.Task InActiveAsync(SaftyHazardCategoryOptions options);

        public System.Threading.Tasks.Task DeleteAsync(SaftyHazardCategoryOptions options);

        public Task<SaftyHazard_Category> UpdateAsync(int id, SafetyHazard_CategoryCreateOptions options);

        public Task<int> getCount();
    }
}
