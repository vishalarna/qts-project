using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_TaskObjectiveLink_Unlinking : Common.IDomainEvent, INotification
    {
        public ILA_TaskObjective_Link _iLA_TaskObjective_Link;

        public OnILA_TaskObjectiveLink_Unlinking(ILA_TaskObjective_Link iLA_TaskObjective_Link)
        {
            _iLA_TaskObjective_Link = iLA_TaskObjective_Link;
        }
    }
}
