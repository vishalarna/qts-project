using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_EnablingObjectiveLink_Unlinking : Common.IDomainEvent, INotification
    {
        public ILA_EnablingObjective_Link _iLA_EnablingObjective_Link;

        public OnILA_EnablingObjectiveLink_Unlinking(ILA_EnablingObjective_Link iLA_EnablingObjective_Link)
        {
            _iLA_EnablingObjective_Link = iLA_EnablingObjective_Link;
        }
    }
}
