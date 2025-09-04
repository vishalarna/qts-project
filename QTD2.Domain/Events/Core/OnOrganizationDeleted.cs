using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnOrganizationDeleted : Common.IDomainEvent, INotification
    {
        public Organization Organization { get; }

        public OnOrganizationDeleted(Organization organization)
        {
            Organization = organization;
        }
    }
}
