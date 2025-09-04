using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnPublicClassScheduleRequestAccepted: Common.IDomainEvent, INotification
    {
        public PublicClassScheduleRequest PublicClassScheduleRequest { get; }
        public OnPublicClassScheduleRequestAccepted(PublicClassScheduleRequest publicClassScheduleRequest)
        {
            PublicClassScheduleRequest = publicClassScheduleRequest;
        }
    }
}
