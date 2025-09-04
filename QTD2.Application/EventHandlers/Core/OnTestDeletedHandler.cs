using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnTestDeletedHandler : INotificationHandler<OnTestDeleted>
    {
        private readonly QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService _iLATraineeEvaluationService;

        public OnTestDeletedHandler(QTD2.Domain.Interfaces.Service.Core.IILATraineeEvaluationService iLATraineeEvaluationService)
        {
            _iLATraineeEvaluationService = iLATraineeEvaluationService;
        }

        public async System.Threading.Tasks.Task Handle(OnTestDeleted testDeleted, CancellationToken cancellationToken)
        {
            var iLATraineeEvaluations = await _iLATraineeEvaluationService.GetLinkedTestsByTestIdAsync(testDeleted.Test.Id);
            foreach (var evaluation in iLATraineeEvaluations)
            {
                evaluation.Delete();
                await _iLATraineeEvaluationService.UpdateAsync(evaluation);
            }
        }
    }
}