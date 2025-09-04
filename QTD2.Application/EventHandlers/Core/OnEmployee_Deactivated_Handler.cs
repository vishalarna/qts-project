using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnEmployee_Deactivated_Handler : INotificationHandler<OnEmployee_Deactivated>
    {
        private readonly Domain.Interfaces.Service.Core.ITaskQualificationService _taskQualificationService;
        private readonly Domain.Interfaces.Service.Core.ITaskQualification_Evaluator_LinkService _tq_eval_linkService;
        public OnEmployee_Deactivated_Handler(ITaskQualification_Evaluator_LinkService tq_eval_linkService, ITaskQualificationService taskQualificationService)
        {
            _tq_eval_linkService = tq_eval_linkService;
            _taskQualificationService = taskQualificationService;
        }

        public async System.Threading.Tasks.Task Handle(OnEmployee_Deactivated employee, CancellationToken cancellationToken)
        {
            var evalTqs = await _tq_eval_linkService.GetTaskQualificationsByEmployeeId(employee.DeactivatedEmployee.Id);
            var taskQuals = await _taskQualificationService.GetPendingTaskQualificationsListAsTraineeByEmpId(employee.DeactivatedEmployee.Id);

            if (evalTqs.Count != 0 && evalTqs != null)
            {
                foreach (var evalTq in evalTqs)
                {
                    evalTq.Deactivate();
                }
                await _tq_eval_linkService.BulkUpdateAsync(evalTqs);
            }

            if(taskQuals.Count !=0 && taskQuals != null)
            {
                foreach(var tqual in taskQuals)
                {
                    foreach (var tqeval in tqual.TaskQualification_Evaluator_Links)
                    {
                        tqeval.Deactivate();
                    }
                    tqual.Deactivate();
                }
                await _taskQualificationService.BulkUpdateAsync(taskQuals);
            }
        }
    }
}
