using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnEmployee_Deleted : Common.IDomainEvent, INotification
    {
        public Employee Employee { get; }

        public OnEmployee_Deleted(Employee employee)
        {
            Employee = employee;
        }
    }
}
