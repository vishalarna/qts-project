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
    public class OnClassSchedule_Evaluator_LinkHandler : INotificationHandler<OnClassSchedule_Evaluator_Link>
    {
        private readonly ITaskQualificationService _taskQualificationService;
        public OnClassSchedule_Evaluator_LinkHandler(ITaskQualificationService taskQualificationService) 
        {
            _taskQualificationService = taskQualificationService;
        }

        public async Task Handle(OnClassSchedule_Evaluator_Link notification, CancellationToken cancellationToken)
        {
            var taskQuals = (await _taskQualificationService.FindAsync(x=>x.ClassScheduleId == notification.ClassSchedule.Id)).ToList();
            taskQuals = taskQuals.Where(x => x.IsPending).ToList();
            foreach(var tq in taskQuals)
            {
                tq.LinkEvaluator(notification.Evaluator);
            }
            await _taskQualificationService.BulkUpdateAsync(taskQuals);
        }
    }
}
