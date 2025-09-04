using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProcedure_IssuingAuthorityService : Common.IService<Entities.Core.Procedure_IssuingAuthority>
    {
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Procedure_IssuingAuthority>> GetAllProceduresByIssuingAuthorityAsync();
    }
}
