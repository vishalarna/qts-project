using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using MediatR;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Employee_Enrolled : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Employee ClassSchedule_Employee { get; set; }

        public DateTime? PlannedDate { get; set; }

        public OnClassSchedule_Employee_Enrolled(ClassSchedule_Employee classSchedule_Employee)
        {
            ClassSchedule_Employee = classSchedule_Employee;
        }
    }
}
