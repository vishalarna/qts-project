using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IRR_Task_LinkService : Common.IService<RR_Task_Link>
    {
        public System.Threading.Tasks.Task<List<RR_Task_Link>> GetRRTaskLinksByTaskIdAsync(int taskId);
        public System.Threading.Tasks.Task<List<RR_Task_Link>> GetRRTaskLinksByTaskIdsAsync(List<int> taskIds);
    }
}
