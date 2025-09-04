using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_IssuingAuthorityService : Common.Service<Entities.Core.Procedure_IssuingAuthority>, Interfaces.Service.Core.IProcedure_IssuingAuthorityService
    {
        public Procedure_IssuingAuthorityService(IProcedure_IssuingAuthorityRepository procedure_IssuingAuthorityRepository, IProcedure_IssuingAuthorityValidation procedure_IssuingAuthorityValidation)
            : base(procedure_IssuingAuthorityRepository, procedure_IssuingAuthorityValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<Procedure_IssuingAuthority>> GetAllProceduresByIssuingAuthorityAsync()
        {
            return (await AllAsync()).ToList();
        }
    }
}
