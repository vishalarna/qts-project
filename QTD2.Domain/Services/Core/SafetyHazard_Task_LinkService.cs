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
    public class SafetyHazard_Task_LinkService : Common.Service<SafetyHazard_Task_Link>, ISafetyHazard_Task_LinkService
    {
        public SafetyHazard_Task_LinkService(ISafetyHazard_Task_LinkRepository repository, ISafetyHazard_Task_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<SafetyHazard_Task_Link>> GetSafetyHazardTaskLinksByTaskIdAsync(int taskId)
        {
            var safetyHazard_Task_Links = await FindAsync(r => r.TaskId == taskId);
            return safetyHazard_Task_Links.ToList();
        }
    }
}
