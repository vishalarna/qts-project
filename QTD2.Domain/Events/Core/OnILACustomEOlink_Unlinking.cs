using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILACustomEOlink_Unlinking : Common.IDomainEvent, INotification
    {
        public ILACustomObjective_Link _iLACustomObjective_Link;

        public OnILACustomEOlink_Unlinking(ILACustomObjective_Link iLACustomObjective_Link) 
        {
            _iLACustomObjective_Link = iLACustomObjective_Link;
        }
    }
}
