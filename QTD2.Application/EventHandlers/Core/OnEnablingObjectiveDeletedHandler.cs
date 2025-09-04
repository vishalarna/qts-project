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
    public class OnEnablingObjectiveDeletedHandler : INotificationHandler<OnEnablingObjectiveDeleted>
    {
        private readonly ITask_EnablingObjective_LinkService _task_EnablingObjective_LinkService;
        public OnEnablingObjectiveDeletedHandler(ITask_EnablingObjective_LinkService task_EnablingObjective_LinkService)
        {
            _task_EnablingObjective_LinkService = task_EnablingObjective_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnEnablingObjectiveDeleted eoDeleted, CancellationToken cancellationToken)
        {
            var taskEnablingObjectiveLinks = await _task_EnablingObjective_LinkService.GetTaskEnablingObjectiveLinksByEOIdAsync(eoDeleted.EnablingObjectiveDeleted.Id);
            foreach (var taskEnablingObjectiveLink in taskEnablingObjectiveLinks)
            {
                taskEnablingObjectiveLink.Delete();
            }
            await _task_EnablingObjective_LinkService.BulkUpdateAsync(taskEnablingObjectiveLinks);
        }
    }
}
