using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class ILA_TaskObjective_LinkService : Common.Service<ILA_TaskObjective_Link>, IILA_TaskObjective_LinkService
    {
        public ILA_TaskObjective_LinkService(IILA_TaskObjective_LinkRepository repository, IILA_TaskObjective_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<ILA_TaskObjective_Link>> GetILA_TaskObjective_LinksByILAId(int ilaId)
        {
            return (await FindWithIncludeAsync(r => ilaId == r.ILAId, new[] { "Task.SubdutyArea.DutyArea" })).ToList();
        }
        public async System.Threading.Tasks.Task<List<ILA_TaskObjective_Link>> GetILATaskObjective_LinksAsync(int ilaId)
        {
            return (await FindWithIncludeAsync(r =>  r.ILAId ==ilaId, new[] { "Task" })).ToList();
        }

        public async Task<List<ILA_TaskObjective_Link>> GetILATaskObjectiveLinkByTaskIdAsync(int taskId)
        {
            var ila_TaskObjective_Links = await FindAsync(r => r.TaskId == taskId);
            return ila_TaskObjective_Links.ToList();
        }

    }
}
