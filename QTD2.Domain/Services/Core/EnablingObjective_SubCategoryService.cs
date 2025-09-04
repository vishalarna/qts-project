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
    public class EnablingObjective_SubCategoryService : Common.Service<EnablingObjective_SubCategory>, IEnablingObjective_SubCategoryService
    {
        public EnablingObjective_SubCategoryService(IEnablingObjective_SubCategoryRepository enablingObjective_SubCategoryRepository, IEnablingObjective_SubCategoryValidation enablingObjective_SubCategoryValidation)
            : base(enablingObjective_SubCategoryRepository, enablingObjective_SubCategoryValidation)
        {
        }

        public async Task<List<EnablingObjective_SubCategory>> GetMinimalEOSubCatData()
        {
            var subCats = await AllQuery().Select(s => new EnablingObjective_SubCategory
            {
                Active = s.Active,
                Id = s.Id,
                CategoryId = s.CategoryId,
                Number = s.Number,
                Title = s.Title,
                Description = s.Description,
                Deleted = s.Deleted,
                EffectiveDate = s.EffectiveDate
            }).ToListAsync();

            return subCats;
        }
    }
}
