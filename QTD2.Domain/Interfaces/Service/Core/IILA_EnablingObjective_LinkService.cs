using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_EnablingObjective_LinkService : Common.IService<ILA_EnablingObjective_Link>
    {
    public System.Threading.Tasks.Task<List<ILA_EnablingObjective_Link>> GetILA_EnablingObjective_LinksByILAId(int ilaId);
    }
}
