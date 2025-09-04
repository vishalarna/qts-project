using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEnablingObjective_MetaEO_LinkService : Common.IService<EnablingObjective_MetaEO_Link>
    {
        Task<List<EnablingObjective_MetaEO_Link>> GetMetaEnablingObjectivesByEoIdAsync(List<int> eoIds, bool includeInactiveEnablingObjectives);

        Task<List<EnablingObjective_MetaEO_Link>> GetMetaEOByEoIdAsync(int eoId);
    }
}
