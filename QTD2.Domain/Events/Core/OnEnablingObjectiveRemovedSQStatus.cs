using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnEnablingObjectiveRemovedSQStatus : Common.IDomainEvent, INotification
    {
        public EnablingObjective EnablingObjectiveRemovedSQStatus;

        public OnEnablingObjectiveRemovedSQStatus(EnablingObjective enablingObjectiveRemovedSQStatus)
        {
            EnablingObjectiveRemovedSQStatus = enablingObjectiveRemovedSQStatus;
        }
    }
}
