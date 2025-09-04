using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnNewEmployeeAdded : Common.IDomainEvent, INotification
    {
        public Employee Employee { get; }

        public OnNewEmployeeAdded(Employee employee)
        {
            Employee = employee;
        }
    }
}
