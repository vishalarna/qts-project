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
    public class Task_HistoryService : Common.Service<Task_History>, ITask_HistoryService
    {
        public Task_HistoryService(ITask_HistoryRepository repository, ITask_HistoryValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<IEnumerable<Task_History>> GetLastXHistorysAsync(int count)
        {
            return await LatestWithIncludesAsync(new string[] { "Task.SubdutyArea.DutyArea" }, 5);
        }
    }
}
