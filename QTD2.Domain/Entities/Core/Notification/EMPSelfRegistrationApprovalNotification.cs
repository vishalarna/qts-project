using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPSelfRegistrationApprovalNotification : Notification
    {
        public int ClassScheduleEmployeeId { get; set; }

        public virtual ClassSchedule_Employee ClassScheduleEmployee { get; set; }

        public EMPSelfRegistrationApprovalNotification() { }

        public EMPSelfRegistrationApprovalNotification(DateTime dueDate, int classScheduleEmployeeId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            ClassScheduleEmployeeId = classScheduleEmployeeId;
        }
    }
}
