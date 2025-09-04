using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class MetaIla_Employee_Enrolled : Common.IDomainEvent, INotification
    {
        public MetaILA_Employee MetaILA_Employee { get; }
        public bool UseCurrentDate { get; }

        public MetaIla_Employee_Enrolled(MetaILA_Employee metaIla_employee, bool useCurrentDate)
        {
            MetaILA_Employee = metaIla_employee;
            UseCurrentDate = useCurrentDate;
        }
    }
}
