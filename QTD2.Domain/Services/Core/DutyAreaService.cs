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
    public class DutyAreaService : Common.Service<DutyArea>, IDutyAreaService
    {
        public DutyAreaService(IDutyAreaRepository dutyAreaRepository, IDutyAreaValidation dutyAreaValidation)
            : base(dutyAreaRepository, dutyAreaValidation)
        {
        }

        public async Task<List<DutyArea>> GetAllOrderByNumber()
        {
            var dutyareas = await AllQuery().Select(s => new DutyArea
            {
                Id = s.Id,
                Active = s.Active,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                Description = s.Description,
                Deleted = s.Deleted,
                EffectiveDate = s.EffectiveDate,
                Letter = s.Letter,
                Title = s.Title,
                Number = s.Number,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                ReasonForRevision = s.ReasonForRevision
            }).OrderBy(o => o.Number).ToListAsync();

            return dutyareas;
        }

        public async Task<List<DutyArea>> GetDutyAreasWithSubDutyAreaTaskTaskQualificationEmployees()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "SubdutyAreas.Tasks.TaskQualifications.Employee.Person" });
            return queryable.ToList();
        }
    }
}
