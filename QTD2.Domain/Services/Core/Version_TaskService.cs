using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QTD2.Domain.Services.Core
{
    public class Version_TaskService : Common.Service<Version_Task>, IVersion_TaskService
    {
        public Version_TaskService(IVersion_TaskRepository repository, IVersion_TaskValidation validation)
            : base(repository, validation)
        {
        }
        public async System.Threading.Tasks.Task<List<Version_Task>> GetTaskHistoryAsync(List<int> taskIds, List<DateTime> dateRange)
        {
            List<Expression<Func<Domain.Entities.Core.Version_Task, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.Version_Task, bool>>>();
            predicates.Add(hst => taskIds.Contains(hst.TaskId));

            if (dateRange.Any())
            {
                predicates.Add(r => r.EffectiveDate.HasValue && r.EffectiveDate.Value >= DateOnly.FromDateTime(dateRange[0]) && r.EffectiveDate.Value <= DateOnly.FromDateTime(dateRange[1]));
            }

            var version_Tasks = (await FindWithIncludeAsync(predicates, new[] { "Task.SubdutyArea.DutyArea", "Task_Histories" }, true)).ToList();
            return version_Tasks;
        }

        public async System.Threading.Tasks.Task<Version_Task> GetRecentVersionTaskAsync(int taskId)
        {
            var version_Tasks = await FindAsync(r => r.TaskId == taskId);
            return version_Tasks.OrderByDescending(r=>r.Id).FirstOrDefault();
        }
    }
}
