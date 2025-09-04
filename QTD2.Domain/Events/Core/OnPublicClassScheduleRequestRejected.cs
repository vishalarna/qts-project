using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnPublicClassScheduleRequestRejected : Common.IDomainEvent, INotification
    {
        public PublicClassScheduleRequest PublicClassScheduleRequest { get; }
    public OnPublicClassScheduleRequestRejected(PublicClassScheduleRequest publicClassScheduleRequest)
    {
        PublicClassScheduleRequest = publicClassScheduleRequest;
    }
}
}
