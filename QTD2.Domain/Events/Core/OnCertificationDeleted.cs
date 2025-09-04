using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnCertificationDeleted : Common.IDomainEvent, INotification
    {
        public Certification DeletedCertification { get; }

        public OnCertificationDeleted(Certification deletedCertification)
        {
            DeletedCertification = deletedCertification;
        }
    }
}
