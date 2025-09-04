using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_TaskObjective_LinkService : Common.IService<ILA_TaskObjective_Link>
    {
        public System.Threading.Tasks.Task<List<ILA_TaskObjective_Link>> GetILA_TaskObjective_LinksByILAId(int ilaId);
        public System.Threading.Tasks.Task<List<ILA_TaskObjective_Link>> GetILATaskObjective_LinksAsync(int ilaId);
        public System.Threading.Tasks.Task<List<ILA_TaskObjective_Link>> GetILATaskObjectiveLinkByTaskIdAsync(int taskId);
    }
}
