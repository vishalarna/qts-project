using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnILA_Deactivated : Common.IDomainEvent, INotification
    {
        public ILA DeactivatedILA { get; }

        public OnILA_Deactivated(ILA deactivatedILA)
        {
            DeactivatedILA = deactivatedILA;
        }
    }
}
