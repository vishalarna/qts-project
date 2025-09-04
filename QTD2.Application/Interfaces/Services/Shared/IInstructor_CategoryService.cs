using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor_Category;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IInstructor_CategoryService
    {
        public Task<List<Instructor_Category>> GetAsync();

        public Task<Instructor_Category> GetAsync(int id);

        public Task<Instructor_Category> CreateAsync(Instructor_CategoryCreateOptions options);

        public Task<Instructor_Category> UpdateAsync(int id, Instructor_CategoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<int> getCount();

        public Task<List<InstructorCategoryCompactOptions>> GetInsCategoryWithIns();
    }
}
