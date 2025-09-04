using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.Task_History;
using QTD2.Infrastructure.Model.Version_Task;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITask_HistoryService
    {
        public System.Threading.Tasks.Task SaveHistoryAsync(Task_HistoryOptions options);

        public Task<List<Task_History>> GetAllHistoryAsync();

        public Task<Task_History> GetHistoryAsync(int id);

        public Task<List<TaskLatestActivityVM>> GetLatestActivity(bool getTrimmed);

        public Task<List<TaskVersionVM>> GetLatestActivity(int taskId);

        public System.Threading.Tasks.Task RestoreHistory(int taskId, int histId);

        public Task<List<Version_Task>> GetTaskVersions(int taskId);
    }
}
