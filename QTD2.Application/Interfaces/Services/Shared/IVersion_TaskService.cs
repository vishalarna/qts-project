using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Version_Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersion_TaskService
    {
        public Task<List<Version_Task>> GetAsync();

        public Task<Version_Task> GetAsync(int id);

        //public Task<Version_Task> CreateAsync(Version_EnablingObjective_StepCreateOptions options);

        public Task<Version_Task> UpdateAsync(int id, Version_TaskUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public Task<Version_Task> VersionAndCreateCopy(Domain.Entities.Core.Task task, int state = 2,bool useCurrentUTCEffectiveDate = true);

        public Task<Version_Task> CreateTaskVersion(Domain.Entities.Core.Task task, int state, bool useCurrentUTCEffectiveDate = true);

        public Task<List<Version_Task>> GetTaskVersionsWithHistoryAsync(int taskId);
        public Task<Version_Task> UpdateVersionTaskRequalificationInfoAsync(int id, Version_TaskRequalificationInfo options);
    }
}
