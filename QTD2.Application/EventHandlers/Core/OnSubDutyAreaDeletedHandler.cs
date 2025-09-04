using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnSubDutyAreaDeletedHandler : INotificationHandler<OnSubDutyAreaDeleted>
    {
        private readonly ITaskService _taskService;
        public OnSubDutyAreaDeletedHandler(ITaskService taskService) 
        {
            _taskService = taskService;
        }

        public async System.Threading.Tasks.Task Handle(OnSubDutyAreaDeleted subDutyAreaDeleted, CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetTasksListBySubDutyAreaIdAsync(subDutyAreaDeleted.SubDutyAreaDeleted.Id);
            foreach(var task in tasks)
            {
                task.Delete();
            }
            await _taskService.BulkUpdateAsync(tasks);
        }
    }
}
