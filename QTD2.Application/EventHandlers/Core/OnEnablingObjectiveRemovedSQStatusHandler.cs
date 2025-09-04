using MediatR;
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

    public class OnEnablingObjectiveRemovedSQStatusHandler : INotificationHandler<OnEnablingObjectiveRemovedSQStatus>
    {
        private readonly IPosition_SQService _position_SQService;
        public OnEnablingObjectiveRemovedSQStatusHandler(IPosition_SQService position_SQService)
        {
            _position_SQService = position_SQService;
        }

        public async System.Threading.Tasks.Task Handle(OnEnablingObjectiveRemovedSQStatus enablingObjectiveRemovedSQStatus, CancellationToken cancellationToken)
        {
            var positionSQs = await _position_SQService.GetPositionsSQByEOIdAsync(enablingObjectiveRemovedSQStatus.EnablingObjectiveRemovedSQStatus.Id);
            foreach (var positionSQ in positionSQs)
            {
                positionSQ.Delete();
            }
            await _position_SQService.BulkUpdateAsync(positionSQs);
        }
    }
}
