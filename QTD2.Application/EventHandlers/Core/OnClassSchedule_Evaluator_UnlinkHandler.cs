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
    public class OnClassSchedule_Evaluator_UnlinkHandler : INotificationHandler<OnClassSchedule_Evaluator_Unlink>
    {
        private readonly ITaskQualificationService _taskQualificationService;
        public OnClassSchedule_Evaluator_UnlinkHandler(ITaskQualificationService taskQualificationService)
        {
            _taskQualificationService = taskQualificationService;
        }

        public async Task Handle(OnClassSchedule_Evaluator_Unlink notification, CancellationToken cancellationToken)
        {
            var taskQualifications = (await _taskQualificationService.FindWithIncludeAsync(x => x.ClassScheduleId == notification.ClassSchedule.Id,new string[] { "TaskQualification_Evaluator_Links" })).ToList();
            taskQualifications = taskQualifications.Where(x => x.IsPending).ToList();
            foreach (var tq in taskQualifications)
            {
                tq.UnlinkEvaluator(notification.Evaluator);
            }
            await _taskQualificationService.BulkUpdateAsync(taskQualifications);
        }
    }
}
