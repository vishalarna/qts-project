using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class EnablingObjective_CategoryService : Common.Service<EnablingObjective_Category>, IEnablingObjective_CategoryService
    {
        public EnablingObjective_CategoryService(IEnablingObjective_CategoryRepository enablingObjective_CategoryRepository, IEnablingObjective_CategoryValidation enablingObjective_CategoryValidation)
            : base(enablingObjective_CategoryRepository, enablingObjective_CategoryValidation)
        {
        }

        public async Task<List<EnablingObjective_Category>> GetMinimalEOCatData()
        {
            var cats = await AllQuery().Select(s => new EnablingObjective_Category
            {
                Active = s.Active,
                Id = s.Id,
                Description = s.Description,
                Deleted = s.Deleted,
                Number = s.Number,
                Title = s.Title
            }).ToListAsync();

            return cats;
        }

        public async Task<EnablingObjective_Category> GetMinimalEOCatDataById(int id)
        {
            var cats = (await FindAsync(x => x.Id == id)).Select(s => new EnablingObjective_Category
            {
                Active = s.Active,
                Id = s.Id,
                Description = s.Description,
                Deleted = s.Deleted,
                Number = s.Number,
                Title = s.Title
            }).FirstOrDefault();

            return cats;
        }
        public async Task<List<EnablingObjective_Category>> GetMinimalEOCatDataByIds(List<int> id)
        {
            var cats = (await FindAsync(s => id.Contains(s.Id))).Select(s => new EnablingObjective_Category
            {
                Active = s.Active,
                Id = s.Id,
                Description = s.Description,
                Deleted = s.Deleted,
                Number = s.Number,
                Title = s.Title
            }).ToList();

            return cats;
        }
    }
}
