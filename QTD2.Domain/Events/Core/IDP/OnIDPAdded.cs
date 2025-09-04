using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnIDPAdded : Common.IDomainEvent, INotification
    {
        public IDP IDP { get; set; }

        public OnIDPAdded(IDP iDP)
        {
            IDP = iDP;
        }
    }
}
