using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnEmployeeCertificationHistoryCreated : Common.IDomainEvent, INotification
    {
        public EmployeeCertifictaionHistory EmployeeCertificationHistory { get; }

        public OnEmployeeCertificationHistoryCreated(EmployeeCertifictaionHistory employeeCertificationHistory)
        {
            EmployeeCertificationHistory = employeeCertificationHistory;
        }
    }
}
