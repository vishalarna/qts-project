using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Procedure_Task_LinkService : Common.Service<Procedure_Task_Link>, IProcedure_Task_LinkService
    {
        public Procedure_Task_LinkService(IProcedure_Task_LinkRepository repository, IProcedure_Task_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<Procedure_Task_Link>> GetProcedureTaskLinksByTaskIdAsync(int taskId)
        {
            var procedure_Task_Links = await FindAsync(r => r.TaskId == taskId);
            return procedure_Task_Links.ToList();
        }
    }
}
