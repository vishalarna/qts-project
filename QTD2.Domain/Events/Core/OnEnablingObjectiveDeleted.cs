using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnEnablingObjectiveDeleted : Common.IDomainEvent, INotification
    {
        public EnablingObjective EnablingObjectiveDeleted;

        public OnEnablingObjectiveDeleted(EnablingObjective enablingObjectiveDeleted)
        {
            EnablingObjectiveDeleted = enablingObjectiveDeleted;
        }
    }
}
