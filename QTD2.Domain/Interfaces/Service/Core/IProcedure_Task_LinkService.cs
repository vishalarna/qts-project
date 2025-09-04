using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProcedure_Task_LinkService : Common.IService<Procedure_Task_Link>
    {
        public System.Threading.Tasks.Task<List<Procedure_Task_Link>> GetProcedureTaskLinksByTaskIdAsync(int taskId);
    }
}
