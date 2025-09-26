using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnClassSchedule_Employee_UpdatedCompletionInfo : Common.IDomainEvent, INotification
    {
        public ClassSchedule_Employee ClassSchedule_Employee { get; set; }

        public OnClassSchedule_Employee_UpdatedCompletionInfo(ClassSchedule_Employee classSchedule_Employee)
        {
            ClassSchedule_Employee = classSchedule_Employee;
        }
    }
}
