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
    public class OnPublicClassScheduleFeatureEnabledHandler : INotificationHandler<OnPublicClassScheduleFeatureEnabled>
    {
        public OnPublicClassScheduleFeatureEnabledHandler()
        {
        }
        public async System.Threading.Tasks.Task Handle(OnPublicClassScheduleFeatureEnabled featureEnabled, CancellationToken cancellationToken)
        {
           ///enable the notifications associated with the public class schedule feature
        }
    }
}
