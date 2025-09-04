using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IVersion_TaskService : Common.IService<Version_Task>
    {
        public System.Threading.Tasks.Task<List<Version_Task>> GetTaskHistoryAsync(List<int> taskIds, List<DateTime> dateRange);
        public System.Threading.Tasks.Task<Version_Task> GetRecentVersionTaskAsync(int taskId);
    }
}
