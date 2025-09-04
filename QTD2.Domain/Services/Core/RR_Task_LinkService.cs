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
    public class RR_Task_LinkService : Common.Service<RR_Task_Link>, IRR_Task_LinkService
    {
        public RR_Task_LinkService(IRR_Task_LinkRepository repository, IRR_Task_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<RR_Task_Link>> GetRRTaskLinksByTaskIdAsync(int taskId)
        {
            var rr_Task_Links = await FindAsync(r => r.TaskId == taskId);
            return rr_Task_Links.ToList();
        }

        public async Task<List<RR_Task_Link>> GetRRTaskLinksByTaskIdsAsync(List<int> taskIds)
        {
            return (await FindWithIncludeAsync(r => taskIds.Contains(r.TaskId), new string[] { "RegulatoryRequirement" })).ToList();
        }
    }
}
