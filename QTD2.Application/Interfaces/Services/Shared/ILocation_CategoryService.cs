using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Location_Category;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ILocation_CategoryService
    {
        public Task<List<Location_Category>> GetAsync();

        public Task<Location_Category> GetAsync(int id);

        public Task<Location_Category> CreateAsync(Location_CategoryCreateOptions options);

        public Task<Location_Category> UpdateAsync(int id, Location_CategoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<int> getCount();
        public Task<List<LocationCategoryCompactOptions>> GetLocCategoryWithLoc();
    }
}
