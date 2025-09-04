using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class EmpCourseNotification : Notification
    {
        public int CBTId { get; set; }

        public virtual CBT CBT { get; set;}

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public EmpCourseNotification() { }

        public EmpCourseNotification(DateTime dueDate, int employeeId, int cbtId, int toPerson, int clientSettings_NotificationStepId) : base(dueDate, toPerson, clientSettings_NotificationStepId)
        {
            EmployeeId = employeeId;
            CBTId = cbtId;
        }
    }
}
