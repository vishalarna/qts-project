using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_Activated : Common.IDomainEvent, INotification
    {
        public ILA ActivatedILA { get; }

        public OnILA_Activated(ILA activatedILA)
        {
            ActivatedILA = activatedILA;
        }
    }
}
