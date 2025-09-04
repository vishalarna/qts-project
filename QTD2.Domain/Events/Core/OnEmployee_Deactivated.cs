using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnEmployee_Deactivated : Common.IDomainEvent, INotification
    {
        public Employee DeactivatedEmployee { get; }

        public OnEmployee_Deactivated(Employee deactivatedEmployee)
        {
            DeactivatedEmployee = deactivatedEmployee;
        }

    }
}
