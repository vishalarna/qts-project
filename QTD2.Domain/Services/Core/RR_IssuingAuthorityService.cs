using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class RR_IssuingAuthorityService : Common.Service<RR_IssuingAuthority>, IRR_IssuingAuthorityService
    {
        public RR_IssuingAuthorityService(IRR_IssuingAuthorityRepository repository, IRR_IssuingAuthorityValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<RR_IssuingAuthority>> GetAllCompacted()
        {
            var rrIAs = await AllQuery().Select(s => new RR_IssuingAuthority
            {
                Description = s.Description,
                Id = s.Id,
                Active = s.Active,
                Deleted = s.Deleted,
                Title = s.Title,
                Website = s.Website,
                Notes = s.Notes,
                EffectiveDate = s.EffectiveDate
            }).ToListAsync();

            return rrIAs;
        }
    }
}
