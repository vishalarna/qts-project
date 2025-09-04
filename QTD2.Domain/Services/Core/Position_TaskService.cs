using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace QTD2.Domain.Services.Core
{
    public class Position_TaskService : Common.Service<Position_Task>, IPosition_TaskService
    {
        public Position_TaskService(IPosition_TaskRepository positiontaskRepository, IPosition_TaskValidation positiontaskValidation)
            : base(positiontaskRepository, positiontaskValidation)
        {
        }

        public async Task<List<Position_Task>> GetPositionTasksForReliabilityRelatedTaskImpactMatrixR5(List<int> positionTaskIds)
        {
            List<Expression<Func<Position_Task, bool>>> predicates = new List<Expression<Func<Position_Task, bool>>>();

            predicates.Add(pt => positionTaskIds.Contains(pt.Id));

            var positionTasks = (await FindWithIncludeAsync(predicates, new string[] {
                "R5ImpactedTasks.ImpactedTask",
                "Position"
            })).ToList();

            return positionTasks;
        }

        public async Task<List<Position_Task>> GetPositionTaskAsync()
        {
            var positionTasks = (await AllWithIncludeAsync( new string[] {
                "Task.SubdutyArea.DutyArea",
                "Position",
                "R5ImpactedTasks",
            })).ToList();

            var impactedPositionTasks = positionTasks.Where(pt => pt.IsR5Impacted).ToList();
            return impactedPositionTasks;
        }

        public async Task<List<Position_Task>> GetPositionTaskByPositionId(int? id)
        {
            var positionTasks = (await FindWithIncludeAsync(x => x.PositionId == id, new string[] { "Task" })).ToList();
            return positionTasks;
        }

        public async Task<List<Position_Task>> GetPositionTaskByTaskIdAsync(int taskId)
        {
            var positionTasks = await FindAsync(r => r.TaskId == taskId);
            return positionTasks.ToList();
        }

        public async Task<List<Position_Task>> GetPositionTasksWithPositionByTaskIdsAsync(List<int> taskIds)
        {
            List<Expression<Func<Domain.Entities.Core.Position_Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Position_Task, bool>>>();
            predicates.Add(r => taskIds.Contains(r.TaskId));
            return (await FindWithIncludeAsync(predicates, new string[] { "Position" },true)).ToList();
        }

        public async Task<List<Position_Task>> GetPositionTasksByPositionIds(List<int> posIds)
        {
            var positionTasks = (await FindWithIncludeAsync(x => posIds.Contains(x.PositionId), new string[] { "Task" })).ToList();
            return positionTasks;
        }
    }
}
