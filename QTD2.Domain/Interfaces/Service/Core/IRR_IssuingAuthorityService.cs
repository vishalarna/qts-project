using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IRR_IssuingAuthorityService : Common.IService<RR_IssuingAuthority>
    {
        Task<List<RR_IssuingAuthority>> GetAllCompacted();
    }
}
