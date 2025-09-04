using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnIDP_ReviewCreated : Common.IDomainEvent, INotification
    {
        public IDP_Review IDP_Review { get; }

        public OnIDP_ReviewCreated(IDP_Review idpReview)
        {
            IDP_Review = idpReview;
        }
    }
}
