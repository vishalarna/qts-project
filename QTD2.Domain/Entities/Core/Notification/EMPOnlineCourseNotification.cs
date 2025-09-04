using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EMPOnlineCourseNotification : Notification
    {
        public int CBTId { get; set; }

        public virtual CBT CBT { get; set;}

        public int ClassScheduleEmployeeId { get; set; }

        public virtual ClassSchedule_Employee ClassScheduleEmployee { get; set; }

        public EMPOnlineCourseNotification() { }

        public EMPOnlineCourseNotification(DateTime dueDate, int classScheduleEmployeeId, int cbtId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            ClassScheduleEmployeeId = classScheduleEmployeeId;
            CBTId = cbtId;
        }
    }
}
