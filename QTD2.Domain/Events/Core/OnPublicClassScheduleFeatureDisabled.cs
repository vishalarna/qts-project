using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnPublicClassScheduleFeatureDisabled : Common.IDomainEvent, INotification
    {
        public OnPublicClassScheduleFeatureDisabled()
        {
        }
    }
}
