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
    public class SubdutyAreaService : Common.Service<SubdutyArea>, ISubdutyAreaService
    {
        public SubdutyAreaService(ISubdutyAreaRepository subdutyAreaRepository, ISubdutyAreaValidation subdutyAreaValidation)
            : base(subdutyAreaRepository, subdutyAreaValidation)
        {
        }

        public Task<List<SubdutyArea>> GetAllOrderByNumber()
        {
            var subDutyAreas = AllQuery().Select(s => new SubdutyArea
            {
                Id = s.Id,
                Active = s.Active,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Description = s.Description,
                Deleted = s.Deleted,
                EffectiveDate = s.EffectiveDate,
                DutyAreaId = s.DutyAreaId,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                ReasonForRevision = s.ReasonForRevision,
                SubNumber = s.SubNumber,
                Title = s.Title
            }).OrderBy(o => o.SubNumber).ToListAsync();

            return subDutyAreas;
        }

        public async Task<List<SubdutyArea>> GetSubDutyAreasByDutyAreaIdAsync(int dutyAreaId)
        {
            var subDutyAreas = (await FindAsync(x => x.DutyAreaId == dutyAreaId)).ToList();
            return subDutyAreas;
        }
    }
}
