using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnPretestCompleted : Common.IDomainEvent, INotification
    {
        public Domain.Entities.Core.ClassSchedule_Roster ClassSchedule_Roster { get; set; }
    }
}
