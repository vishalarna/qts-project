using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IPosition_TaskService : IService<Position_Task>
    {
        System.Threading.Tasks.Task<List<Position_Task>> GetPositionTasksForReliabilityRelatedTaskImpactMatrixR5(List<int> positionTaskIds);
        System.Threading.Tasks.Task<List<Position_Task>> GetPositionTaskAsync();
        public Task<List<Position_Task>> GetPositionTaskByPositionId(int? id);
        public Task<List<Position_Task>> GetPositionTaskByTaskIdAsync(int taskId);
        public Task<List<Position_Task>> GetPositionTasksWithPositionByTaskIdsAsync(List<int> taskIds);
        public Task<List<Position_Task>> GetPositionTasksByPositionIds(List<int> posIds);
    }
}
