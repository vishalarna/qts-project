using MediatR;
using QTD2.Domain.Entities.Core;
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
    public class OnPublicClassScheduleFeatureDisabledHandler : INotificationHandler<OnPublicClassScheduleFeatureDisabled>
    {
        public OnPublicClassScheduleFeatureDisabledHandler()
        {

        }
        public async System.Threading.Tasks.Task Handle(OnPublicClassScheduleFeatureDisabled featureDisabled, CancellationToken cancellationToken)
        {
            //disable the notifications associated with the feature
        }
    }
}
