using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnCbtCreated : Common.IDomainEvent, INotification
    {
        public CBT CBT { get; }

        public OnCbtCreated(CBT cbt)
        {
            CBT = cbt;
        }
    }
}
